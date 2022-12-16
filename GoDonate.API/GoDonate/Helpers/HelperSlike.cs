namespace GoDonate.Helpers
{
    public static class HelperSlike
    {
        public static byte[] ParsirajUbase(string slika)
        {
            return System.Convert.FromBase64String( slika);
        }
    }
}
