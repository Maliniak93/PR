using Newtonsoft.Json;
using ProgramowanieRozproszone_1_Client.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProgramowanieRozproszone_1_Client
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            Patient patient = new Patient();
        Start:
            await patient.ShowPatients();
            Patient newPatient = patient.AddPatient();
            Console.Clear();
            if (newPatient.FirstName == null)
                goto Start;
            else
            {
                Console.WriteLine("Pacjent : " + newPatient.FirstName + " " + newPatient.LastName + " został pomyślnie stworzony!");
                Console.ReadKey();
                Console.Clear();
                goto Start;
            }
        }
    }
}
