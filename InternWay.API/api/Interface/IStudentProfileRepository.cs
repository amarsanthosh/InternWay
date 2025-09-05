using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interface
{
    public interface IStudentProfileRepository
    {
        Task<List<StudentProfile>> GetAllStudentProfilesAsync();
        Task<StudentProfile?> GetStudentProfileByIdAsync(int id);
        Task<StudentProfile?> AddStudentProfileAsync(StudentProfile studentProfile);
        Task<StudentProfile?> UpdateStudentProfileAsync(int id, StudentProfile studentProfile);
        Task<StudentProfile?> DeleteStudentProfileAsync(int id);
    }
}