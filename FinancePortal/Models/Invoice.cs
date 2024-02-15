using FinancePortal.Util;
using System.ComponentModel.DataAnnotations;

namespace FinancePortal.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string InvoiceId { get; set; }
        public string StudentId { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public double Fee { get; set; }
        public DateTime? DueDate { get; set; }
        public InvoicePaymentStatus IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
