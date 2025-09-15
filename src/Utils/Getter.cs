using ClinicManagementSystem.Exceptions;

//* getter functions
namespace ClinicManagementSystem.Utils
{
    public static class Getter
    {
        //* get phone number
        public static string GetPhone(string type)
        {
            while (true)
            {
                try
                {
                    Console.Write($"Enter {type}s Phone Number: ");
                    string phone = Console.ReadLine().Trim();
                    Validators.ValidatePhoneNumber(phone);
                    return phone;
                }
                catch (InvalidPhoneNumberException ex)
                { Console.WriteLine("Error: " + ex.Message); }
            }
        }

        //! unnecessary
        // public static int GetAge(string type)
        // {
        //     while (true)
        //     {
        //         try
        //         {
        //             Console.Write($"Enter {type}s Age: ");
        //             int age = Convert.ToInt32(Console.ReadLine().Trim());
        //             if (age < 0 || age > 120)
        //             { throw new Exception("Invalid age"); }
        //             return age;
        //         }
        //         catch (FormatException)
        //         { Console.WriteLine("Error: Enter a number e.g. 28"); }

        //         catch (Exception ex)
        //         { Console.WriteLine("Error: " + ex.Message); }
        //     }
        // }

        //* get sex
        public static string GetSex(string type)
        {
            while (true)
            {
                try
                {
                    Console.Write($"Enter {type}s Sex: ");
                    string sex = Console.ReadLine().Trim().ToLower();
                    Validators.ValidateGender(sex);
                    return "male".Contains(sex) ? "male" : "female";
                }
                catch (InvalidGenderException ex)
                { Console.WriteLine("Error: " + ex.Message); }
            }
        }

        //* get social security
        public static int GetSS(string type)
        {
            while (true)
            {
                Console.Write($"Enter {type}s Social Security Number: ");
                string SS = Console.ReadLine().Trim();
                if (Validators.IsAllDigits(SS)) {return Convert.ToInt32(SS);}
                Console.WriteLine("Error: ID must contain only digits.");
            }
        }

        //* get first name
        public static string GetFirstName(string type)
        {
            string fname;
            do
            {
                Console.Write($"Enter {type}'s First Name: ");
                fname = Console.ReadLine().Trim();
            }
            while (string.IsNullOrWhiteSpace(fname));
            return fname;
        }

        //* get last name
        public static string GetLastName(string type)
        {
            string lname;
            do
            {
                Console.Write($"Enter {type}'s Last Name: ");
                lname = Console.ReadLine().Trim();
            }
            while (string.IsNullOrWhiteSpace(lname));
            return lname;
        }

        //* get date of birth
        public static DateOnly GetDOB(string type)
        {
            while (true)
            {
                Console.Write($"Enter {type}'s Date of Birth (yyyy-MM-dd): ");
                string? input = Console.ReadLine().Trim();

                if (DateOnly.TryParseExact(input, "yyyy-MM-dd", out DateOnly dob))
                    {return dob;}
                Console.WriteLine("Error: Please enter a valid date in format yyyy-MM-dd.");
            }
        }
    }
}

