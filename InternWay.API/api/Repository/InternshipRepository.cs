using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Internship;
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

        public async Task<List<InternshipDto>> GetAllInternshipsAsync()
        {
            // return await _context.Internships.ToListAsync();
            return await _context.Internships
            .Include(i => i.InternshipSkills)
            .ThenInclude(iss => iss.Skill)
            .Select(i => new InternshipDto
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Location = i.Location,
                PostedOn = i.PostedOn,
                IsActive = i.IsActive,
                RecruiterId = i.RecruiterId,
                Skills = i.InternshipSkills
            .Select(isss => new SkillDto
            {
                Id = isss.Skill.Id,
                // Name = isss.Skill.Name
            }).ToList()
            })
    
            .ToListAsync(); //
        }


        public async Task<InternshipDto?> GetInternshipByIdAsync(int id)
        {
            var internship = await _context.Internships
            .Include(i => i.InternshipSkills)
            .ThenInclude(iss => iss.Skill)
            .FirstOrDefaultAsync(i => i.Id == id);

            if (internship == null)
            {
                return null;

            }
            return new InternshipDto
            {
                Id = internship.Id,
                Title = internship.Title,
                Description = internship.Description,
                Location = internship.Location,
                PostedOn = internship.PostedOn,
                IsActive = internship.IsActive,
                RecruiterId = internship.RecruiterId,
                Skills = internship.InternshipSkills
            .Select(isss => new SkillDto
            {
                Id = isss.Skill.Id,
                // Name = isss.Skill.Name
            }).ToList()
            };
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