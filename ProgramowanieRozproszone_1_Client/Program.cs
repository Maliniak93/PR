using ProgramowanieRozproszone_1_Client.Model;
using System;
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

            ConsoleKey response;
            Console.WriteLine("Czy chcesz dodać nowego pacjenta? [y/n]");
            response = Console.ReadKey(true).Key;
            if (response == ConsoleKey.Y)
            {
                Console.Clear();
                await patient.AddPatient();
                Console.Clear();
                goto Start;
            }
            else
            {
                Console.Clear();
                goto Start;
            }
        }
    }
}
