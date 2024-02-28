using FinancePortal.Data;
using FinancePortal.Models;
using FinancePortal.Util;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FinancePortal.API
{
    public static class FinanceAPI
    {
        /// <summary>
        /// Extention method for creating minimal APIs
        /// Map this method to main class to run this api
        /// The context is also passed as dependency injection
        /// </summary>
        /// <param name="app"></param>
        public static void MapAPIEndPoint(this WebApplication app)
        {
            //API end point to create user account 
            app.MapPost("/api/CreateAccount", ([FromBody] Student student, ApplicationDbContext context) =>
            {
                var user = context.Account.FirstOrDefault(c => c.StudentId == student.StudentId);

                if (user != null)
                    return Results.BadRequest("StudentID already Exists");

                var newUser = new Account()
                {
                    StudentId = student.StudentId,
                    Password = "000000",
                    DateCreated = DateTime.Now
                };

                context.Account.Add(newUser);
                context.SaveChanges();

                return Results.Ok("Account Created");

            });


            //Create invoice API
            app.MapPost("/api/CreateInvoice", (Invoice invoice, ApplicationDbContext context) =>
            {
                var InvoiceCount = context.Invoice.Count();
                var newInvoice = new Invoice()
                {
                    InvoiceId = Utility.GenerateInvoiceId(InvoiceCount),
                    StudentId = invoice.StudentId,
                    Fee = invoice.Fee,
                    InvoiceType = invoice.InvoiceType,
                    IsPaid = InvoicePaymentStatus.OutStanding,
                    DueDate = invoice.DueDate
                };

                context.Invoice.Add(newInvoice);
                context.SaveChanges();

                return Results.Ok("Invoice created");
            });

            //Get all studnet Pending Invoices
            app.MapGet("/api/GetStudentInvoices", (string studentId, ApplicationDbContext context) =>
            {
                var invoices = context.Invoice.Where(c => c.StudentId == studentId).ToList();

                return Results.Ok(invoices);
            });

            //Check if a studnet has any pending invoice or not
            app.MapGet("/api/CheckPendingInvoice", (string studentId, ApplicationDbContext context) =>
            {
                var invoices = context.Invoice.Where(c => c.StudentId == studentId && c.IsPaid == InvoicePaymentStatus.OutStanding).ToList();

                bool isPending = true;

                if (invoices == null || invoices.Count == 0)
                    isPending = false;

                return Results.Ok(isPending);
            });

            //Get Invoice by InvoiceId
            app.MapGet("/api/GetInvoice", (string invoiceId, ApplicationDbContext context) =>
            {
                var invoice = context.Invoice.FirstOrDefault(c => c.InvoiceId == invoiceId);

                if (invoice == null)
                    return Results.NotFound();

                return Results.Ok(invoice);

            });

            //Pay Invoice

            app.MapPut("/api/PayInvoice", (string invoiceId, ApplicationDbContext context) => 
            {
                var invoice = context.Invoice.FirstOrDefault(c => c.InvoiceId == invoiceId);

                if (invoice == null)
                    return Results.NotFound();

                invoice.IsPaid = InvoicePaymentStatus.Paid;
                invoice.PaymentDate = DateTime.Now;

                context.SaveChanges();

                return Results.Ok();

            });

        }
    }
}
