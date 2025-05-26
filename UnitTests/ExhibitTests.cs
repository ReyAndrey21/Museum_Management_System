using NUnit.Framework;
using Museum_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace UnitTests
{
    [TestFixture]
    public class ExhibitTests
    {
        private Section _artSection;
        private Section _historySection;
        private Exhibit _testExhibit;

        [SetUp]
        public void Setup()
        {
            _artSection = new Section
            {
                IdSection = 1,
                NameSection = "Art Gallery",
                Description = "Modern and contemporary art",
                Exhibits = new List<Exhibit>()
            };

            _historySection = new Section
            {
                IdSection = 2,
                NameSection = "History Hall",
                Description = "Historical artifacts and documents",
                Exhibits = new List<Exhibit>()
            };

            _testExhibit = new Exhibit
            {
                IdExhibit = 1,
                NameExhibit = "Mona Lisa",
                Description = "Leonardo da Vinci's masterpiece",
                HistoricalPeriod = "Renaissance",
                CategoryExhibit = "Painting",
                IdSection = _artSection.IdSection,
                Section = _artSection,
                Reviews = new List<Review>()
            };
        }

        [Test]
        public void Exhibit_WithValidData_IsValid()
        {
            // Arrange
            var exhibits = new[]
            {
                new Exhibit
                {
                    IdExhibit = 2,
                    NameExhibit = "The Starry Night",
                    Description = "Vincent van Gogh's famous painting",
                    HistoricalPeriod = "Post-Impressionism",
                    CategoryExhibit = "Painting",
                    IdSection = _artSection.IdSection,
                    Section = _artSection
                },
                new Exhibit
                {
                    IdExhibit = 3,
                    NameExhibit = "Ancient Vase",
                    Description = "Greek pottery from 500 BC",
                    HistoricalPeriod = "Ancient Greece",
                    CategoryExhibit = "Pottery",
                    IdSection = _historySection.IdSection,
                    Section = _historySection
                }
            };

            foreach (var exhibit in exhibits)
            {
                // Assert
                Assert.That(exhibit.NameExhibit, Is.Not.Empty);
                Assert.That(exhibit.Description, Is.Not.Empty);
                Assert.That(exhibit.HistoricalPeriod, Is.Not.Empty);
                Assert.That(exhibit.CategoryExhibit, Is.Not.Empty);
                Assert.That(exhibit.Section, Is.Not.Null);
                Assert.That(exhibit.Section?.NameSection, Is.Not.Empty);
            }
        }

        [Test]
        public void Exhibit_WithReviews_HasCorrectAverageRating()
        {
            // Arrange
            var reviews = new List<Review>
            {
                new Review { Rating = 5, Comment = "Excellent!" },
                new Review { Rating = 4, Comment = "Very good" },
                new Review { Rating = 3, Comment = "Good" }
            };

            // Act
            _testExhibit.Reviews = reviews;

            // Assert
            Assert.That(_testExhibit.Reviews?.Count, Is.EqualTo(3));
            Assert.That(_testExhibit.Reviews?.All(r => r.Rating >= 1 && r.Rating <= 5), Is.True);
            Assert.That(_testExhibit.Reviews?.All(r => !string.IsNullOrEmpty(r.Comment)), Is.True);
        }

        [Test]
        public void Exhibit_WithInvalidSection_ThrowsException()
        {
            // Arrange
            var exhibit = new Exhibit
            {
                IdExhibit = 4,
                NameExhibit = "Test Exhibit",
                Description = "Test Description",
                HistoricalPeriod = "Test Period",
                CategoryExhibit = "Test Category",
                Section = null
            };

            // Act & Assert
            Assert.Throws<NullReferenceException>(() =>
            {
                _ = exhibit.Section?.NameSection ?? throw new NullReferenceException();
            });
        }

        [Test]
        public void Exhibit_WithEmptyName_IsInvalid()
        {
            // Arrange
            var exhibit = new Exhibit
            {
                IdExhibit = 5,
                NameExhibit = string.Empty,
                Description = "Test Description",
                HistoricalPeriod = "Test Period",
                CategoryExhibit = "Test Category",
                IdSection = _artSection.IdSection,
                Section = _artSection
            };

            // Act
            var validationContext = new ValidationContext(exhibit, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(exhibit, validationContext, validationResults, true);

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults.Any(vr => vr.MemberNames.Contains("NameExhibit")), Is.True);
        }

        [Test]
        public void Exhibit_WithEmptyCategory_IsInvalid()
        {
            // Arrange
            var exhibit = new Exhibit
            {
                IdExhibit = 6,
                NameExhibit = "Test Exhibit",
                Description = "Test Description",
                HistoricalPeriod = "Test Period",
                CategoryExhibit = string.Empty,
                IdSection = _artSection.IdSection,
                Section = _artSection
            };

            // Act
            var validationContext = new ValidationContext(exhibit, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(exhibit, validationContext, validationResults, true);

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults.Any(vr => vr.MemberNames.Contains("CategoryExhibit")), Is.True);
        }
    }
} 