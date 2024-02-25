namespace FinancePortal.Util
{
    public static class Utility
    {
        private const string IDPrefix = "INC";
        private const int number = 3256;
        public static string GenerateInvoiceId(int count)
        {
            var newNumber = number + count;
            string id = $"{IDPrefix}{newNumber}";
            return id;
        }
    }
}
