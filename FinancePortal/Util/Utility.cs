namespace FinancePortal.Util
{
    public class Utility
    {
        private static string IDPrefix = "INC";

        public static string GenerateInvoiceId(int counter)
        {
            string id = $"{IDPrefix}{counter:D4}";
            return id;
        }
    }
}
