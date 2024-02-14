using System.ComponentModel.DataAnnotations;

namespace FinancePortal.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string InvoiceId { get; set; }
        public string StudentId { get; set; }
        public short InvoiceType { get; set; }
        public double Fee { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
