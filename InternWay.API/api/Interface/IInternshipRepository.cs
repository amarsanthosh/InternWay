using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Internship;
using api.Models;

namespace api.Interface
{
    public interface IInternshipRepository
    {
        Task<List<InternshipDto>> GetAllInternshipsAsync();
        Task<InternshipDto?> GetInternshipByIdAsync(int id);
        Task<Internship?> AddInternshipAsync(Internship internship);
        Task<Internship?> UpdateInternshipAsync(int id, Internship internship);
        Task<Internship?> DeleteInternshipAsync(int id);
    }
}