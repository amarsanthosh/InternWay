using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Application;
using api.Interface;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [ApiController]
    [Route("api/application")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;
        public ApplicationController(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        // GET: api/application
        [HttpGet]
        [Authorize(Roles = "Admin,Recruiter")]
        public async Task<IActionResult> GetAllApplications()
        {
            var applications = await _applicationRepository.GetAllApplicationsAsync();
            return Ok(applications);
        }
        // GET: api/application/{id}
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetApplicationById([FromRoute] int id)
        {
            var application = await _applicationRepository.GetApplicationByIdAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }
        // POST: api/application
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> AddApplication([FromBody] ApplicationCreateDto applicationdto)
        {
            var application = _mapper.Map<Application>(applicationdto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _applicationRepository.AddApplicationAsync(application);
            return Ok("Successfully added");
        }
        // PUT: api/application/{id}
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> UpdateApplication([FromRoute] int id, [FromBody] ApplicationUpdateDto applicationdto)
        {
            var application = _mapper.Map<Application>(applicationdto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedApplication = await _applicationRepository.UpdateApplicationAsync(id, application);
            if (updatedApplication == null)
            {
                return NotFound();
            }
            return Ok("Successfully updated");
        }
        // DELETE: api/application/{id}
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> DeleteApplication([FromRoute] int id)
        {
            var deletedApplication = await _applicationRepository.DeleteApplicationAsync(id);
            if (deletedApplication == null)
            {
                return NotFound();
            }
            return Ok("Successfully deleted");
        }
        
    }
}