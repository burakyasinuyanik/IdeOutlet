using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Utility
{
    public static class Helper
    {

        public static string RandomStringGenerator(int length)
        {
            string randomString = "";
            char[] upperCase = "QWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray();
            char[] lowerCase = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();
            char[] number = "1234567890".ToCharArray();
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {

                int rndChoice = rnd.Next(1, 4);

                if (rndChoice == 1)
                {
                    int upperCaseRnd = rnd.Next(0, upperCase.Length);
                    randomString += upperCase[upperCaseRnd];
                }
                else if (rndChoice == 2)
                {
                    int lowerCaseRnd = rnd.Next(0, lowerCase.Length);
                    randomString += lowerCase[lowerCaseRnd];
                }
                else
                {
                    int numberRnd = rnd.Next(0, number.Length);
                    randomString += number[numberRnd];

                }



            }


            return randomString;
        }

        public static string TextClean(this string text)
        {
            string s = text;
            s = s.Replace(' ', '-');
            s = s.Replace('ğ', 'g');
            s = s.Replace('ü', 'u');
            s = s.Replace('ı', 'i');
            s = s.Replace('ş', 's');
            s = s.Replace('ö', 'o');
            s = s.Replace('ç', 'c');
            s = s.Replace('İ', 'I');
            s = s.Replace('Ğ', 'G');
            s = s.Replace('Ü', 'U');
            s = s.Replace('Ş', 'S');
            s = s.Replace('Ö', 'O');
            s = s.Replace('Ç', 'C');
            s = s.Replace("$", String.Empty);
            s = s.Replace("*", String.Empty);
            s = s.Replace("/", String.Empty);
            s = s.Replace("\\", String.Empty);
            s = s.Replace("|", String.Empty);
            s = s.Replace("^", String.Empty);
            s = s.Replace("+", String.Empty);
            s = s.Replace("}", String.Empty);
            s = s.Replace("{", String.Empty);
            s = s.Replace("(", String.Empty);
            s = s.Replace(")", String.Empty);
            s = s.Replace("[", String.Empty);
            s = s.Replace("]", String.Empty);
            s = s.Replace("&", String.Empty);
            s = s.Replace("%", String.Empty);
            s = s.Replace("#", String.Empty);
            s = s.Replace("'", String.Empty);
            s = s.Replace("!", String.Empty);
            s = s.Replace("<", String.Empty);
            s = s.Replace(">", String.Empty);
            s = s.Replace(";", String.Empty);
            s = s.Replace(",", String.Empty);
            s = s.Replace("@", String.Empty);
            s = s.Replace("æ", String.Empty);
            s = s.Replace("ß", String.Empty);
            return s;

        }

        public static int ToInt(this object text)
        {

            return Convert.ToInt32(text);
        }

        public static string ToTitleCase(this string text)
        {
            string deneme = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text.ToLower());
            return deneme;

        }

    }
}

