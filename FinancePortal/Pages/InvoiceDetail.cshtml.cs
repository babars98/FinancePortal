using FinancePortal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinancePortal.Pages
{
    public class InvoiceDetailModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public string invoiceId { get; set; }

        public InvoiceDetailModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(string invoiceId)
        {
            var invoice = _context.Invoice.FirstOrDefault(c => c.InvoiceId == invoiceId);

            ViewData["invoice"] = invoice;
        }

        public IActionResult OnPost(string invoiceId)
        {
            var invoice = _context.Invoice.FirstOrDefault(c => c.InvoiceId == invoiceId);

            if (invoice == null)
                return NotFound();

            invoice.IsPaid = Util.InvoicePaymentStatus.Paid;
            invoice.PaymentDate = DateTime.Now;

            _context.SaveChanges();

            return RedirectToPage("/InvoiceDetail", new {invoiceId = invoiceId});
        }
    }
}
