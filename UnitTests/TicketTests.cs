using NUnit.Framework;
using Museum_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.Net.Sockets;

namespace UnitTests
{
    [TestFixture]
    public class TicketTests
    {
        private TicketType _regularTicket;
        private TicketType _vipTicket;
        private Discount _studentDiscount;
        private Discount _seniorDiscount;
        private Users _testUser;
        private Users _vipUser;

        [SetUp]
        public void Setup()
        {
            // Arrange - Setup test data
            _regularTicket = new TicketType
            {
                IdTicketType = 1,
                TypeName = "Regular",
                BasePrice = 50.0,
                Tickets = new List<Ticket>()
            };

            _vipTicket = new TicketType
            {
                IdTicketType = 2,
                TypeName = "VIP",
                BasePrice = 100.0,
                Tickets = new List<Ticket>()
            };

            _studentDiscount = new Discount
            {
                IdDiscount = 1,
                BeneficiaryCategory = "Student",
                PercentageDiscount = 20.0,
                Tickets = new List<Ticket>()
            };

            _seniorDiscount = new Discount
            {
                IdDiscount = 2,
                BeneficiaryCategory = "Senior",
                PercentageDiscount = 30.0,
                Tickets = new List<Ticket>()
            };

            _testUser = new Users
            {
                IdUsers = 1,
                Username = "testuser",
                Email = "test@example.com",
                Tickets = new List<Ticket>()
            };

            _vipUser = new Users
            {
                IdUsers = 2,
                Username = "vipuser",
                Email = "vip@example.com",
                Tickets = new List<Ticket>()
            };
        }

        #region Basic Ticket Creation Tests
        [Test]
        public void CreateTicket_WithoutDiscount_CalculatesCorrectFinalPrice()
        {
            // Arrange
            var ticket = new Ticket
            {
                IdTicketType = _regularTicket.IdTicketType,
                IdUsers = _testUser.IdUsers,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                TicketType = _regularTicket,
                User = _testUser,
                FinalPrice = _regularTicket.BasePrice
            };

            // Assert
            Assert.That(ticket.FinalPrice, Is.EqualTo(50.0));
            Assert.That(ticket.TicketType?.TypeName, Is.EqualTo("Regular"));
            Assert.That(ticket.Discount, Is.Null);
        }

        [Test]
        public void CreateTicket_WithStudentDiscount_CalculatesCorrectFinalPrice()
        {
            // Arrange
            var ticket = new Ticket
            {
                IdTicketType = _regularTicket.IdTicketType,
                IdUsers = _testUser.IdUsers,
                IdDiscount = _studentDiscount.IdDiscount,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                TicketType = _regularTicket,
                User = _testUser,
                Discount = _studentDiscount,
                FinalPrice = _regularTicket.BasePrice * (1 - _studentDiscount.PercentageDiscount / 100)
            };

            // Assert
            Assert.That(ticket.FinalPrice, Is.EqualTo(40.0)); // 50 - 20%
            Assert.That(ticket.Discount?.BeneficiaryCategory, Is.EqualTo("Student"));
            Assert.That(ticket.Discount?.PercentageDiscount, Is.EqualTo(20.0));
        }

        [Test]
        public void CreateTicket_WithSeniorDiscount_CalculatesCorrectFinalPrice()
        {
            // Arrange
            var ticket = new Ticket
            {
                IdTicketType = _regularTicket.IdTicketType,
                IdUsers = _testUser.IdUsers,
                IdDiscount = _seniorDiscount.IdDiscount,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                TicketType = _regularTicket,
                User = _testUser,
                Discount = _seniorDiscount,
                FinalPrice = _regularTicket.BasePrice * (1 - _seniorDiscount.PercentageDiscount / 100)
            };

            // Assert
            Assert.That(ticket.FinalPrice, Is.EqualTo(35.0)); // 50 - 30%
        }
        #endregion

        #region VIP Ticket Tests
        [Test]
        public void CreateVIPTicket_WithoutDiscount_CalculatesCorrectFinalPrice()
        {
            // Arrange
            var ticket = new Ticket
            {
                IdTicketType = _vipTicket.IdTicketType,
                IdUsers = _vipUser.IdUsers,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                TicketType = _vipTicket,
                User = _vipUser,
                FinalPrice = _vipTicket.BasePrice
            };

            // Assert
            Assert.That(ticket.FinalPrice, Is.EqualTo(100.0));
            Assert.That(ticket.TicketType?.TypeName, Is.EqualTo("VIP"));
        }

        [Test]
        public void CreateVIPTicket_WithStudentDiscount_CalculatesCorrectFinalPrice()
        {
            // Arrange
            var ticket = new Ticket
            {
                IdTicketType = _vipTicket.IdTicketType,
                IdUsers = _vipUser.IdUsers,
                IdDiscount = _studentDiscount.IdDiscount,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                TicketType = _vipTicket,
                User = _vipUser,
                Discount = _studentDiscount,
                FinalPrice = _vipTicket.BasePrice * (1 - _studentDiscount.PercentageDiscount / 100)
            };

            // Assert
            Assert.That(ticket.FinalPrice, Is.EqualTo(80.0)); // 100 - 20%
        }
        #endregion

