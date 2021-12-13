namespace ExtensionsWebApi.CustomExtensions
{
    public static class CustomExtensions
    {
        public static string GetShortName(this string name)// İsmi kısaltılmış olarak geri döndüren extension
        {
            string result = name.Substring(0, 1) + ". ";
            return result;
        }

        public static string getProtectedPhoneNumber(this string value)//numarayı sansürler(son 4 hane hariç)
        {
            string result = "*******" + value.Substring(0, 4);
            return value;
        }
        public static string getProtectedEmail(this string value)//emaili sansürler (ilk 4 karakter hariç)
        {
            string result = value.Substring(0, 4) + "********";
            return result;
        }

    }
}