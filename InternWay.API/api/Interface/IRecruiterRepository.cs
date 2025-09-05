using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interface
{
    public interface IRecruiterRepository
    {
        Task<List<Recruiter>> GetAllRecruitersAsync();
        Task<Recruiter?> GetRecruiterByIdAsync(int id);
        Task<Recruiter?> AddRecruiterAsync(Recruiter recruiter);
        Task<Recruiter?> UpdateRecruiterAsync(int id, Recruiter recruiter);
        Task<Recruiter?> DeleteRecruiterAsync(int id);
        
    }
}