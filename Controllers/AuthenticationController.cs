using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagementSystem.DTOs;
using StudentManagementSystem.Services.AdminManagementServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public static AdminManagement adminManagement = new AdminManagement();

        private readonly IAdminManagementServices _adminManagementServices;

        public AuthenticationController(IAdminManagementServices adminManagementServices)
        {
            _adminManagementServices = adminManagementServices;
        }

        [HttpPost("signup")]
        [EnableCors("AllowSpecificOrigin")] // added this one
        public async Task<ActionResult<bool>> SignUp(SignUpDTO adminSignUp)
        {
            var result = await _adminManagementServices.SignUp(adminSignUp);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO admin)
        {
            var result = await _adminManagementServices.Login(admin);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("dashboard")]
        public async Task<ActionResult> Dashboard()
        {
            string name = User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            return Ok($"Authenticated {name}");
        }
    } 
}
