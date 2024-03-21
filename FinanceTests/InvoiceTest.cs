

namespace FinanceTests
{
    public class InvoiceTest
    {
        [Fact]
        public void OnPost_WithValidInvoiceId_ShouldUpdateInvoiceAndRedirectToInvoiceDetailPage()
        {
            // Arrange
            var invoiceId = "123";
            var invoice = new Invoice { InvoiceId = invoiceId }; // Create a sample invoice object

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Invoice).Returns(MockDbSet(new List<Invoice> { invoice }.AsQueryable()));

            var model = new InvoiceDetailModel(mockContext.Object);

            // Act
            var result = model.OnPost(invoiceId);

            // Assert
            Assert.IsType<RedirectToPageResult>(result);

            var redirectResult = result as RedirectToPageResult;
            Assert.Equal("/InvoiceDetail", redirectResult.PageName);
            Assert.Equal(invoiceId, redirectResult.RouteValues["invoiceId"]);

            Assert.Equal(FinancePortal.Util.InvoicePaymentStatus.Paid, invoice.IsPaid);
            Assert.NotNull(invoice.PaymentDate);
        }

        [Fact]
        public void OnPost_WithInvalidInvoiceId_ShouldReturnNotFound()
        {
            // Arrange
            var invoiceId = "123";

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Invoice).Returns(MockDbSet(new List<Invoice>().AsQueryable()));

            var model = new InvoiceDetailModel(mockContext.Object);

            // Act
            var result = model.OnPost(invoiceId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Helper method to mock DbSet
        private DbSet<T> MockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }

    }
}