using System;
using System.Threading.Tasks;
using AuthenticationBase;
using AuthenticationBase.Models;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppSecurity _appSecurity;

        public AuthenticationService(UserManager<User> userManager,
            AppSecurity appSecurity)
        {
            _userManager = userManager;
            _appSecurity = appSecurity;
        }

        public async Task<ObjectResult> SignUp(SignUpModel signUpModel)
        {
            var userExists = await _userManager.FindByNameAsync(signUpModel.UserName);

            if (userExists != null)
            {
                return new ConflictObjectResult(new SignUpResponse
                {
                    Status = "Error",
                    Message = "User exists"
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
                    Message = "User creation failed!"
                });
            }

            return new OkObjectResult(new SignUpResponse
            {
                Status = "Success",
                Message = "User created successfully!"
            });
        }

        public async Task<ObjectResult> Login(LoginModel loginModel)
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

            var token = _appSecurity.GetToken(user.Id, DateTime.Now, userRoles);

            return new OkObjectResult(new LoginResponse
            {
                UserName = loginModel.UserName,
                Token = token
            });
        }
    }
}
