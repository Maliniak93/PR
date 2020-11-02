using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace ProgramowanieRozproszone_1_Client.Model
{
    class Patient
    {
        HttpClient client = new HttpClient();
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime LastPositiveTest { get; set; }
        public string Email { get; set; }

        public async Task ShowPatients()
        {
            List<Patient> patients;
            HttpResponseMessage response = await client.GetAsync("https://localhost:44376/api/patients");
                if (response.IsSuccessStatusCode)
                {
                var result = response.Content.ReadAsStringAsync().Result;
                patients = JsonConvert.DeserializeObject<List<Patient>>(result);
                foreach (Patient patient in patients)
                {
                    Console.WriteLine("Id Pacjenta : " + patient.Id + ", Imie Pacjenta : " + patient.FirstName + ", Nazwisko Pacjenta : " + patient.LastName + ",\nEmail pacjenta : " + patient.Email + ", Data ostatniego pozytywnego testu : " + patient.LastPositiveTest.ToString("dd'/'MM'/'yyyy\n"));
                }
            }
        }
        public async Task AddPatient()
        {

            Patient patient = new Patient();
            Console.Clear();
            Console.WriteLine("Wprowadź imię pacjenta: ");
            patient.FirstName = Console.ReadLine();
            Console.WriteLine("Wprowadź nazwisko pacjenta: ");
            patient.LastName = Console.ReadLine();
            Console.WriteLine("Wprowadź wiek pacjenta: ");
            patient.Age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Data ostatniego pozytywnego testu: ");
            patient.LastPositiveTest = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Wprowadź email pacjenta: ");
            patient.Email = Console.ReadLine();

            string patientJson = JsonConvert.SerializeObject(patient);

            await client.PostAsync("https://localhost:44376/api/patients",
                new StringContent(patientJson, Encoding.UTF8, "application/json"));
            Console.WriteLine("Pacjent : " + patient.FirstName + " " + patient.LastName + " został pomyślnie stworzony!");
            Console.ReadKey();
        }
    }
}
