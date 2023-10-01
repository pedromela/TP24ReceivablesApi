using Microsoft.EntityFrameworkCore;
using TP24Entities;
using TP24Entities.Models;
using TP24LendingApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using TP24LendingApi.Models;

namespace TP24LendingApiTests
{
    public class ReceivablesControllerTests
    {
        ReceivablesContext _context;
        public ReceivablesControllerTests()
        {
            DbContextOptions<ReceivablesContext> dbContextOptions = new DbContextOptionsBuilder<ReceivablesContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
            _context = new ReceivablesContext(dbContextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void StoreReceivables_RequiredPropertiesException()
        {
            // Arrange
            var controller = new ReceivablesController(_context);
            var receivables = new List<Receivable> {
                new Receivable { Reference = "1", OpeningValue = 100, PaidValue = 50, ClosedDate = "2023-01-01" }
            };
            // Act and Assert
            var ex = Assert.ThrowsAsync<DbUpdateException>(async () => await controller.StoreReceivables(receivables));
            Assert.Equal("Required properties '{'CurrencyCode', 'DebtorCountryCode', 'DebtorName', 'DebtorReference', 'DueDate', 'IssueDate'}' are missing for the instance of entity type 'Receivable'. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the entity key value.", ex.Result.Message);
        }

        [Fact]
        public async void StoreReceivables_StoresSuccessfuly()
        {
            // Arrange
            var controller = new ReceivablesController(_context);
            var receivables = new List<Receivable>
            {
                new Receivable
                {
                    Reference = "1",
                    OpeningValue = 100,
                    PaidValue = 50,
                    ClosedDate = "2023-06-01",
                    CurrencyCode = "EUR",
                    DebtorCountryCode = "PT",
                    DebtorName = "name",
                    DebtorReference = "1",
                    DueDate = "2023-12-31",
                    IssueDate = "2023-01-01"
                }
            };
            // Act
            var result = await controller.StoreReceivables(receivables) as OkObjectResult;
            // Assert
            Assert.Equal(200, result?.StatusCode);
        }

        [Fact]
        public void GetSummaryStatistics_ReturnsCorrectSummary()
        {
            // Arrange
            _context.Receivables.Add(new Receivable { 
                Reference = "1",
                OpeningValue = 100,
                PaidValue = 50,
                ClosedDate = "2023-06-01",
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DebtorReference = "Debtor1",
                DueDate = "2023-12-31",
                IssueDate = "2023-01-01"
            });
            _context.Receivables.Add(new Receivable { 
                Reference = "2", 
                OpeningValue = 200, 
                PaidValue = 150,
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DebtorReference = "Debtor1",
                DueDate = "2023-12-31",
                IssueDate = "2023-01-01"
            });
            _context.SaveChanges();
            
            var controller = new ReceivablesController(_context);

            // Act
            var result = controller.GetSummaryStatistics() as ObjectResult;
            var summary = result?.Value as Summary;

            // Assert
            Assert.Equal(300, summary?.ReceivablesOpeningValue);
            Assert.Equal(200, summary?.ReceivablesPaidValue);
            Assert.Equal(200, summary?.OpenReceivablesOpeningValue);
            Assert.Equal(150, summary?.OpenReceivablesPaidValue);
            Assert.Equal(100, summary?.ClosedReceivablesOpeningValue);
            Assert.Equal(50, summary?.ClosedReceivablesPaidValue);
            Assert.Equal(0, summary?.CancelledReceivablesOpeningValue);
            Assert.Equal(0, summary?.CancelledReceivablesPaidValue);
        }

        [Fact]
        public void GetDebtorSummary_ReturnsCorrectSummary()
        {
            // Arrange
            _context.Receivables.Add(new Receivable { 
                Reference = "1", 
                DebtorReference = "Debtor1",
                OpeningValue = 100,
                PaidValue = 100,
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DueDate = "2023-12-31",
                IssueDate = "2023-01-01"
            });
            _context.Receivables.Add(new Receivable { 
                Reference = "2", 
                DebtorReference = "Debtor1",
                OpeningValue = 100,
                PaidValue = 50, 
                ClosedDate = "2023-01-01",
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DueDate = "2023-12-31",
                IssueDate = "2023-01-01"
            });
            _context.Receivables.Add(new Receivable { 
                Reference = "3", 
                DebtorReference = "Debtor2",
                OpeningValue = 200,
                PaidValue = 150,
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DueDate = "2023-12-31",
                IssueDate = "2023-01-01"
            });

            _context.Receivables.Add(new Receivable
            {
                Reference = "4",
                DebtorReference = "Debtor2",
                OpeningValue = 200,
                PaidValue = 150,
                Cancelled = true,
                CurrencyCode = "EUR",
                DebtorCountryCode = "PT",
                DebtorName = "name",
                DueDate = "2023-12-31",
                IssueDate = "2023-01-01"
            });
            _context.SaveChanges();
            var controller = new ReceivablesController(_context);

            // Act
            var result = controller.GetDebtorSummary("Debtor1") as ObjectResult;
            var summary = result?.Value as Summary;

            var result2 = controller.GetDebtorSummary("Debtor2") as ObjectResult;
            var summary2 = result2?.Value as Summary;

            // Assert
            Assert.Equal(200, summary?.ReceivablesOpeningValue);
            Assert.Equal(150, summary?.ReceivablesPaidValue);
            Assert.Equal(100, summary?.OpenReceivablesOpeningValue);
            Assert.Equal(100, summary?.OpenReceivablesPaidValue);
            Assert.Equal(100, summary?.ClosedReceivablesOpeningValue);
            Assert.Equal(50, summary?.ClosedReceivablesPaidValue);
            Assert.Equal(0, summary?.CancelledReceivablesOpeningValue);
            Assert.Equal(0, summary?.CancelledReceivablesPaidValue);

            Assert.Equal(400, summary2?.ReceivablesOpeningValue);
            Assert.Equal(300, summary2?.ReceivablesPaidValue);
            Assert.Equal(400, summary2?.OpenReceivablesOpeningValue);
            Assert.Equal(300, summary2?.OpenReceivablesPaidValue);
            Assert.Equal(0, summary2?.ClosedReceivablesOpeningValue);
            Assert.Equal(0, summary2?.ClosedReceivablesPaidValue);
            Assert.Equal(200, summary2?.CancelledReceivablesOpeningValue);
            Assert.Equal(150, summary2?.CancelledReceivablesPaidValue);
        }
    }
}