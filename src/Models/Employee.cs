//* employee datatype

namespace ClinicManagementSystem.Models
{
    public class Employee : Person
    {
        public decimal MonthlySalary = 0;
        public Employee(string fname, string lname, int SS, DateOnly dob, string phoneNumber, string sex, decimal salary)
            : base(fname, lname, SS, dob, phoneNumber, sex) {MonthlySalary = salary;}

        public override void PrintInfo() 
            {Console.WriteLine($"Employee: {FirstName}, Phone: {PhoneNumber}, Salary: {MonthlySalary}");}

        public override decimal CalculateSalary() {return MonthlySalary;}
    }
}
