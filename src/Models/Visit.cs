//* visit datatype

namespace ClinicManagementSystem.Models
{
    public class Visit
    {
        public Patient Patient {get;}
        public Doctor Doctor {get;}
        public DateTime VisitTime {get;}
        public bool IsCanceled = false;
        public string? CancellationReason = null;
        public DateTime? CancellationDate = null;
        public Visit? RescheduledFrom = null;

        public Visit(Patient patient, Doctor doctor, DateTime time)
            {Patient = patient; Doctor = doctor; VisitTime = time;}

        public void PrintVisit() 
            {Console.WriteLine($"Visit: {VisitTime} - {Patient.Name} with Dr. {Doctor.Name}");}
        
        public static void CancelVisit(Visit[] visits, ref int visitCount)
        {
            if (visitCount == 0)
                {Console.WriteLine("No visits to cancel."); return;}

            for (int i = 0; i < visitCount; i++) 
                {Console.WriteLine($"{i}. {visits[i].VisitTime} - {visits[i].Patient.Name} with Dr. {visits[i].Doctor.Name}");}

            Console.Write("Enter visit index to cancel: ");
            int index = Convert.ToInt32(Console.ReadLine().Trim());
            if ((index >= 0) && (index < visitCount))
            {
                //* reorganizing the array
                for (int i = index; i < visitCount-1; i++) 
                    {visits[i] = visits[i + 1];}
                visitCount -= 1;
                Console.WriteLine("Visit canceled.");
            }
            else {Console.WriteLine("Invalid index.");}
        }

    }
}
