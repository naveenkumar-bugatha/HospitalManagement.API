using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Integration.Tests
{
    internal class HttpHelper
    {
        public static StringContent GetJsonHttpContent(object items)
        {
            return new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
        }

        internal static class Urls
        {
            public readonly static string GetPatients = "/api/Patient/GetPatients";
            public readonly static string GetPatient = "/api/Patient/GetPatient/";
            public readonly static string AddPatient = "/api/Patient/AddPatient";
        }
    }
}
