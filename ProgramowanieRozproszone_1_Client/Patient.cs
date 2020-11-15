using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using IdentityModel.Client;

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
            client.DefaultRequestHeaders.Authorization = BasicAuthenticationHeaderValue.Parse(await GetToken());
            List<Patient> patients;
            HttpResponseMessage response = await client.GetAsync("https://localhost:5001/api/patients");
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
            client.DefaultRequestHeaders.Authorization = BasicAuthenticationHeaderValue.Parse(await GetToken());
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

            await client.PostAsync("https://localhost:5001/api/patients",
                new StringContent(patientJson, Encoding.UTF8, "application/json"));
            Console.WriteLine("Pacjent : " + patient.FirstName + " " + patient.LastName + " został pomyślnie stworzony!");
            Console.ReadKey();
        }
        private static async Task<string> GetToken()
        {
            using var client = new HttpClient();
            DiscoveryDocumentResponse disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
            {
                Address = "https://login.microsoftonline.com/146ab906-a33d-47df-ae47-fb16c039ef96/v2.0/",
                Policy =
                {
                    ValidateEndpoints = false
                }
            });

            if (disco.IsError)
                throw new InvalidOperationException(
                    $"No discovery document. Details: {disco.Error}");

            var tokenRequest = new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "67dd9cfb-4344-4cc8-a2ca-573f6bb4422f",
                ClientSecret = "tlVkd6QAX-kcl.XP_4Yslh00-2kPS6G_9_",
                Scope = "api://67dd9cfb-4344-4cc8-a2ca-573f6bb4422f/.default"
            };

            var token = await client.RequestClientCredentialsTokenAsync(tokenRequest);

            if (token.IsError)
                throw new InvalidOperationException($"Couldn't gather token. Details: {token.Error}");

            return $"{token.TokenType} {token.AccessToken}";
        }
    }
}


