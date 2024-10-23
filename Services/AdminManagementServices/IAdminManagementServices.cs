using StudentManagementSystem.DTOs;

namespace StudentManagementSystem.Services.AdminManagementServices
{
    public interface IAdminManagementServices
    {
        Task<bool> SignUp(SignUpDTO adminSignUp);

        Task<string> Login(LoginDTO adminLogin);
    }
}
