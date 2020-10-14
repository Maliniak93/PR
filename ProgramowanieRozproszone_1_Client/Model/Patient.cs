using Newtonsoft.Json;
using ProgramowanieRozproszone_1_Client.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProgramowanieRozproszone_1_Client.Model
{
    class Patient
    {
        public static readonly PatientAPI _api = new PatientAPI();
        public static HttpClient httpClient = new HttpClient();

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime LastPositiveTest { get; set; }

        public async Task ShowPatients()
        {
                List<Patient> patients;
                HttpClient client = _api.Initial();
                HttpResponseMessage res = await client.GetAsync("api/patients");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    patients = JsonConvert.DeserializeObject<List<Patient>>(result);
                    foreach (Patient patient in patients)
                    {
                        Console.WriteLine("Id Pacjenta : " + patient.Id + ", Imie Pacjenta : " + patient.FirstName + ", Nazwisko Pacjenta : " + patient.LastName + ", Data ostatniego pozytywnego testu : " + patient.LastPositiveTest.ToString("dd'/'MM'/'yyyy"));
                    }
                }
        }
        public Patient AddPatient()
        {
            Patient patient = new Patient();
            HttpClient client = _api.Initial();
            ConsoleKey response;
            Console.WriteLine("Czy chcesz dodać nowego pacjenta? [y/n]");
            response = Console.ReadKey(true).Key;
            if (response == ConsoleKey.Y)
            {
                Console.Clear();
                Console.WriteLine("Imie Pacjeta?\n");
                patient.FirstName = Console.ReadLine();
                Console.WriteLine("Nazwisko Pacjeta?\n");
                patient.LastName = Console.ReadLine();
                Console.WriteLine("Wiek Pacjeta?\n");
                patient.Age = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Data ostatniego pozytywnego testu?\n");
                patient.LastPositiveTest = Convert.ToDateTime(Console.ReadLine());
                var postTask = client.PostAsJsonAsync<Patient>("api/patients", patient);
                postTask.Wait();
                return patient;
            }
            else
            {
                return patient;
            }
        }
    }
}