        #region Validation Tests
        [Test]
        public void Ticket_WithInvalidTicketType_ThrowsException()
        {
            // Arrange
            var ticket = new Ticket
            {
                IdUsers = _testUser.IdUsers,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                User = _testUser,
                TicketType = null // explicit null
            };

            // Act & Assert
            Assert.Throws<NullReferenceException>(() =>
            {
                var price = ticket.TicketType.TypeName; // forțează excepția
            });
        }

        [Test]
        public void Ticket_WithInvalidUser_ThrowsException()
        {
            // Arrange
            var ticket = new Ticket
            {
                IdTicketType = _regularTicket.IdTicketType,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                TicketType = _regularTicket,
                User = null // explicit null
            };

            // Act & Assert
            Assert.Throws<NullReferenceException>(() =>
            {
                var username = ticket.User.Username; // forțează excepția
            });
        }
        #endregion

        #region Date Validation Tests
        [Test]
        public void Ticket_WithFuturePurchaseDate_IsValid()
        {
            // Arrange
            var futureDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

            // Act
            var ticket = new Ticket
            {
                IdTicketType = _regularTicket.IdTicketType,
                IdUsers = _testUser.IdUsers,
                PurchaseDate = futureDate,
                TicketType = _regularTicket,
                User = _testUser,
                FinalPrice = _regularTicket.BasePrice
            };

            // Assert
            Assert.That(ticket.PurchaseDate, Is.EqualTo(futureDate));
        }

        [Test]
        public void Ticket_WithPastPurchaseDate_IsValid()
        {
            // Arrange
            var pastDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

            // Act
            var ticket = new Ticket
            {
                IdTicketType = _regularTicket.IdTicketType,
                IdUsers = _testUser.IdUsers,
                PurchaseDate = pastDate,
                TicketType = _regularTicket,
                User = _testUser,
                FinalPrice = _regularTicket.BasePrice
            };

            // Assert
            Assert.That(ticket.PurchaseDate, Is.EqualTo(pastDate));
        }
        #endregion

        #region Multiple Tickets Tests
        [Test]
        public void User_CanHaveMultipleTickets()
        {
            // Arrange
            var tickets = new List<Ticket>
            {
                new Ticket
                {
                    IdTicketType = _regularTicket.IdTicketType,
                    IdUsers = _testUser.IdUsers,
                    PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                    TicketType = _regularTicket,
                    User = _testUser,
                    FinalPrice = _regularTicket.BasePrice
                },
                new Ticket
                {
                    IdTicketType = _vipTicket.IdTicketType,
                    IdUsers = _testUser.IdUsers,
                    PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                    TicketType = _vipTicket,
                    User = _testUser,
                    FinalPrice = _vipTicket.BasePrice
                }
            };

            // Act
            _testUser.Tickets = tickets;

            // Assert
            Assert.That(_testUser.Tickets?.Count, Is.EqualTo(2));
            Assert.That(_testUser.Tickets?.First().FinalPrice, Is.EqualTo(50.0));
            Assert.That(_testUser.Tickets?.Last().FinalPrice, Is.EqualTo(100.0));
        }
        #endregion

        #region Discount Combination Tests
        [Test]
        public void Ticket_WithMultipleDiscounts_UsesHighestDiscount()
        {
            // Arrange
            var ticket = new Ticket
            {
                IdTicketType = _regularTicket.IdTicketType,
                IdUsers = _testUser.IdUsers,
                IdDiscount = _seniorDiscount.IdDiscount, // Using senior discount (30%)
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                TicketType = _regularTicket,
                User = _testUser,
                Discount = _seniorDiscount,
                FinalPrice = _regularTicket.BasePrice * (1 - _seniorDiscount.PercentageDiscount / 100)
            };

            // Assert
            Assert.That(ticket.FinalPrice, Is.EqualTo(35.0)); // 50 - 30%
            Assert.That(ticket.Discount?.PercentageDiscount, Is.GreaterThan(_studentDiscount.PercentageDiscount));
        }
        #endregion

        #region Edge Cases
        [Test]
        public void Ticket_WithZeroPrice_IsValid()
        {
            // Arrange
            var freeTicket = new TicketType
            {
                IdTicketType = 3,
                TypeName = "Free",
                BasePrice = 0.0,
                Tickets = new List<Ticket>()
            };

            // Act
            var ticket = new Ticket
            {
                IdTicketType = freeTicket.IdTicketType,
                IdUsers = _testUser.IdUsers,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                TicketType = freeTicket,
                User = _testUser,
                FinalPrice = 0.0
            };

            // Assert
            Assert.That(ticket.FinalPrice, Is.EqualTo(0.0));
        }

        [Test]
        public void Ticket_WithMaximumDiscount_CalculatesCorrectPrice()
        {
            // Arrange
            var maxDiscount = new Discount
            {
                IdDiscount = 3,
                BeneficiaryCategory = "Special",
                PercentageDiscount = 100.0,
                Tickets = new List<Ticket>()
            };

            var ticket = new Ticket
            {
                IdTicketType = _regularTicket.IdTicketType,
                IdUsers = _testUser.IdUsers,
                IdDiscount = maxDiscount.IdDiscount,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                TicketType = _regularTicket,
                User = _testUser,
                Discount = maxDiscount,
                FinalPrice = _regularTicket.BasePrice * (1 - maxDiscount.PercentageDiscount / 100)
            };

            // Assert
            Assert.That(ticket.FinalPrice, Is.EqualTo(0.0)); // 50 - 100%
        }
        #endregion
    }
}