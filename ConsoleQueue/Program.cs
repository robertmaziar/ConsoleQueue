using System;
using System.Collections;
using System.Linq;

namespace ConsoleQueue
{
    public class Patient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Symptoms { get; set; }
    }

    public static class PatientModel
    {
        public static Patient CurrentPatient;

        public static Patient GetCurrentPatient()
        {
            return CurrentPatient;
        }

        public static void SetCurrentPatient(Patient value)
        {
            CurrentPatient = value;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Queue pQueue = new Queue();
            pQueue.Enqueue(new Patient() { FirstName = "John", LastName = "Doe", Symptoms = new string[] { "Cough", "Fever" } });
            pQueue.Enqueue(new Patient() { FirstName = "Jane", LastName = "Robbinson", Symptoms = new string[] { "Stomach Pain" } });
            pQueue.Enqueue(new Patient() { FirstName = "Billy", LastName = "Bob", Symptoms = new string[] { "Cough", "Cold" } });

            PatientModel.SetCurrentPatient((Patient)pQueue.Peek());

            int userInput;
            do
            {
                userInput = DisplayMenu(pQueue);
                if (userInput == 1)
                {
                    DisplayCurrentPatient();
                }
                else if (userInput == 2)
                {
                    if (CanProcessNextPatient(pQueue))
                    {
                        ProcessNextPatient(pQueue);
                    }
                    else
                    {
                        Console.WriteLine("|--------------------------------------------------------");
                        Console.WriteLine("\tNo Patients left in Queue");
                        Console.WriteLine("|--------------------------------------------------------");
                        Console.ReadLine();
                    }
                }
            } while (userInput != 3);
        }

        public static int DisplayMenu(Queue queue)
        {
            Console.Clear();
            Console.WriteLine("|--------------------------------------------------------");
            Console.WriteLine("\tPatients in Queue: {0}", queue.Count);

            while (true)
            {
                Console.WriteLine("|--------------------------------------------------------");
                Console.WriteLine("\tEnter one of the following numbers to perform an action");
                Console.WriteLine("\t[1] View Current Patient");
                Console.WriteLine("\t[2] Move to Next Patient");
                Console.WriteLine("\t[3] Exit");
                Console.WriteLine("|--------------------------------------------------------");

                int result;
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("\tPlease enter a valid numbered option.");
                }
            }
        }

        public static void DisplayCurrentPatient()
        {
            Patient currentPatient = PatientModel.GetCurrentPatient();
            Console.WriteLine("|--------------------------------------------------------");
            Console.WriteLine("\tCurrent Patient is: {0}", currentPatient.FirstName + " " + currentPatient.LastName);

            if (currentPatient.Symptoms.Count() > 0)
            {
                Console.WriteLine("\tSymptoms:");
                int symptomCount = 1;
                foreach (string symptom in currentPatient.Symptoms)
                {
                    Console.WriteLine($"\t[{symptomCount}] {symptom}");
                    symptomCount++;
                }
            }

            Console.WriteLine("|--------------------------------------------------------");
            Console.WriteLine("\tPress any key to continue...");
            Console.ReadLine();
        }

        public static bool CanProcessNextPatient(Queue queue)
        {
            if (queue.Count > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static void ProcessNextPatient(Queue queue)
        {
            queue.Dequeue();

            if (CanProcessNextPatient(queue))
            {
                PatientModel.SetCurrentPatient((Patient)queue.Peek());
                Patient newPatient = PatientModel.GetCurrentPatient();

                Console.WriteLine("|--------------------------------------------------------");
                Console.WriteLine("\tPatients in Queue: [{0}]", queue.Count);
                Console.WriteLine("\tNew Patient is: {0}", newPatient.FirstName + " " + newPatient.LastName);
                Console.WriteLine("|--------------------------------------------------------");
                Console.WriteLine("\tPress any key to continue...");
            }
            else
            {
                Console.WriteLine("|--------------------------------------------------------");
                Console.WriteLine("\tNo Patients left in Queue");
                Console.WriteLine("|--------------------------------------------------------");
            }

            Console.ReadLine();
        }
    }
}
