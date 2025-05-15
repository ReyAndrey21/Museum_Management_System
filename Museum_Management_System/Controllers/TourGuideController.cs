using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Museum_Management_System.Data;
using Museum_Management_System.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authorization; // Comentat pentru testare
// using Microsoft.AspNetCore.Identity;    // Comentat pentru testare

namespace Museum_Management_System.Controllers
{
    // --- ViewModel (rămâne la fel) ---
    public class TourGuideProfileViewModel
    {
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Status { get; set; }
        public string? ForeignLanguages { get; set; }
    }

    // --- Controller Modificat pentru Testare ---

    // [Authorize] // Comentat pentru testare
    public class TourGuideController : Controller
    {
        private readonly AppDbContext _context;
        // private readonly UserManager<Users> _userManager; // Comentat/Eliminat

        // Constructor - NU mai injectăm UserManager acum
        public TourGuideController(AppDbContext context)
        {
            _context = context;
        }

        // Dashboard de start pentru ghidul de tur
        public IActionResult DashboardTourGuide()
        {
            int? userId = HttpContext.Session.GetInt32("IdUsers");
            if (userId == null)
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            var tourGuide = _context.TourGuides
                .Include(tg => tg.User)
                .FirstOrDefault(tg => tg.IdUsers == userId);

            if (tourGuide == null)
            {
                TempData["ErrorMessage"] = "Nu ați fost găsit ca ghid de tur în sistem.";
                return RedirectToAction("Login", "UsersAuth");
            }

            var viewModel = new TourGuideProfileViewModel
            {
                Username = tourGuide.User?.Username,
                FirstName = tourGuide.User?.FirstName,
                LastName = tourGuide.User?.LastName,
                ProfilePicture = tourGuide.User?.ProfilePicture,
                Status = tourGuide.Status,
                ForeignLanguages = tourGuide.ForeignLanguages
            };

            return View(viewModel);
        }

        // Funcție modernizată pentru a obține ID-ul ghidului din sesiune
        private int GetCurrentGuideId()
        {
            int? userId = HttpContext.Session.GetInt32("IdUsers");
            if (userId == null)
            {
                return -1; // Returnăm -1 pentru a indica o eroare
            }

            var tourGuide = _context.TourGuides.FirstOrDefault(tg => tg.IdUsers == userId);
            if (tourGuide == null)
            {
                return -1; // Ghidul nu a fost găsit
            }

            return tourGuide.IdTourGuide;
        }

        // GET: /TourGuide/ or /TourGuide/Index
        public async Task<IActionResult> Index()
        {
            // --- Folosește metoda sincronă ---
            int currentGuideId = GetCurrentGuideId();
            if (currentGuideId <= 0)
            {
                TempData["ErrorMessage"] = "ID Ghid invalid (hardcodat). Verificați GetCurrentGuideId().";
                return RedirectToAction("Index", "Home"); // Mergi la Home dacă ID-ul e invalid
            }

            try
            {
                var guideTours = await _context.Tours
                                       .Where(t => t.IdTourGuide == currentGuideId)
                                       .Include(t => t.TourBookings) // Include rezervările dacă ai nevoie de ele în View
                                       .OrderByDescending(t => t.DateTour)
                                       .ThenByDescending(t => t.HourTour)
                                       .ToListAsync();

                return View(guideTours); // Trimite lista (poate fi goală)
            }
            catch (Exception ex) // Prinde orice eroare la interogare
            {
                TempData["ErrorMessage"] = "A apărut o eroare la încărcarea tururilor.";
                System.Diagnostics.Debug.WriteLine($"EROARE în TourGuideController/Index: {ex}"); // Loghează eroarea
                return View(new List<Tour>()); // Returnează view-ul cu o listă goală
            }
        }

        // GET: /TourGuide/Create
        public IActionResult Create()
        {
            var model = new Tour { DateTour = DateOnly.FromDateTime(DateTime.Today) };
            return View(model);
        }

