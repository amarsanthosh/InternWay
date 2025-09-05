using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Application;
using api.Models;

namespace api.Interface
{
    public interface IApplicationRepository
    {
        Task<List<Application>> GetAllApplicationsAsync();
        Task<Application?> GetApplicationByIdAsync(int id);
        Task<Application?> AddApplicationAsync(Application application);
        Task<Application?> UpdateApplicationAsync(int id , Application application);
        Task<Application?> DeleteApplicationAsync(int id);
    }
}