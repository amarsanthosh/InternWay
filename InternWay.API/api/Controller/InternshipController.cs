using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Internship;
using api.Interface;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [ApiController]
    [Route("api/internship")]
    public class InternshipController : ControllerBase
    {
        private readonly IInternshipRepository _internshipRepository;
        private readonly IMapper _mapper;
        public InternshipController(IInternshipRepository internshipRepository, IMapper mapper)
        {
            _internshipRepository = internshipRepository;
            _mapper = mapper;
        }
        // GET: api/internship
        [HttpGet]
        public async Task<IActionResult> GetAllInternships()
        {
            var internships = await _internshipRepository.GetAllInternshipsAsync();
            return Ok(internships);
        }
        // GET: api/internship/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInternshipById([FromRoute] int id)
        {
            var internship = await _internshipRepository.GetInternshipByIdAsync(id);
            if (internship == null)
            {
                return NotFound();
            }
            return Ok(internship);
        }
        // POST: api/internship
        [HttpPost]
        public async Task<IActionResult> AddInternship([FromBody] InternshipCreateDto internshipdto)
        {
            var internship = _mapper.Map<Internship>(internshipdto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _internshipRepository.AddInternshipAsync(internship);
            return Ok("Successfully added");
        }
        // PUT: api/internship/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateInternship([FromRoute] int id, [FromBody] InternshipUpdateDto internshipdto)
        {
            var internship = _mapper.Map<Internship>(internshipdto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedInternship = await _internshipRepository.UpdateInternshipAsync(id, internship);
            if (updatedInternship == null)
            {
                return NotFound();
            }
            return Ok("Successfully updated");
        }
        // DELETE: api/internship/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteInternship([FromRoute] int id)
        {
            var deletedInternship = await _internshipRepository.DeleteInternshipAsync(id);
            if (deletedInternship == null)
            {
                return NotFound();
            }
            return Ok("Successfully deleted");
        }
    }
}