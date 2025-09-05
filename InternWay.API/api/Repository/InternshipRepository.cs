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
    public class InternshipRepository : IInternshipRepository
    {
        private readonly ApplicationDbContext _context;

        public InternshipRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Internship?> AddInternshipAsync(Internship internship)
        {
            await _context.Internships.AddAsync(internship);
            await _context.SaveChangesAsync();
            return internship;
        }

        public async Task<Internship?> DeleteInternshipAsync(int id)
        {
            var existingInternship = await _context.Internships.FirstOrDefaultAsync(i => i.Id == id);
            if (existingInternship == null)
            {
                return null;
            }
            _context.Internships.Remove(existingInternship);
            await _context.SaveChangesAsync();
            return existingInternship;
        }

        public async Task<List<Internship>> GetAllInternshipsAsync()
        {
            return await _context.Internships.ToListAsync();
        }

        public async Task<Internship?> GetInternshipByIdAsync(int id)
        {
            var internship = await _context.Internships.FirstOrDefaultAsync(i => i.Id == id);
            if (internship == null)
            {
                return null;
            }
            return internship;
        }

        public async Task<Internship?> UpdateInternshipAsync(int id, Internship internship)
        {
            var existingInternship = await _context.Internships.FirstOrDefaultAsync(i => i.Id == id);
            if (existingInternship == null)
            {
                return null;
            }
            existingInternship.Title = internship.Title;
            existingInternship.Description = internship.Description;
            existingInternship.Location = internship.Location;
            existingInternship.RecruiterId = internship.RecruiterId;
            existingInternship.InternshipSkills = internship.InternshipSkills;

            await _context.SaveChangesAsync();
            return existingInternship;
        }
    }
}