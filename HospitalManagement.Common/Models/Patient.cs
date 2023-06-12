using System.Diagnostics.CodeAnalysis;

namespace HospitalManagement.Common.Models
{
    [ExcludeFromCodeCoverage]
    public class Patient
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}