        // POST: /TourGuide/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,AvailableSpots,Duration,DateTour,HourTour")] Tour tour)
        {
            // --- Folosește metoda sincronă ---
            int currentGuideId = GetCurrentGuideId();
            if (currentGuideId <= 0)
            {
                ModelState.AddModelError("", "ID-ul ghidului (hardcodat) nu a putut fi determinat sau este invalid.");
                return View(tour); // Reafișează cu eroare
            }

            // Setează FK înainte de validare
            tour.IdTourGuide = currentGuideId;

            // Elimină validarea pentru ce nu vine din formular sau e generat
            ModelState.Remove(nameof(Tour.IdTour));
            ModelState.Remove(nameof(Tour.TourGuide));
            ModelState.Remove(nameof(Tour.TourBookings));
            ModelState.Remove(nameof(Tour.Reviews));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(tour);
                    await _context.SaveChangesAsync(); // Aici poate apărea eroarea de FK dacă ghidul 1 nu există
                    TempData["SuccessMessage"] = "Turul a fost creat cu succes!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx) // Prinde specific erori de DB la salvare
                {
                    System.Diagnostics.Debug.WriteLine($"EROARE DbUpdateException la salvare tur: {dbEx} --- Inner: {dbEx.InnerException}");
                    var specificError = dbEx.InnerException?.Message ?? dbEx.Message;
                    // Verifică dacă eroarea este legată de cheia externă
                    if (specificError.Contains("violates foreign key constraint") && (specificError.Contains("TourGuide") || specificError.Contains("id_tour_guide")))
                    {
                        ModelState.AddModelError("", $"Salvare eșuată: Ghidul cu ID {currentGuideId} nu există în baza de date. Verificați tabela TourGuide.");
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Eroare la salvarea în baza de date: {specificError}");
                    }
                    TempData["ErrorMessage"] = "Eroare la salvarea în baza de date. Verificați datele și consola.";
                    return View(tour);
                }
                catch (Exception ex) // Altă eroare neașteptată
                {
                    TempData["ErrorMessage"] = "A apărut o eroare neașteptată la salvarea turului.";
                    System.Diagnostics.Debug.WriteLine($"EROARE Exception la salvare tur: {ex}");
                    ModelState.AddModelError("", "Eroare neașteptată la salvare.");
                    return View(tour);
                }
            }

            // Dacă ModelState NU e valid
            TempData["ErrorMessage"] = "Vă rugăm corectați erorile din formular.";
            return View(tour);
        }

        // GET: /TourGuide/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound("ID-ul turului nu a fost specificat.");

            // --- Folosește metoda sincronă ---
            int currentGuideId = GetCurrentGuideId();
            if (currentGuideId <= 0) { TempData["ErrorMessage"] = "ID Ghid invalid (hardcodat)."; return RedirectToAction("Index", "Home"); }

