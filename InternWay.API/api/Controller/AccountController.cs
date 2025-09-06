using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interface;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };
                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password!);
                if (!createUser.Succeeded)
                {
                    return BadRequest(createUser.Errors);
                }

                //Assign role to user
                var role = registerDto.Role!.ToLower() == "recruiter" ? "Recruiter" : "Student";
                var roleResult = await _userManager.AddToRoleAsync(appUser, role);
                if (!roleResult.Succeeded)
                {
                    return BadRequest(roleResult.Errors);
                }

                //Creating profile depending on role
                if (role == "Student")
                {
                    appUser.StudentProfile = new StudentProfile
                    {
                        AppUserId = appUser.Id,
                        FullName = "",
                        Bio = "",

                    };
                }
                else
                {
                    appUser.Recruiter = new Recruiter
                    {
                        AppUserId = appUser.Id,
                        CompanyName = "",
                    };
                }
                await _userManager.UpdateAsync(appUser);

                return Ok(new NewUserDto
                {
                    Username = appUser.UserName,
                    Token = await _tokenService.CreateToken(appUser),
                    Role = role
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        // [HttpPost("Login")]


        
    }
}