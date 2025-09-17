using ClinicManagementSystem.Models;
using ClinicManagementSystem.Utils;

//* patient datatype
namespace ClinicManagementSystem.Models
{
    public class Patient : Person
    {
        //* General Info
        public int Id {get; set;}
        public string Type;
        public string Insurance;
        private string history = "";
        public string History
        {
            get {return history;}
            set {history += "\n" + value;}
        }

        //* Child Specific fields
        public int? ParentId {get; set;}
        public PersonRecord? Parent {get; set;}
        public string? ParentsMaritalStatus;
        public string? ParentsJob;

        //* Adult specific fields
        public string? Job;
        public string? MaritalStatus;

        //* adult constructor
        public Patient(string fname, string lname, int SS, DateOnly dob, string phoneNumber,
            string sex, string insurance, string history, string job, string maritalStatus)
            : base(fname, lname, SS, dob, phoneNumber, sex)
        {
            Age = GetAge(dob); Type = "Adult";
            Insurance = insurance; History = history;
            Job = job; MaritalStatus = maritalStatus;
        }

        //* child constructor
        public Patient(string fname, string lname, int SS, DateOnly dob, string phoneNumber,
            string sex, string insurance, string history, string pms, string parentsJob, int parentId)
            : base(fname, lname, SS, dob, phoneNumber, sex)
        {
            Type = "Child"; Insurance = insurance;
            History = history; ParentsMaritalStatus = pms;
            ParentsJob = parentsJob; ParentId = parentId;
        }

        public static Patient GetInfo()
        {
            string fn = Getter.GetFirstName("Patient");
            string ln = Getter.GetLastName("Patient");
            int ss = Getter.GetSS("Patient");
            DateOnly dob = Getter.GetDOB("Patient");
            string pn = Getter.GetPhone("Patient");
            string sx = Getter.GetSex("Patient");
            string type = (GetAge(dob) < 18) ? "Child" : "Adult";

            string ins;
            do {Console.Write("Enter Patients Insurance: "); ins = Console.ReadLine().Trim();}
            while (string.IsNullOrWhiteSpace(ins));

            string hist;
            Console.WriteLine("Enter Patient's History (type 'done' to finish): ");
            hist = "";
            string line;
            while (true)
            {
                line = Console.ReadLine();
                if (line?.Trim().ToLower() == "done") break;
                hist += line + "\n";
            }

            if (type == "Child")
            {
                string pms;
                do
                {
                    Console.Write("Enter Parents Marital Status (`Married`, `Divorced`, `deceased`): ");
                    pms = Console.ReadLine().Trim();
                } while (!Validators.ValidatePMS(pms));

                string pj;
                do {Console.Write("Enter Parents Job: "); pj = Console.ReadLine().Trim();}
                while (string.IsNullOrWhiteSpace(pj));

                while (true)
                {
                    Console.Write("Enter Parent's PersonRecord ID: ");
                    if (int.TryParse(Console.ReadLine().Trim(), out parentId) && parentId > 0) break;
                    Console.WriteLine("Invalid parent ID. Please enter a valid number.");
                }
                return new Patient(fn, ln, ss, dob, pn, sx, ins, hist, pms: pms, parentsJob: pj, parentId);
            }

            string job;
            do {Console.Write("Enter Patients Job: "); job = Console.ReadLine().Trim();}
            while (string.IsNullOrWhiteSpace(job));
            string ms;
            do {Console.Write("Enter Patients Marital Status: "); ms = Console.ReadLine().Trim();}
            while (string.IsNullOrWhiteSpace(ms));
            return new Patient(fn, ln, ss, dob, pn, sx, ins, hist, job: job, maritalStatus: ms);
        }

        public override void PrintInfo()
        {Console.WriteLine($"Patient: {FirstName}, ID: {SocialSecurity}, Phone: {PhoneNumber}, Age: {Age}");}
    }
}
