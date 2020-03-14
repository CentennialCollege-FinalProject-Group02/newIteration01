using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappySitter.Utils
{
    public class StringUtils
    {
        public StringUtils()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static String CombineStrings(String left, String right, String link)
        {
            if (left.Trim().Length > 0 && right.Trim().Length > 0)
            {
                return left + link + right;
            }
            else
            {
                return (left.Trim().Length > 0 ? left : right);
            }
        }

        public static String SubIfEmpty(String str, String sub)
        {
            if (str.Trim().Length > 0)
                return str.Trim();
            else
                return sub;
        }

        public String ExtractNumber(String strno)
        {
            String clean = "";
            foreach (Char c in strno.Trim().ToCharArray())
            {
                if (Char.IsDigit(c))
                {
                    clean += c.ToString();
                }
            }
            if (clean.Length < 1)
            {
                return "00000";
            }
            else
            {
                return Int32.Parse(clean).ToString("00000");
            }
        }

        public static string CapitalizeFirstLetter(string txt)
        {
            if (String.IsNullOrWhiteSpace(txt))
            {
                return txt;
            }
            return txt[0].ToString().ToUpper() + txt.Substring(1);
        }

        public static string CapitalizeName(string txt)
        {
            if (String.IsNullOrWhiteSpace(txt))
            {
                return txt;
            }
            string[] arr = txt.Trim().Split(' ');
            string result = "";
            foreach (string s in arr)
            {
                result += CapitalizeFirstLetter(s) + " ";
            }

            return result.Trim();
        }

        public static string Stringify(List<string> list, string connector)
        {
            string str = "";

            foreach (string s in list)
            {
                str = CombineStrings(str, s, connector);
            }

            return str;
        }

        public static string GetFirstName(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return "";
            string n = name.Trim();
            return n.Substring(0, n.IndexOf(' '));
        }

        public static string SplitCamel(string s)
        {
            String str = "";
            for (int i = 0; i < s.Length; i++)
            {
                string c = s.Substring(i, 1);
                if (c == c.ToUpper())
                {
                    str += " " + c;
                }
                else
                {
                    str += c;
                }
            }
            return str.Trim();
        }
        public static String Clean(String s)
        {
            return s.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", "");
        }

        public static string CleanUserName(string userName)
        {
            int index = userName.IndexOf("@");
            if (index > 0)
            {
                return userName.Substring(0, index);
            }

            return userName;
        }

        public static bool Contains(string source, string toCheck, StringComparison comp)
        {
            bool isFound = false;

            //string[] toCheckArryay = toCheck.Split(new string[] { "," }, StringSplitOptions.None);
            string[] arr = toCheck.Split(',');

            foreach (var item in arr)
            {
                if (source?.IndexOf(item, comp) >= 0)
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;
        }

        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Invalid arg");
            }

            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        // convert from 1 to 20.
        public static string ConvertNumToOrdinal(int number)
        {
            var dict = new Dictionary<int, string>();

            //dict.Add(1, "First");
            //dict.Add(2, "Second");
            //dict.Add(3, "Third");
            //dict.Add(4, "Fourth");
            //dict.Add(5, "Fifth");
            //dict.Add(6, "Sixth");
            //dict.Add(7, "Seventh");
            //dict.Add(8, "Eighth");
            //dict.Add(9, "Ninth");
            //dict.Add(10, "Tenth");
            //dict.Add(11, "Eleventh");
            //dict.Add(12, "Twelfth");
            //dict.Add(13, "Thirteenth");
            //dict.Add(14, "Fourteenth");
            //dict.Add(15, "Fifteenth");
            //dict.Add(16, "Sixteenth");
            //dict.Add(17, "Seventeenth");
            //dict.Add(18, "Eighteenth");
            //dict.Add(19, "Nineteenth");
            //dict.Add(20, "Twentieth");

            //if (dict.ContainsKey(number))
            //{
            //    return dict[number];
            //}
            //else
            //{
            //    return number.ToString();
            //}

            return "Service Area " + number;

        }

    }
}