using Newtonsoft.Json;
using System.Text;

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
            public readonly static string GetPatients = "/api/Patients/GetPatients";
            public readonly static string GetPatient = "/api/Patients/GetPatient/";
            public readonly static string AddPatient = "/api/Patients/AddPatient";
            public readonly static string DeletePatient = "/api/Patients/DeletePatient/";
            public readonly static string UpdatePatient = "/api/Patients/UpdatePatient";
        }
    }
}
