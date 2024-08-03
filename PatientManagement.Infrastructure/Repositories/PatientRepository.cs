using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientManagement.Application.Repositories;
using PatientManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;
        public PatientRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task DeleteAsync(Patient entity)
        {
            try
            {
                _context.Patients.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            try
            {
                return await _context.Patients.ToListAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Patient> GetAsync(Guid id)
        {
            try
            {
                return await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Patient>> GetFilteredAsync(Expression<Func<Patient, bool>> predicate)
        {
            try
            {
                return await _context.Patients.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Patient> InsertAsync(Patient entity)
        {
            try
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Patient> UpdateAsync(Patient entity)
        {
            try
            {
                var existingEntity = await _context.Patients.FindAsync(entity.Id);
                if (existingEntity == null)
                {
                    throw new KeyNotFoundException($"Patient with Id {entity.Id} not found.");
                }

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);

                await _context.SaveChangesAsync();

                return existingEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
