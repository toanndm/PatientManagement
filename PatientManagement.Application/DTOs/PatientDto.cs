using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Application.DTOs
{
    public sealed class PatientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PrimaryAddress { get; set; }
        public string PrimaryProvince { get; set; }
        public string PrimaryDistrict { get; set; }
        public string PrimaryWard { get; set; }
        public string SecondaryAddress { get; set; }
        public string SecondaryProvince { get; set; }
        public string SecondaryDistrict { get; set; }
        public string SecondaryWard { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
    }
}
