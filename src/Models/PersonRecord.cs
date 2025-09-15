//* PersonRecord data type 
namespace ClinicManagementSystem.Models
{
    public class PersonRecord
    {
        public int Id {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public DateOnly DOB {get; set;}
        public int Age
            {get {return DateOnly.FromDateTime(DateTime.Today).Year - DOB.Year;}}
        public string PhoneNumber {get; set;}
        public string Sex {get; set;}
    }
}
