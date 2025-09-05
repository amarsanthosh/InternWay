using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Recruiter;
using api.Interface;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [ApiController]
    [Route("api/recruiter")]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterRepository _recruiterRepository;
        private readonly IMapper _mapper;
        public RecruiterController(IRecruiterRepository recruiterRepository, IMapper mapper)
        {
            _recruiterRepository = recruiterRepository;
            _mapper = mapper;
        }

        // GET: api/recruiter
        [HttpGet]
        public async Task<IActionResult> GetAllRecruiters()
        {
            var recruiters = await _recruiterRepository.GetAllRecruitersAsync();
            return Ok(recruiters);
        }
        // GET: api/recruiter/{id}
        [HttpGet("{int:id}")]
        public async Task<IActionResult> GetRecruiterById([FromRoute] int id)
        {
            var recruiter = await _recruiterRepository.GetRecruiterByIdAsync(id);
            if (recruiter == null)
            {
                return NotFound();
            }
            return Ok(recruiter);
        }

        // POST: api/recruiter
        [HttpPost]
        public async Task<IActionResult> AddRecruiter([FromBody] RecruiterCreateDto recruiterdto)
        {
            var recruiter = _mapper.Map<Recruiter>(recruiterdto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _recruiterRepository.AddRecruiterAsync(recruiter);
            return Ok("Successfully added");
        }

        // PUT: api/recruiter/{id}
        [HttpPut("{int:id}")]
        public async Task<IActionResult> UpdateRecruiter([FromRoute] int id, [FromBody] RecruiterUpdateDto recruiterdto)
        {
            var recruiter = _mapper.Map<Recruiter>(recruiterdto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedRecruiter = await _recruiterRepository.UpdateRecruiterAsync(id, recruiter);
            if (updatedRecruiter == null)
            {
                return NotFound();
            }
            return Ok("Successfully updated");
        }

        // DELETE: api/recruiter/{id}
        [HttpDelete("{int:id}")]
        public async Task<IActionResult> DeleteRecruiter([FromRoute] int id)
        {
            var deletedRecruiter = await _recruiterRepository.DeleteRecruiterAsync(id);
            if (deletedRecruiter == null)
            {
                return NotFound();
            }
            return Ok("Successfully deleted");
        }
    }
}