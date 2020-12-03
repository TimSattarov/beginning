using AuthenticationService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationService.Services
{
    public interface IAuthenticationService
    {
        Task<ObjectResult> SignUp(SignUpModel signUpModel);
        Task<ObjectResult> Login(LoginModel loginModel);
    }
}
