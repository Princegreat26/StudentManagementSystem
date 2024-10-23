using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DTOs;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services.StudentManagementServices
{
    public interface IStudentManagementServices
    {
        Task<List<ResponseDTO>> GetAllStudents();

        Task<ResponseDTO> GetSingleStudent(string regNumber);

        Task<List<DepartmentDTO>> GetAllDepartments();

        Task<PrivateDTO> GetMoreInfo(int id);

        Task<bool> AddStudent(UserDTO user);

        Task<object> UpdateStudent(int id, UpdateUserDTO updateUserDTO);

        Task<object> RemoveStudent(int id);
    }
}