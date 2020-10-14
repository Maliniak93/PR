using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ProgramowanieRozproszone_1_Client.Helper
{
    class PatientAPI
    {
        public HttpClient Initial()
        {
            var patient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44376/")
            };
            return patient;
        }
    }
}
