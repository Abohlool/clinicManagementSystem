using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Utils
{
    public static class SearchAndSort
    {
        //* selection sort
        public static void SortPatientsByName(Patient[] patients, int count)
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (string.Compare(patients[i].FirstName, patients[j].FirstName) > 0)
                        {(patients[j], patients[i]) = (patients[i], patients[j]);}
                }
            }
        }

        //* full/linear search
        public static void SearchPatientByName(Patient[] patients, int count, string name)
        {
            bool found = false;
            for (int i = 0; i < count; i++)
            {
                if (patients[i].FirstName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {patients[i].PrintInfo(); found = true;}
            }
            if (!found) 
                {Console.WriteLine($"No patient found with that name: {name}.");}
        }

        //* binary search
        public static void SearchByID(Patient[] patients, int count, int id)
        {
            SortPatientsByID(patients, count);
            int low = 0, high = count - 1;
            while (low <= high)
            {
                int middle = (low + high) / 2;
                
                if (patients[middle].SocialSecurity == id)
                    {patients[middle].PrintInfo(); return;}

                else if (patients[middle].SocialSecurity < id) {low = middle + 1;}
                else {high = middle - 1;}
            }
        Console.WriteLine($"Patient not found by ID: {id}.");
        }

        //* selection sort
        public static void SortPatientsByID(Patient[] patients, int count)
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (patients[i].SocialSecurity > patients[j].SocialSecurity)
                        {(patients[j], patients[i]) = (patients[i], patients[j]);}
                }
            }
        }
    
            //* full/linear search
        public static void SearchVisitsByPatient(Visit[] visits, int count, string name)
        {
            bool found = false;
            for (int i = 0; i < count; i++)
            {
                if (visits[i].Patient.FirstName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    visits[i].PrintVisit();
                    found = true;
                }
            }
            if (!found) {Console.WriteLine("No visits found.");}
        }

        //* full/linear search
        public static void SearchVisitsByDateRange(Visit[] visits, int count, DateTime start, DateTime end)
        {
            bool found = false;
            for (int i = 0; i < count; i++)
            {
                if (visits[i].VisitTime >= start && visits[i].VisitTime <= end)
                {
                    visits[i].PrintVisit();
                    found = true;
                }
            }
            if (!found) {Console.WriteLine("No visits in that date range.");}
        }
    }
}
