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
    public class StudentProfileRepository : IStudentProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentProfile?> AddStudentProfileAsync(StudentProfile studentProfile)
        {
            await _context.StudentProfiles.AddAsync(studentProfile);
            await _context.SaveChangesAsync();
            return studentProfile;
        }

        public async Task<StudentProfile?> DeleteStudentProfileAsync(int id)
        {
            var existingStudentProfile = await _context.StudentProfiles.FirstOrDefaultAsync(s => s.Id == id);
            if (existingStudentProfile == null)
            {
                return null;
            }
            _context.StudentProfiles.Remove(existingStudentProfile);
            await _context.SaveChangesAsync();
            return existingStudentProfile;
            
        }

        public async Task<List<StudentProfile>> GetAllStudentProfilesAsync()
        {
            return await _context.StudentProfiles.ToListAsync();
        }

        public async Task<StudentProfile?> GetStudentProfileByIdAsync(int id)
        {
            var studentProfile = await _context.StudentProfiles.FirstOrDefaultAsync(s => s.Id == id);
            if (studentProfile == null)
            {
                return null;
            }
            return studentProfile;
        }

        public async Task<StudentProfile?> UpdateStudentProfileAsync(int id, StudentProfile studentProfile)
        {
            var existingStudentProfile = await _context.StudentProfiles.FirstOrDefaultAsync(s => s.Id == id);
            if (existingStudentProfile == null)
            {
                return null;
            }
            existingStudentProfile.FullName = studentProfile.FullName;
            existingStudentProfile.Bio = studentProfile.Bio;
            existingStudentProfile.LinkedInUrl = studentProfile.LinkedInUrl;
            existingStudentProfile.ResumeUrl = studentProfile.ResumeUrl;
            existingStudentProfile.StudentSkills = studentProfile.StudentSkills;

            await _context.SaveChangesAsync();
            return existingStudentProfile;
        }
    }
}