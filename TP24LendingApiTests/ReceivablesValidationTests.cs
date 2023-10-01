
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using TP24LendingApi.Models;

namespace TP24LendingApiTests
{
    public class ReceivablesValidationTests
    {
        [Fact]
        public void Receivables_ReferenceIsRequired()
        {
            // Arrange
            var receivable = new ReceivableForCreationDto
            {
                OpeningValue = 100,
                PaidValue = 50,
                ClosedDate = new DateTime(2023, 06, 01),
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DebtorReference = "1",
                DueDate = new DateTime(2023, 01, 02),
                IssueDate = new DateTime(2023, 01, 01)
            };
            var validations = new Collection<ValidationResult>();

            //Act
            var isValid = Validator.TryValidateObject(receivable, new ValidationContext(receivable, null, null), validations, true);

            //Assert
            Assert.False(isValid);
            Assert.Equal(1, validations?.Count);
            Assert.Equal("Reference is required.", validations?[0].ErrorMessage);
        }

        [Fact]
        public void Receivables_DueDateLesserThanIssueDate_Failure()
        {
            // Arrange
            var receivable = new ReceivableForCreationDto {
                Reference = "1",
                OpeningValue = 100,
                PaidValue = 50,
                ClosedDate = new DateTime(2023, 06, 01),
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DebtorReference = "1",
                DueDate = new DateTime(2023, 01, 01),
                IssueDate = new DateTime(2023, 01, 01)
            };
            var validations = new Collection<ValidationResult>();

            //Act
            var isValid = Validator.TryValidateObject(receivable, new ValidationContext(receivable, null, null), validations, true);

            //Assert
            Assert.False(isValid);
            Assert.Equal(1, validations?.Count);
            Assert.Equal("Date: DueDate must be greater than IssueDate.", validations?[0].ErrorMessage);
        }

        [Fact]
        public void Receivables_DueDateGreaterThanIssueDate_Success()
        {
            // Arrange
            var receivable = new ReceivableForCreationDto
            {
                Reference = "1",
                OpeningValue = 100,
                PaidValue = 50,
                ClosedDate = new DateTime(2023, 06, 01),
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DebtorReference = "1",
                DueDate = new DateTime(2023, 12, 01),
                IssueDate = new DateTime(2023, 01, 01)
            };
            var validations = new Collection<ValidationResult>();

            //Act
            var isValid = Validator.TryValidateObject(receivable, new ValidationContext(receivable, null, null), validations, true);

            //Assert
            Assert.True(isValid);
        }


        [Fact]
        public void Receivables_ClosedDateLesserThanIssueDate_Failure()
        {
            // Arrange
            var receivable = new ReceivableForCreationDto
            {
                Reference = "1",
                OpeningValue = 100,
                PaidValue = 50,
                ClosedDate = new DateTime(2023, 01, 01),
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DebtorReference = "1",
                DueDate = new DateTime(2023, 01, 10),
                IssueDate = new DateTime(2023, 01, 02)
            };
            var validations = new Collection<ValidationResult>();

            //Act
            var isValid = Validator.TryValidateObject(receivable, new ValidationContext(receivable, null, null), validations, true);

            //Assert
            Assert.False(isValid);
            Assert.Equal(1, validations?.Count);
            Assert.Equal("Date: ClosedDate must be greater than IssueDate.", validations?[0].ErrorMessage);
        }

        [Fact]
        public void Receivables_ClosedDateGreaterThanIssueDate_Success()
        {
            // Arrange
            var receivable = new ReceivableForCreationDto
            {
                Reference = "1",
                OpeningValue = 100,
                PaidValue = 50,
                ClosedDate = new DateTime(2023, 06, 01),
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DebtorReference = "1",
                DueDate = new DateTime(2023, 12, 01),
                IssueDate = new DateTime(2023, 01, 01)
            };
            var validations = new Collection<ValidationResult>();

            //Act
            var isValid = Validator.TryValidateObject(receivable, new ValidationContext(receivable, null, null), validations, true);

            //Assert
            Assert.True(isValid);
        }


        [Fact]
        public void Receivables_ValidCurrencyCode_Failure()
        {
            // Arrange
            var receivable = new ReceivableForCreationDto
            {
                Reference = "1",
                OpeningValue = 100,
                PaidValue = 50,
                ClosedDate = new DateTime(2023, 06, 01),
                CurrencyCode = "EUR123",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DebtorReference = "1",
                DueDate = new DateTime(2023, 12, 01),
                IssueDate = new DateTime(2023, 01, 01)
            };
            var validations = new Collection<ValidationResult>();

            //Act
            var isValid = Validator.TryValidateObject(receivable, new ValidationContext(receivable, null, null), validations, true);

            //Assert
            Assert.False(isValid);
            Assert.Equal(1, validations?.Count);
            Assert.Equal("Currency: EUR123 is not a valid currency code.", validations?[0].ErrorMessage);
        }

        [Fact]
        public void Receivables_ValidCurrencyCode_Success()
        {
            // Arrange
            var receivable = new ReceivableForCreationDto
            {
                Reference = "1",
                OpeningValue = 100,
                PaidValue = 50,
                ClosedDate = new DateTime(2023, 06, 01),
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DebtorReference = "1",
                DueDate = new DateTime(2023, 12, 01),
                IssueDate = new DateTime(2023, 01, 01)
            };
            var validations = new Collection<ValidationResult>();

            //Act
            var isValid = Validator.TryValidateObject(receivable, new ValidationContext(receivable, null, null), validations, true);

            //Assert
            Assert.True(isValid);
        }

        [Fact]
        public void Receivables_ValidCountryCode_Failure()
        {
            // Arrange
            var receivable = new ReceivableForCreationDto
            {
                Reference = "1",
                OpeningValue = 100,
                PaidValue = 50,
                ClosedDate = new DateTime(2023, 06, 01),
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT123",
                DebtorName = "name",
                DebtorReference = "1",
                DueDate = new DateTime(2023, 12, 01),
                IssueDate = new DateTime(2023, 01, 01)
            };
            var validations = new Collection<ValidationResult>();

            //Act
            var isValid = Validator.TryValidateObject(receivable, new ValidationContext(receivable, null, null), validations, true);

            //Assert
            Assert.False(isValid);
            Assert.Equal(1, validations?.Count);
            Assert.Equal("Country: PT123 is not a valid country code.", validations?[0].ErrorMessage);
        }

        [Fact]
        public void Receivables_ValidCountryCode_Success()
        {
            // Arrange
            var receivable = new ReceivableForCreationDto
            {
                Reference = "1",
                OpeningValue = 100,
                PaidValue = 50,
                ClosedDate = new DateTime(2023, 06, 01),
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DebtorReference = "1",
                DueDate = new DateTime(2023, 12, 01),
                IssueDate = new DateTime(2023, 01, 01)
            };
            var validations = new Collection<ValidationResult>();

            //Act
            var isValid = Validator.TryValidateObject(receivable, new ValidationContext(receivable, null, null), validations, true);

            //Assert
            Assert.True(isValid);
        }
    }
}
