using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    [ApiController] 
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var userExists = await _userManager.FindByNameAsync(signUpModel.UserName);

            if (userExists != null)
            {
                return new ConflictObjectResult(new SignUpResponse 
                    { 
                        Status = "Error",
                        Message = "User exist"
                    });
            }

            var user = new User
            {
                Email = signUpModel.Email,
                UserName = signUpModel.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, signUpModel.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new SignUpResponse
                {
                    Status = "Error",
                    Message = "User creation failed"
                });
            }

            return new OkObjectResult(new SignUpResponse
            {
                Status = "Success",
                Message = "User created successfully!"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                return new UnauthorizedObjectResult(new LoginResponse
                {
                    UserName = loginModel.UserName
                });
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(userRoles.Select(UserRole => new Claim(ClaimTypes.Role, UserRole)));

            var authLoginKEy = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Security:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Security:Issuer"],
                audience: _configuration["Security:Audience"],
                expires: DateTime.Now.AddHours(24),
                claims: claims,
                signingCredentials: new SigningCredentials(authLoginKEy, SecurityAlgorithms.HmacSha256)
                );
            return new OkObjectResult(new LoginResponse
            {
                UserName = loginModel.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
