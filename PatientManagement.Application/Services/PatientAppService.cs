using AutoMapper;
using PatientManagement.Application.DTOs;
using PatientManagement.Application.Repositories;
using PatientManagement.Application.Utils;
using PatientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Application.Services
{
    public class PatientAppService : IPatientAppService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public PatientAppService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<Patient> CreatePatientAsync(PatientDto input)
        {
            try
            {
                if (await IsDuplicatePatientAsync(input))
                {
                    throw new DuplicateException("Duplicate patient found.");
                }

                return await _patientRepository.InsertAsync(_mapper.Map<PatientDto, Patient>(input));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the patient.", ex);
            }
        }

        public async Task DeactivatePatientAsync(Guid id, string reason)
        {
            try
            {
                var patient = await _patientRepository.GetAsync(id);
                if (patient == null)
                {
                    throw new KeyNotFoundException("Patient not found.");
                }

                patient.IsActive = false;
                patient.Reason = reason;

                await _patientRepository.UpdateAsync(patient);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deactivating the patient.", ex);
            }
        }

        public async Task ActivatePatientAsync(Guid id)
        {
            try
            {
                var patient = await _patientRepository.GetAsync(id);
                if (patient == null)
                {
                    throw new KeyNotFoundException("Patient not found.");
                }

                patient.IsActive = true;

                await _patientRepository.UpdateAsync(patient);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deactivating the patient.", ex);
            }
        }

        public async Task<PagedResultDto<Patient>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                return await _patientRepository.GetAllAsync(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving all patients.", ex);
            }
        }
        
        public async Task<List<Patient>> GetAllNoPagedAsync()
        {
            try
            {
                return await _patientRepository.GetAllNoPagedAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving all patients.", ex);
            }
        }

        public async Task<Patient> GetPatientByIdAsync(Guid id)
        {
            try
            {
                var patient = await _patientRepository.GetAsync(id);
                if (patient == null)
                {
                    throw new KeyNotFoundException("Patient not found.");
                }

                return patient;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the patient.", ex);
            }
        }

        public async Task<bool> IsDuplicatePatientAsync(PatientDto input)
        {
            try
            {
                var patients = await _patientRepository.GetAllNoPagedAsync();
                return patients.Any(p =>
                    p.FirstName.Equals(input.FirstName, StringComparison.OrdinalIgnoreCase) &&
                    p.LastName.Equals(input.LastName, StringComparison.OrdinalIgnoreCase) &&
                    p.Gender == input.Gender &&
                    p.DateOfBirth == input.DateOfBirth);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while checking for duplicate patients.", ex);
            }
        }

        public async Task<Patient> UpdatePatientAsync(Guid id, PatientDto input)
        {
            try
            {
                var existingPatient = await _patientRepository.GetAsync(id);
                if (existingPatient == null)
                {
                    throw new KeyNotFoundException("Patient not found.");
                }

                _mapper.Map(input, existingPatient);

                await _patientRepository.UpdateAsync(existingPatient);
                return existingPatient;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the patient.", ex);
            }
        }
    }
}
