using System;
using System.Text.RegularExpressions;

namespace HappySitter.DAL
{
    public class ValidationUtil
    {
        public static bool IsLegalString(string input)
        {
            //THE REGEX: [a-zA-Z0-9!@#$()?:.,'+\-_/=] IS THE OLD ONE, DOES NOT LET YOU ENTER ACCENTED NAMES (LIKE FRENCH NAMES)
            //THE REGEX: [a-zA-ZÀ-ÿ0-9!@#$()?:.,'+\-_/=] IS THE NEW ONE, LETS YOU ENTER ACCENTED NAMES (LIKE FRENCH NAMES)

            foreach (char character in input)
            {
                if (character == ' ')
                    continue;
                if (!Regex.IsMatch(character.ToString(), @"[a-zA-ZÀ-ÿ0-9!@#$()?:.,'+\-_/=]"))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AreLegalCharacters(params string[] inputs)
        {
            foreach (string input in inputs)
            {
                if (!IsLegalString(input))
                {
                    return false;
                }
            }

            return true;
        }

        

        public static bool AreNull(params string[] args)
        {
            foreach (string s in args)
            {
                if (String.IsNullOrWhiteSpace(s))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool AreDate(params string[] args)
        {
            try
            {
                foreach (string s in args)
                {
                    DateTime t = Convert.ToDateTime(s);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool AreTime(params string[] args)
        {
            try
            {
                foreach (string s in args)
                {
                    TimeSpan apttime = TimeSpan.Parse(s);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

       

        public static bool IsDatePresentOrFuture(DateTime date)
        {
            return date >= DateTime.Now;
        }

        public static bool IsDateInPast(DateTime date)
        {
            return date < DateTime.Now.Date;
        }

        public static bool IsDateWithinMonths(DateTime date, int numOfMonths)
        {
            return date <= DateTime.Now.AddMonths(numOfMonths);
        }

        public static bool IsPasswordValid(string pwd)
        {
            Regex regex = new Regex("^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,16}$");

            Match match = regex.Match(pwd);

            return match.Success;
        }

        public static bool AnyAreNull(params string[] args)
        {
            foreach (var s in args)
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    return true;
                }
            }

            return false;
        }

    }


}