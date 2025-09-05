using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Application;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Application?> AddApplicationAsync(Application application)
        {
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();

            return application; 
        }

        public async Task<Application?> DeleteApplicationAsync(int id)
        {
            var existingApplication = await _context.Applications.FirstOrDefaultAsync(i => i.Id == id);
            if (existingApplication == null)
            {
                return null;
            }
            _context.Applications.Remove(existingApplication);
            await _context.SaveChangesAsync();
            return existingApplication;
        }

        public async Task<List<Application>> GetAllApplicationsAsync()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<Application?> GetApplicationByIdAsync(int id)
        {
            var application = await _context.Applications.FirstOrDefaultAsync(a => a.Id == id);
            if (application == null)
            {
                return null;
            }
            return application; 
        }

        public async Task<Application?> UpdateApplicationAsync(int id, Application application)
        {
            var existingApplication = await _context.Applications.FirstOrDefaultAsync(i => i.Id == id);
            if (existingApplication == null)
            {
                return null;
            }
            existingApplication.InternshipId = application.InternshipId;
            existingApplication.StudentProfileId = application.StudentProfileId;
            existingApplication.Status = application.Status;
            await _context.SaveChangesAsync();
            return existingApplication;
        }
    }
}