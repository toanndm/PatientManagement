using PatientManagement.Application.DTOs;
using PatientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Application.Services
{
    public interface IPatientAppService
    {
        Task<Patient> CreatePatientAsync(PatientDto input);
        Task<Patient> UpdatePatientAsync(Guid id, PatientDto input);
        Task DeactivatePatientAsync(Guid id, string reason);
        Task ActivatePatientAsync(Guid id);
        Task<Patient> GetPatientByIdAsync(Guid id);
        Task<PagedResultDto<Patient>> GetAllAsync(int pageNumber, int pageSize);
        Task<List<Patient>> GetAllNoPagedAsync();
        Task<bool> IsDuplicatePatientAsync(PatientDto input);
    }
}
