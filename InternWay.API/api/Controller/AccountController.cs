using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interface;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName!.ToLower());
            if (user == null)
            {
                return Unauthorized("Invalid user");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid credentials");
            }

            var RefreshToken = _tokenService.GenerateRefreshToken(HttpContext.Connection.RemoteIpAddress!.ToString());

            

            user.RefreshTokens.Add(RefreshToken);
            await _userManager.UpdateAsync(user);

            Response.Cookies.Append("refreshToken", RefreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Expires = RefreshToken.Expires
            });
            return Ok(new NewUserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()  //ContinueWith(t => t.Result.FirstOrDefault())
            });
        }

        //Returning new access token, by validating and rotating refresh token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = await _userManager.Users.Include(u => u.RefreshTokens)
                                                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(i => i.Token == refreshToken));

            if (user == null)
            {
                return Unauthorized("Invalid Refresh Token");
            }

            var token = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);
            if (!token!.IsActive)
            {
                return Unauthorized("Token is not Active, Expired/Revoked");
            }

            //Now we Rotate the Refresh Token {assigning the new one for extra security}

            var remoteIp = HttpContext.Connection.RemoteIpAddress!.ToString();

            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = remoteIp;

            var newRefreshToken = _tokenService.GenerateRefreshToken(remoteIp);
            user.RefreshTokens.Add(newRefreshToken);

            await _userManager.UpdateAsync(user);
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, new CookieOptions
            {
                // Secure = true,
                // SameSite = SameSiteMode.Strict,
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            });

            var newAccessToken = await _tokenService.CreateToken(user);

            return Ok(new
            {
                Token = newAccessToken
            });
        }
        
    }
}