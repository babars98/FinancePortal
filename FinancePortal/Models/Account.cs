using System.ComponentModel.DataAnnotations;

namespace FinancePortal.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
