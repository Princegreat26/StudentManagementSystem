using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DTOs;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using StudentManagementSystem.Services.StudentManagementServices;
using System.Net.Mail;
using StudentManagementSystem.Services.EmailServices;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentManagementController : ControllerBase
    {
        private readonly IStudentManagementServices _studentManagementServices;
        private readonly IEmailServices _emailService;

        public StudentManagementController(IStudentManagementServices studentManagementServices, IEmailServices emailService)
        {
            _studentManagementServices = studentManagementServices;
            _emailService = emailService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentManagementServices.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("department")]
        [EnableCors("AllowSpecificOrigin")] // added this one
        public async Task<IActionResult> GetAllDepartments()
        {
            var department = await _studentManagementServices.GetAllDepartments();
            return Ok(department);
        }

        [Authorize]
        [HttpGet("{regNumber}")]
        public async Task<IActionResult> GetSingleStudent(string regNumber)
        {
            var result = await _studentManagementServices.GetSingleStudent(regNumber);
            if (result == null)
                return NotFound("Student doesn't exist in the database");
            return Ok(result);
        }

        [Authorize]
        [HttpGet("student/{id}")]
        public async Task<IActionResult> GetMoreInfo([FromRoute]int id)
        {
            var result = await _studentManagementServices.GetMoreInfo(id);
            if (result is null)
                return BadRequest("Couldn't load the student data");
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [EnableCors("AllowSpecificOrigin")] // added this one
        public async Task<IActionResult> AddStudent(UserDTO user)
        {
            var result = await _studentManagementServices.AddStudent(user);

            if (result)
            {
                // Send email to the student
                var emailSubject = "Welcome To Net University";
                var emailBody = _emailService.GetEmailTemplate(user.FirstName, user.LastName, user.RegistrationNumber, user.Department);

                await _emailService.SendEmailAsync(user.EmailAddress, emailSubject, emailBody);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            var result = await _studentManagementServices.UpdateStudent(id,updateUserDTO);
            if (result == null)
                return NotFound(new { message = "Student doesn't exist in the database" });
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStudent(int id)
        {
            {
                var result = await _studentManagementServices.RemoveStudent(id);
                if (result == null)
                    return NotFound(new { message = "Student doesn't exist in the database" });
                return Ok(result);
            }
        }
    }
}