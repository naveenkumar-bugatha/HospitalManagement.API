namespace HospitalManagement.Common.Models
{
    public class Patient
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}