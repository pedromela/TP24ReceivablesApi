using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP24LendingApi.Controllers;
using TP24LendingApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace TP24LendingApiTests
{
    public class ReceivablesValidationTests
    {
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
