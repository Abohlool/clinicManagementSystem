using ClinicManagementSystem.Models;
using ClinicManagementSystem.Utils;
using MySql.Data.MySqlClient;

namespace ClinicManagementSystem
{
    class Program
    {
        public const int MAX = 100;

        //* main
        static void Main(string[] args)
        {
            Patient[] patients = new Patient[MAX];
            int patientCount = 0;
            Doctor[] doctors = new Doctor[MAX];
            int doctorCount = 0;
            Employee[] employees = new Employee[MAX];
            int employeeCount = 0;
            Visit[] visits = new Visit[MAX];
            int visitCount = 0;

            while (true)
            {
                Console.WriteLine("\n=== Main Menu ===");
                Console.WriteLine("1. Manage Patients");
                Console.WriteLine("2. Manage Doctors");
                Console.WriteLine("3. Manage Employee");
                Console.WriteLine("4. Manage Visit");
                Console.WriteLine("5. Show All Data");
                Console.WriteLine("6. Exit");
                Console.Write("Choice: ");
                string ch = Console.ReadLine().Trim();                                                               //? choice

                switch (ch)                                                                                         //! System management
                {
                    case "1":                                                                                       //? patients
                    {
                        Console.WriteLine("\n=== Manage Patients ===");
                        Console.WriteLine("1. Add patient");
                        Console.WriteLine("2. Remove patient");
                        Console.WriteLine("3. Search for patient");
                        Console.Write("Choice: ");
                        string pch = Console.ReadLine().Trim();                                                     //? patient choice
                        
                        switch (pch)                                                                                //! patient management
                        {
                            case "1":                                                                               //! add patient
                            {
                                Console.WriteLine("\n=== Add Patient ===");
                                if (patientCount >= MAX) 
                                    {Console.WriteLine("Patient limit reached."); break;}

                                Console.Write("Name: ");
                                string pname = Console.ReadLine();                                                  //? patient name
                                int pid = Getter.GetSS();                                                           //? patient id
                                string pphone = Getter.GetPhone();                                                  //? patient phone number
                                int page = Getter.GetAge();                                                         //? patient age
                                string pgender = Getter.GetGender();                                                //? patient gender
                                
                                patients[patientCount++] = new Patient(pname.Trim(), pid, pphone.Trim(), page, pgender.Trim());
                                Console.WriteLine($"Patient `{pname}` added");
                                break;
                            }

                            case "2":                                                                               //! remove patient
                            {
                                Console.WriteLine("\n=== Remove Patient ===");
                                if (patientCount == 0)
                                        {Console.WriteLine("No patients to remove"); break;}
                                for (int i = 0; i < patientCount; i++) 
                                    {Console.WriteLine($"{i}. {patients[i].FirstName}");}

                                Console.Write("Select patient: ");
                                int pidx = Convert.ToInt32(Console.ReadLine().Trim());                              //? patient index
                                if (patientCount <= pidx || pidx < 0) 
                                    {Console.WriteLine("Invalid patient index"); break;}
                                    
                                patients[pidx].PrintInfo();
                                
                                
                                if (Validators.ConfirmRemoval(patients[pidx].FirstName)) 
                                {
                                    Console.WriteLine($"Patient {patients[pidx].FirstName} removed");
                                    //* reorganizing the array
                                    for (int i = pidx; i < patientCount-1; i++) 
                                        {patients[i] = patients[i+1];}
                                    patientCount -= 1;
                                    break;
                                }
                                Console.WriteLine("Patient not removed. Due to invalid confirmation");
                                break;
                            }

                            case "3":                                                                               //! search patient
                            {
                                Console.WriteLine("\n=== Search For Patient ===");
                                Console.WriteLine("1. Search by ID");
                                Console.WriteLine("2. Search by Name");
                                Console.Write("Choice: ");
                                string psch = Console.ReadLine().Trim();                                            //? patient search choice

                                switch (psch)
                                {
                                    case "1":                                                                       //! search by id
                                        Console.WriteLine("\n=== Search by ID ===");
                                        int sid = Getter.GetSS();
                                        SearchAndSort.SearchByID(patients, patientCount, sid);
                                        break;
                                    
                                    case "2":                                                                       //! search by name
                                        Console.WriteLine("\n=== Search by Name ===");
                                        Console.Write("Enter Name: ");
                                        string sname = Console.ReadLine().Trim();
                                        SearchAndSort.SearchPatientByName(patients, patientCount, sname);
                                        break;
                                    
                                    default:
                                        Console.WriteLine("Invalid option.");
                                        break;
                                }
                                break;
                            }

                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                        break;
                    }
                    
                    case "2":                                                                                       //? doctors
                    {
                        Console.WriteLine("\n=== Manage Doctors ===");
                        Console.WriteLine("1. Add doctor");
                        Console.WriteLine("2. Remove doctor");
                        Console.WriteLine("3. Add available time");
                        Console.Write("Choice: ");
                        string dch = Console.ReadLine();                                                            //? doctor choice
                        
                        switch (dch)                                                                                //! doctor management
                        {
                            case "1":                                                                               //! add doctor
                            {
                                Console.WriteLine("\n=== Add Doctor ===");
                                if (doctorCount >= MAX)
                                    {Console.WriteLine("Doctor limit reached."); break;}

                                Console.Write("Name: ");
                                string dname = Console.ReadLine();                                                  //? doctor name
                                int did = Getter.GetSS();                                                           //? doctor id
                                string dphone = Getter.GetPhone();                                                  //? doctor phone number

                                Console.Write("Specialization: ");
                                string spec = Console.ReadLine();                                                   //? doctor specialization

                                doctors[doctorCount++] = new Doctor(dname.Trim(), did, dphone.Trim(), spec.Trim());
                                Console.WriteLine($"Doctor `{dname}` added");
                                break;
                            }

                            case "2":                                                                               //! remove doctor
                            {
                                Console.WriteLine("\n=== Remove Doctor ===");
                                if (doctorCount == 0)
                                    {Console.WriteLine("No doctors to remove"); break;}
                                for (int i = 0; i < doctorCount; i++) 
                                    {Console.WriteLine($"{i}. {doctors[i].FirstName}");}

                                Console.Write("Select doctor: ");
                                int didx = Convert.ToInt32(Console.ReadLine().Trim());                              //? doctor index
                                if (doctorCount <= didx || didx < 0) 
                                    {Console.WriteLine("Invalid doctor id"); break;}

                                doctors[didx].PrintInfo();
                                
                                
                                if (Validators.ConfirmRemoval(doctors[didx].FirstName)) 
                                {
                                    Console.WriteLine($"Doctor {doctors[didx].FirstName} removed");
                                    //* reorganizing the array
                                    for (int i = didx; i < doctorCount-1; i++) 
                                        {doctors[i] = doctors[i+1];}
                                    doctorCount -= 1;
                                    break;
                                }
                                Console.WriteLine("Doctor not removed. Due to invalid confirmation");
                                break;
                            }

                            case "3":                                                                               //! add available time
                            {
                                Console.WriteLine("\n=== Add Available Time ===");
                                for (int i = 0; i < doctorCount; i++)
                                    {Console.WriteLine($"{i}. {doctors[i].FirstName}");}
                                
                                Console.Write("Select doctor: ");
                                int didx = Convert.ToInt32(Console.ReadLine().Trim());                              //? doctor index
                                if (doctorCount <= didx || didx < 0)
                                    {Console.WriteLine("Invalid doctor index"); break;}

                                Console.Write("Enter available time (yyyy-MM-dd HH:mm): ");
                                DateTime slot = Convert.ToDateTime(Console.ReadLine());
                                doctors[didx].AddAvailableTime(slot);
                                Console.WriteLine($"Time slot added");
                                break;
                            }

                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                        break;
                    }

                    case "3":                                                                                       //? employees
                    {
                        Console.WriteLine("\n=== Manage Employees ===");
                        Console.WriteLine("1. Add employee");
                        Console.WriteLine("2. Remove employee");
                        Console.Write("Choice: ");
                        string ech = Console.ReadLine();                                                            //? employee choice
                        
                        switch (ech)                                                                                //! employee management
                        {
                            case "1":                                                                               //! add employee
                            {
                                Console.WriteLine("\n=== Add Employee ===");
                                if (employeeCount >= MAX)
                                    {Console.WriteLine("Employee limit reached."); break;}

                                Console.Write("Name: ");
                                string ename = Console.ReadLine();                                                  //? employee name
                                int eid = Getter.GetSS();                                                           //? employee id
                                string ephone = Getter.GetPhone();                                                  //? employee phone number

                                Console.Write("Salary: ");
                                double sal = Convert.ToDouble(Console.ReadLine().Trim());                           //? employee salary

                                employees[employeeCount++] = new Employee(ename.Trim(), eid, ephone.Trim(), sal);
                                Console.WriteLine($"Employee `{ename}` added");
                                break;
                            }

                            case "2":                                                                               //! remove employee
                            {
                                Console.WriteLine("\n=== Remove Employee ===");
                                if (employeeCount == 0)
                                    { Console.WriteLine("No employees to remove"); break;}

                                for (int i = 0; i < employeeCount; i++)
                                    {Console.WriteLine($"{i}. {employees[i].FirstName}");}

                                Console.Write("Select employee: ");
                                int eidx = Convert.ToInt32(Console.ReadLine().Trim());                               //? employee index
                                if (employeeCount <= eidx || eidx < 0) 
                                    {Console.WriteLine("Invalid employee id"); break;}

                                employees[eidx].PrintInfo();
                                
                                if (Validators.ConfirmRemoval(employees[eidx].FirstName)) 
                                {
                                    Console.WriteLine($"Employee {employees[eidx].FirstName} removed");
                                    //* reorganizing the array
                                    for (int i = eidx; i < employeeCount-1; i++) 
                                        {employees[i] = employees[i+1];}
                                    employeeCount -= 1;
                                    break;
                                }
                                Console.WriteLine("Employee not removed. Due to invalid confirmation");
                                break;
                            }

                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                        break;
                    }

                    case "4":                                                                                       //? visits
                    {
                        Console.WriteLine("\n=== Manage Visits ===");
                        Console.WriteLine("1. Add Visit");
                        Console.WriteLine("2. Remove Visit");
                        Console.WriteLine("3. Reschedule Visit");
                        Console.WriteLine("4. Search Visits");
                        Console.Write("Choice: ");
                        string vch = Console.ReadLine();                                                            //? visit choice

                        switch (vch)                                                                                //! visit management
                        {
                            case "1":                                                                               //! add visit
                            {
                                Console.WriteLine("\n=== Add Visit ===");
                                if (visitCount >= MAX) 
                                    {Console.WriteLine("Visit limit reached."); break;}

                                for (int i = 0; i < patientCount; i++) 
                                    {Console.WriteLine($"{i}. {patients[i].FirstName}");}
                                
                                Console.Write("Select patient: ");
                                int pi = Convert.ToInt32(Console.ReadLine());                                       //? patient index

                                if (pi < 0 || pi >= patientCount)
                                    { Console.WriteLine("Invalid patient index."); break;}

                                for (int i = 0; i < doctorCount; i++) 
                                    {Console.WriteLine($"{i}. {doctors[i].FirstName}");}
                                
                                Console.Write("Select doctor: ");
                                int di = Convert.ToInt32(Console.ReadLine());                                       //? doctor index
                                if (di < 0 || di >= doctorCount)
                                    {Console.WriteLine("Invalid doctor index."); break;}

                                if (doctors[di].TimeCount > 0)
                                {
                                    Console.WriteLine("Available slots:");
                                    for (int i = 0; i < doctors[di].TimeCount; i++)
                                        {Console.WriteLine($"{i}. {doctors[di].AvailableTimes[i]}");}

                                    Console.Write("Select a slot: ");
                                    int sidx = Convert.ToInt32(Console.ReadLine());                                 //? slot index
                                    if (sidx >= doctors[di].TimeCount || sidx < 0 )
                                        {Console.WriteLine("Invalid slot."); break;}

                                    DateTime vtime = doctors[di].AvailableTimes[sidx];
                                    visits[visitCount++] = new Visit(patients[pi], doctors[di], vtime);
                                    Console.WriteLine("Visit registered.");

                                    for (int i = sidx; i < doctors[di].TimeCount - 1; i++)
                                        {doctors[di].AvailableTimes[i] = doctors[di].AvailableTimes[i + 1];}
                                    doctors[di].TimeCount -= 1;

                                }
                                else {Console.WriteLine("Doctor not available.");}
                                break;
                            }

                            case "2":                                                                               //! remove visit
                            {
                                Console.WriteLine("\n=== Remove Visit ===");
                                Visit.CancelVisit(visits, ref visitCount);
                                break;
                            }
                            
                            case "3":                                                                               //! edit visit
                            {
                                Console.WriteLine("\n=== Edit Visit ===");
                                for (int i = 0; i < visitCount; i++) 
                                    {Console.Write($"{i}."); visits[i].PrintVisit();}
                                
                                Console.Write("Select visit: ");
                                int vid = Getter.GetSS();                                                           //? visit id

                                if (visitCount <= vid || vid < 0) 
                                    {Console.WriteLine("Invalid visit id"); break;}
                                
                                Console.WriteLine("1. Change doctor");
                                Console.WriteLine("2. Change date");
                                Console.Write("Choice: ");
                                string rch = Console.ReadLine().Trim();                                             //? reschedule choice

                                switch (rch)                                                                        //! reschedule options
                                {
                                    case "1":                                                                       //! change doctor
                                    {
                                        Console.WriteLine("\n=== Change Doctor ===");
                                        for (int i = 0; i < doctorCount; i++)
                                            {Console.WriteLine($"{i}. {doctors[i].FirstName}");}

                                        Console.Write("Select new doctor: ");
                                        int newdidx = Convert.ToInt32(Console.ReadLine().Trim());                   //? new doctor index

                                        if (newdidx < 0 || newdidx >= doctorCount)
                                            {Console.WriteLine("Invalid doctor id"); break;}

                                        if (doctors[newdidx].TimeCount == 0)
                                            {Console.WriteLine("This doctor has no available slots."); break;}

                                        Console.WriteLine("Available slots:");
                                        for (int i = 0; i < doctors[newdidx].TimeCount; i++)
                                            Console.WriteLine($"{i}. {doctors[newdidx].AvailableTimes[i]}");

                                        Console.Write("Select slot: ");
                                        int sidx = Convert.ToInt32(Console.ReadLine().Trim());                      //? slot index

                                        if (sidx < 0 || sidx >= doctors[newdidx].TimeCount)
                                            {Console.WriteLine("Invalid slot"); break;}

                                        visits[vid].Doctor = doctors[newdidx];
                                        visits[vid].VisitTime = doctors[newdidx].AvailableTimes[sidx];

                                        for (int i = sidx; i < doctors[newdidx].TimeCount - 1; i++)
                                            {doctors[newdidx].AvailableTimes[i] = doctors[newdidx].AvailableTimes[i + 1];}
                                        doctors[newdidx].TimeCount -= 1;

                                        Console.WriteLine("Visit rescheduled to new doctor.");
                                        break;
                                    }

                                    case "2":                                                                       //! change date
                                    {
                                        Console.WriteLine("\n=== Change Date ===");
                                        Doctor currentDoc = visits[vid].Doctor;

                                        if (currentDoc.TimeCount == 0)
                                            {Console.WriteLine("This doctor has no other available slots."); break;}

                                        Console.WriteLine("Available slots:");
                                        for (int i = 0; i < currentDoc.TimeCount; i++)
                                            {Console.WriteLine($"{i}. {currentDoc.AvailableTimes[i]}");}

                                        Console.Write("Select slot: ");
                                        int sidx = Convert.ToInt32(Console.ReadLine().Trim());                      //? slot index

                                        if (sidx < 0 || sidx >= currentDoc.TimeCount)
                                            {Console.WriteLine("Invalid slot"); break;}

                                        visits[vid].VisitTime = currentDoc.AvailableTimes[sidx];
                                        
                                        //* reorganizing the array
                                        for (int i = sidx; i < currentDoc.TimeCount - 1; i++)
                                            {currentDoc.AvailableTimes[i] = currentDoc.AvailableTimes[i + 1];}
                                        currentDoc.TimeCount -= 1;

                                        Console.WriteLine("Visit rescheduled to new time.");
                                        break;
                                    }
                                        
                                    default:
                                        Console.WriteLine("Invalid option.");
                                        break;
                                }
                                break;
                            }

                            case "4":                                                                               //! search visits
                            {
                                Console.WriteLine("\n=== Search Visits ===");
                                Console.WriteLine("1. By patient name");
                                Console.WriteLine("2. By date range");
                                Console.Write("Choice: ");
                                string vsch = Console.ReadLine().Trim();                                            //? visit search choice

                                switch (vsch)
                                {
                                    case "1":                                                                       //? search by patient
                                        Console.WriteLine("\n=== Search by Name");
                                        Console.Write("Enter patient name: ");
                                        string pname = Console.ReadLine().Trim();
                                        SearchAndSort.SearchVisitsByPatient(visits, visitCount, pname);
                                        break;

                                    case "2":                                                                       //? search by date
                                        Console.WriteLine("\n=== Search By Date ===");
                                        Console.Write("Enter start date (yyyy-MM-dd): ");
                                        DateTime start = Convert.ToDateTime(Console.ReadLine().Trim());             //? start date
                                        Console.Write("Enter end date (yyyy-MM-dd): ");
                                        DateTime end = Convert.ToDateTime(Console.ReadLine().Trim());               //? end date
                                        SearchAndSort.SearchVisitsByDateRange(visits, visitCount, start, end);
                                        break;

                                    default:
                                        Console.WriteLine("Invalid option.");
                                        break;
                                }
                                break;
                            }
                        }
                        break;
                    }
                    
                    case "5":                                                                                       //? info
                    {
                        Console.WriteLine("\n--- Patients ---");
                        SearchAndSort.SortPatientsByName(patients, patientCount);
                        for (int i = 0; i < patientCount; i++)
                            { patients[i].PrintInfo(); }

                        Console.WriteLine("--- Doctors ---");
                        for (int i = 0; i < doctorCount; i++) 
                            {doctors[i].PrintInfo(); doctors[i].ShowSchedule();}

                        Console.WriteLine("--- Employees ---");
                        for (int i = 0; i < employeeCount; i++)
                        {
                            employees[i].PrintInfo();
                            Console.WriteLine("Salary: " + employees[i].CalculateSalary());
                        }

                        Console.WriteLine("--- Visits ---");
                        for (int i = 0; i < visitCount; i++) 
                            {visits[i].PrintVisit();}
                        break;
                    }

                    case "6":                                                                                       //? quit
                        {Console.WriteLine("\nClosing..."); return;}

                    default:                                                                                        //? invalid input
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
