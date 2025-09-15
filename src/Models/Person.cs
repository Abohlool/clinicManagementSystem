//* abstract datatype person
namespace ClinicManagementSystem.Models
{
    public abstract class Person
    {
        public int id;
        public string Name {get;}
        public string FirstName {get;}
        public string LastName {get;}
        public int SocialSecurity {get;}
        public int Age {get; set;}
        public DateOnly DOB {get;}
        public string PhoneNumber {get; set;}
        public string Sex {get; set;}

        public Person(string fname, string lname, int SS, DateOnly dob, string phoneNumber, string sex)
        {
            FirstName = fname; LastName = lname;
            SocialSecurity = SS; Age = GetAge(dob);
            DOB = dob; PhoneNumber = phoneNumber;
            Sex = sex;
            Name = $"{FirstName} {LastName}";
        }

        public abstract void PrintInfo();
        public static int GetAge(DateOnly dob)
            {return DateOnly.FromDateTime(DateTime.Today).Year - dob.Year;}
        public virtual decimal CalculateSalary() {return 0;}
    }
}

