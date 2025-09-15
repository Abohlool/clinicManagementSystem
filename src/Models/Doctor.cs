//* doctor datatype

using ClinicManagementSystem.Utils;

namespace ClinicManagementSystem.Models
{
    public class Doctor : Person
    {
        public int Id {get; set;}
        public string Specialization {get; set;}
        public DateTime[] AvailableTimes = new DateTime[100];
        public int TimeCount = 0;

        public Doctor(string fname, string lname, int SS, DateOnly dob, string phoneNumber, string sex, string spec)
            : base(fname, lname, SS, dob, phoneNumber, sex) {Specialization = spec; Age = GetAge(dob);}

        public static Doctor GetInfo()
        {
            string fn = Getter.GetFirstName("Doctor");
            string ln = Getter.GetLastName("Doctor");
            int ss = Getter.GetSS("Doctor");
            DateOnly dob = Getter.GetDOB("Doctor");
            string pn = Getter.GetPhone("Doctor");
            string sx = Getter.GetSex("Doctor");

            string spec;
            do {Console.Write("Enter Doctor's Specialization: "); spec = Console.ReadLine().Trim();}
            while (string.IsNullOrWhiteSpace(spec));

            return new Doctor(fn, ln, ss, dob, pn, sx, spec);
        }

        public override void PrintInfo() 
            {Console.WriteLine($"Doctor: {FirstName}, Specialization: {Specialization}, Phone: {PhoneNumber}");}

        //* updates the doctor’s schedule
        public void AddAvailableTime(DateTime time)
        {
            for (int i = 0; i < TimeCount; i++)
            {
                if (AvailableTimes[i] == time) 
                    {Console.WriteLine("This slot is already in the schedule."); return;}
            }
            AvailableTimes[TimeCount++] = time;
            Console.WriteLine("New availability added to schedule.");
        }

        public void ShowSchedule()
        {
            if (TimeCount == 0) 
                {Console.WriteLine("No times available.");}
            else
            {
                Console.WriteLine("Schedule:");
                for (int i = 0; i < TimeCount; i++) 
                    {Console.WriteLine(" - " + AvailableTimes[i]);}
            }
        }


        //! unnecessary
        // public bool IsAvailable(DateTime time)
        // {
        //     for (int i = 0; i < TimeCount; i++) 
        //         {if (AvailableTimes[i] == time) {return true;}}
        //     return false;
        // }
    }
}
