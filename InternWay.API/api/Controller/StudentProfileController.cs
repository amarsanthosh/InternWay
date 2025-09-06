using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.StudentProfile;
using api.Interface;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [ApiController]
    [Route("api/studentprofile")]
    public class StudentProfileController : ControllerBase
    {
        private readonly IStudentProfileRepository _studentProfileRepository;
        private readonly IMapper _mapper;
        public StudentProfileController(IStudentProfileRepository studentProfileRepository, IMapper mapper)
        {
            _studentProfileRepository = studentProfileRepository;
            _mapper = mapper;
        }
        // GET: api/studentprofile
        [HttpGet]
        [Authorize(Roles = "Admin,Recruiter")]
        public async Task<IActionResult> GetAllStudentProfiles()
        {
            var studentProfiles = await _studentProfileRepository.GetAllStudentProfilesAsync();
            return Ok(studentProfiles);
        }

        // GET: api/studentprofile/{id}
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Recruiter")]
        public async Task<IActionResult> GetStudentProfileById([FromRoute] int id)
        {
            var studentProfile = await _studentProfileRepository.GetStudentProfileByIdAsync(id);
            if (studentProfile == null)
            {
                return NotFound();
            }
            return Ok(studentProfile);
        }

        // POST: api/studentprofile
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> AddStudentProfile([FromBody] StudentCreateDto studentProfileDto)
        {    if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var studentProfile = _mapper.Map<StudentProfile>(studentProfileDto);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            studentProfile.AppUserId = userId!; 
            await _studentProfileRepository.AddStudentProfileAsync(studentProfile);
            return Ok("Successfully added");
        }

        // PUT: api/studentprofile/{id}
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> UpdateStudentProfile([FromRoute] int id, [FromBody] StudentUpdateDto studentProfileDto)
        {
            var studentProfile = _mapper.Map<StudentProfile>(studentProfileDto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedStudentProfile = await _studentProfileRepository.UpdateStudentProfileAsync(id, studentProfile);
            if (updatedStudentProfile == null)
            {
                return NotFound();
            }
            return Ok("Successfully updated");
        }

        // DELETE: api/studentprofile/{id}
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> DeleteStudentProfile([FromRoute] int id)
        {
            var deletedStudentProfile = await _studentProfileRepository.DeleteStudentProfileAsync(id);
            if (deletedStudentProfile == null)
            {
                return NotFound();
            }
            return Ok("Successfully deleted");
        }
    }
}