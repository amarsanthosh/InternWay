using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class RecruiterRepository : IRecruiterRepository
    {
        private readonly ApplicationDbContext _context;

        public RecruiterRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Recruiter?> AddRecruiterAsync(Recruiter recruiter)
        {
            await _context.Recruiters.AddAsync(recruiter);
            await _context.SaveChangesAsync();
            return recruiter;   
        }

        public async Task<Recruiter?> DeleteRecruiterAsync(int id)
        {
            var existingRecruiter = await _context.Recruiters.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRecruiter == null)
            {
                return null;
            }
            _context.Recruiters.Remove(existingRecruiter);
            await _context.SaveChangesAsync();
            return existingRecruiter;
        }

        public async Task<List<Recruiter>> GetAllRecruitersAsync()
        {
            return await _context.Recruiters.ToListAsync();
        }

        public async Task<Recruiter?> GetRecruiterByIdAsync(int id)
        {
            var recruiter = await _context.Recruiters.FirstOrDefaultAsync(r => r.Id == id);
            if (recruiter == null)
            {
                return null;
            }
            return recruiter;
        }

        public async Task<Recruiter?> UpdateRecruiterAsync(int id, Recruiter recruiter)
        {
            var existingRecruiter = await _context.Recruiters.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRecruiter == null)
            {
                return null;
            }
            existingRecruiter.About = recruiter.About;
            existingRecruiter.CompanyName = recruiter.CompanyName;
            existingRecruiter.Location = recruiter.Location;
            // existingRecruiter.AppUserId = recruiter.AppUserId;           
            await _context.SaveChangesAsync();
            return existingRecruiter;
        }
    }
}