            try
            {
                var tour = await _context.Tours.FindAsync(id);

                if (tour == null) return NotFound($"Turul cu ID {id} nu a fost găsit.");

                // Verifică dacă ghidul hardcodat este proprietarul
                if (tour.IdTourGuide != currentGuideId)
                {
                    // În modul de testare, poate vrei să permiți editarea oricum
                    // sau să afișezi un mesaj clar
                    TempData["WarningMessage"] = $"Atenție: Editați turul {id} care aparține ghidului {tour.IdTourGuide}, nu celui curent (hardcodat {currentGuideId}).";
                    // return Forbid(); // Sau activează Forbid dacă vrei să testezi și asta
                }

                // Verificare dacă mai poate fi editat
                if (tour.DateTour < DateOnly.FromDateTime(DateTime.Today))
                {
                    TempData["ErrorMessage"] = "Doar tururile viitoare pot fi editate.";
                    return RedirectToAction(nameof(Index));
                }

                return View(tour);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "A apărut o eroare la încărcarea turului pentru editare.";
                System.Diagnostics.Debug.WriteLine($"EROARE la Edit GET (ID: {id}): {ex}");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /TourGuide/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTour,Title,Description,AvailableSpots,Duration,DateTour,HourTour")] Tour tour)
        {
            if (id != tour.IdTour) return BadRequest("ID-ul din rută nu corespunde cu ID-ul din formular.");

            // --- Folosește metoda sincronă ---
            int currentGuideId = GetCurrentGuideId();
            if (currentGuideId <= 0) { ModelState.AddModelError("", "ID Ghid invalid (hardcodat)."); return View(tour); }

            // Re-verificare proprietate folosind datele din DB
            var originalTour = await _context.Tours.AsNoTracking().FirstOrDefaultAsync(t => t.IdTour == id);
            if (originalTour == null) return NotFound($"Turul original cu ID {id} nu a fost găsit.");

            // Verifică dacă ghidul hardcodat este proprietarul
            if (originalTour.IdTourGuide != currentGuideId)
            {
                TempData["WarningMessage"] = $"Atenție: Încercați să editați turul {id} care aparține ghidului {originalTour.IdTourGuide}, nu celui curent (hardcodat {currentGuideId}).";
                // return Forbid(); // Sau activează Forbid
            }

            // Re-verificare dacă mai poate fi editat
            if (originalTour.DateTour < DateOnly.FromDateTime(DateTime.Today))
            {
                TempData["ErrorMessage"] = "Acest tur nu mai poate fi editat (data a trecut).";
                // Reafișează view-ul Edit cu modelul original (nemodificat) și mesajul
                return View(originalTour);
            }

            // Păstrează Id-ul ghidului corect (cel original sau cel hardcodat, depinde ce vrei)
            // Pentru a permite salvarea în test, folosim cel hardcodat. Atenție la logica de ownership!
            tour.IdTourGuide = currentGuideId;

            // Elimină validarea doar pentru proprietățile de navigare
            ModelState.Remove(nameof(Tour.TourGuide));
            ModelState.Remove(nameof(Tour.TourBookings));
            ModelState.Remove(nameof(Tour.Reviews));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tour);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Turul a fost modificat cu succes!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.IdTour)) return NotFound();
                    else { ModelState.AddModelError("", "Datele au fost modificate de altcineva. Reîncărcați."); }
                    // Reîncarcă datele curente din DB
                    var currentDbValues = await _context.Tours.FindAsync(id);
                    return View(currentDbValues ?? tour);
                }
                catch (DbUpdateException dbEx)
                {
                    System.Diagnostics.Debug.WriteLine($"EROARE DbUpdateException la editare tur: {dbEx} --- Inner: {dbEx.InnerException}");
                    ModelState.AddModelError("", $"Eroare la salvarea în baza de date: {dbEx.InnerException?.Message ?? dbEx.Message}");
                    return View(tour);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "A apărut o eroare neașteptată la salvarea modificărilor.";
                    System.Diagnostics.Debug.WriteLine($"EROARE Exception la editare tur: {ex}");
                    ModelState.AddModelError("", "Eroare neașteptată la salvare.");
                    return View(tour);
                }
            }

            // Dacă ModelState NU e valid
            TempData["ErrorMessage"] = "Vă rugăm corectați erorile din formular.";
            return View(tour);
        }


        // GET: /TourGuide/Cancel/5
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null) return NotFound();

            // --- Folosește metoda sincronă ---
            int currentGuideId = GetCurrentGuideId();
            if (currentGuideId <= 0) { TempData["ErrorMessage"] = "ID Ghid invalid (hardcodat)."; return RedirectToAction("Index", "Home"); }

            try
            {
                var tour = await _context.Tours.FirstOrDefaultAsync(m => m.IdTour == id);
                if (tour == null) return NotFound();

                // Verificare proprietate (poate fi comentată pentru testare)
                if (tour.IdTourGuide != currentGuideId)
                {
                    TempData["WarningMessage"] = $"Atenție: Anulați turul {id} care aparține ghidului {tour.IdTourGuide}.";
                    // return Forbid();
                }

                // Re-verificare dacă poate fi anulat
                if (tour.DateTour < DateOnly.FromDateTime(DateTime.Today))
                {
                    TempData["ErrorMessage"] = "Doar tururile viitoare pot fi anulate.";
                    return RedirectToAction(nameof(Index));
                }

                return View(tour);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "A apărut o eroare la pregătirea anulării.";
                System.Diagnostics.Debug.WriteLine($"EROARE la Cancel GET (ID: {id}): {ex}");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /TourGuide/Cancel/5
        [HttpPost, ActionName("CancelConfirmed")] // Schimbat ActionName pentru claritate
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelConfirmed(int id) // Renumit parametrul din form
        {
            // --- Folosește metoda sincronă ---
            int currentGuideId = GetCurrentGuideId();
            if (currentGuideId <= 0) { TempData["ErrorMessage"] = "ID Ghid invalid (hardcodat)."; return RedirectToAction("Index", "Home"); }

            try
            {
                var tour = await _context.Tours.FindAsync(id);
                if (tour == null) return NotFound();

                // Verificare proprietate (poate fi comentată pentru testare)
                if (tour.IdTourGuide != currentGuideId)
                {
                    TempData["WarningMessage"] = $"Atenție: Încercați să anulați turul {id} care aparține ghidului {tour.IdTourGuide}.";
                    // return Forbid();
                }

                // Re-verificare dacă poate fi anulat
                if (tour.DateTour < DateOnly.FromDateTime(DateTime.Today))
                {
                    TempData["ErrorMessage"] = "Acest tur nu mai poate fi anulat.";
                    return RedirectToAction(nameof(Index));
                }

                // Ștergere pentru testare (sau adaugă logica cu Status dacă o implementezi)
                _context.Tours.Remove(tour);
                TempData["SuccessMessage"] = "Turul a fost șters (anulat) cu succes.";

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbEx)
            {
                System.Diagnostics.Debug.WriteLine($"EROARE DbUpdateException la anulare tur: {dbEx} --- Inner: {dbEx.InnerException}");
                TempData["ErrorMessage"] = $"Eroare la anularea în baza de date: {dbEx.InnerException?.Message ?? dbEx.Message}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "A apărut o eroare la anularea turului.";
                System.Diagnostics.Debug.WriteLine($"EROARE Exception la anulare tur: {ex}");
                return RedirectToAction(nameof(Index));
            }
        }




        // GET: /TourGuide/ViewExhibits
        public async Task<IActionResult> ViewExhibits(string searchTerm, int? sectionId)
        {
            try
            {
                var query = _context.Exhibits.Include(e => e.Section).AsQueryable();

                if (sectionId.HasValue)
                {
                    query = query.Where(e => e.IdSection == sectionId.Value);
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(e =>
                       (e.NameExhibit != null && e.NameExhibit.Contains(searchTerm)) ||
                       (e.Description != null && e.Description.Contains(searchTerm))
                    );
                }

                var exhibits = await query.OrderBy(e => e.NameExhibit).ToListAsync();
                // Asigură-te că ai un View numit "ViewExhibits.cshtml" în Views/TourGuide
                return View("ViewExhibits", exhibits);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "A apărut o eroare la încărcarea exponatelor.";
                System.Diagnostics.Debug.WriteLine($"EROARE la ViewExhibits: {ex}");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /TourGuide/RequestFeedback/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestFeedback(int tourId)
        {
            // --- Folosește metoda sincronă ---
            int currentGuideId = GetCurrentGuideId();
            if (currentGuideId <= 0) { TempData["ErrorMessage"] = "ID Ghid invalid (hardcodat)."; return RedirectToAction(nameof(Index)); }

            try
            {
                var tourExists = await _context.Tours.AnyAsync(t => t.IdTour == tourId && t.IdTourGuide == currentGuideId);
                if (!tourExists) return NotFound($"Turul cu ID {tourId} nu a fost găsit sau nu aparține ghidului {currentGuideId}.");

                // TODO: Implementează logica specifică pentru "request feedback"

                TempData["InfoMessage"] = $"Logica pentru solicitarea feedback-ului pentru Turul {tourId} nu este încă implementată.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "A apărut o eroare la solicitarea feedback-ului.";
                System.Diagnostics.Debug.WriteLine($"EROARE la RequestFeedback: {ex}");
                return RedirectToAction(nameof(Index));
            }
        }




        // GET: /TourGuide/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("ID-ul turului nu a fost specificat.");
            }

            // --- Folosește metoda sincronă pentru ID-ul ghidului (pentru testare) ---
            int currentGuideId = GetCurrentGuideId();
            if (currentGuideId <= 0)
            {
                TempData["ErrorMessage"] = "ID Ghid invalid (hardcodat).";
                return RedirectToAction("Index", "Home"); // Sau altă gestionare
            }

            try
            {
                // Încarcă turul și include rezervările dacă vrei să afișezi informații despre ele
                var tour = await _context.Tours
                                       .Include(t => t.TourBookings) // Opțional, dacă vrei să arăți detalii despre rezervări
                                       .FirstOrDefaultAsync(m => m.IdTour == id);

                if (tour == null)
                {
                    return NotFound($"Turul cu ID {id} nu a fost găsit.");
                }

                // Verifică dacă ghidul hardcodat este proprietarul (poate fi comentat pentru testare generală)
                if (tour.IdTourGuide != currentGuideId)
                {
                    // În modul de testare, poți alege să afișezi oricum detaliile
                    // sau să returnezi Forbid() dacă vrei să testezi ownership-ul
                    TempData["WarningMessage"] = $"Atenție: Vizualizați detaliile turului {id} care aparține ghidului {tour.IdTourGuide}, nu celui curent (hardcodat {currentGuideId}).";
                    // return Forbid(); // Activează dacă vrei să restricționezi
                }

                return View(tour); // Trimite modelul 'tour' la View-ul Details.cshtml
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "A apărut o eroare la încărcarea detaliilor turului.";
                System.Diagnostics.Debug.WriteLine($"EROARE în TourGuideController/Details (ID: {id}): {ex}");
                return RedirectToAction(nameof(Index)); // Redirecționează la lista de tururi
            }
        }

        // Acțiune pentru vizualizarea profilului ghidului de tur
        public IActionResult ViewProfileTourGuide()
        {
            int? userId = HttpContext.Session.GetInt32("IdUsers");
            if (userId == null)
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("Utilizatorul nu a fost găsit.");
            }

            if (user.Role != "tour guide")
            {
                TempData["ErrorMessage"] = "Acces interzis. Nu sunteți autorizat ca ghid de tur.";
                return RedirectToAction("Login", "UsersAuth");
            }

            return View(user);
        }

        // --- Funcție Helper (rămâne la fel) ---
        private bool TourExists(int id)
        {
            return _context.Tours.Any(e => e.IdTour == id);
        }
    }
}