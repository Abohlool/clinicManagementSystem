using System.Text.RegularExpressions;
using ClinicManagementSystem.Exceptions;

namespace ClinicManagementSystem.Utils
{
    public static class Validators
    {
        //* checks for invalid phone numbers
        public static void ValidatePhoneNumber(string Phone)
        {
            Regex rg = new(@"^(?:\+98|98|0)(9\d{2}) ?\d{3} ?\d{4}$");
            if (!rg.IsMatch(Phone))
            {throw new InvalidPhoneNumberException("Invalid phone number.");}
        }

        //* checks for invalid gender
        public static void ValidateGender(string Gender)
        {
            Regex rg = new(@"^(male|female|m|f)$", RegexOptions.IgnoreCase);
            if (!rg.IsMatch(Gender))
            {throw new InvalidGenderException("Invalid gender choose male or female");}
        }

        //* checks if string is all digits 
        public static bool IsAllDigits(string S)
        {
            if (S.Equals("")) {return false;}
            foreach (char c in S)
            {if (!char.IsDigit(c)) {return false;}}
            return true;
        }

        //* confirm removal
        public static bool ConfirmRemoval(string Name)
        {
            Console.Write("Enter name to confirm: ");
            string input = Console.ReadLine()?.Trim();
            return string.Equals(input, Name, StringComparison.OrdinalIgnoreCase);
        }

        public static bool ValidatePMS(string pms)
        {
            Regex rg = new(@"^(married)|(divorced)|(deceased)$", RegexOptions.IgnoreCase);
            return rg.IsMatch(pms);
        }
        public static bool ValidateMS(string ms)
        {
            // single', 'married', 'divorced', 'deceased
            Regex rg = new(@"^(single)|(married)|(divorced)|(deceased)$", RegexOptions.IgnoreCase);
            return rg.IsMatch(ms);
        }

        //! unnecessary
        // public static void ValidatePatientType(string Type)
        // {
        //     Regex rg = new(@"^child|adult", RegexOptions.IgnoreCase);
        //     if (!rg.IsMatch(Type))
        //         {throw new InvalidPatientTypeException("Invalid patient type choose child or adult");}
        // }
    }
}
