using PatientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Application.Repositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
    }
}
