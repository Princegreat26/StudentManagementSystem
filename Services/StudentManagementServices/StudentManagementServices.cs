using StudentManagementSystem.Models;
using StudentManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StudentManagementSystem.DTOs;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Services.StudentManagementServices
{
    public class StudentManagementServices : IStudentManagementServices
    {
        private readonly StudentDataContext _context;

        public StudentManagementServices(StudentDataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddStudent(UserDTO user)
        {
            var getDeptId = await _context.Departments.Where(x => x.Name == user.Department).FirstOrDefaultAsync();

            if (getDeptId == null)
            {
                return false;
            }

            var student = new Student
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                RegistrationNumber = GenerateRegistrationNumber(),
                DepartmentsId = getDeptId.Id
            };
            await _context.StudentManagementSyt.AddAsync(student);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<List<ResponseDTO>> GetAllStudents()
        {

            var finalList = new List<ResponseDTO>();
            var allStudents = await _context.StudentManagementSyt.Include(x => x.Department).ToListAsync();

            foreach (var item in allStudents)
            {
              //  var dept = await _context.Departments.Where(x => x.Id == item.DepartmentsId).FirstOrDefaultAsync();

                var singleList = new ResponseDTO
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    LastName = item.LastName,
                    EmailAddress = item.EmailAddress,
                    Address = item.Address,
                    PhoneNumber = item.PhoneNumber,
                    RegisterationNumber = item.RegistrationNumber,
                    Department = item.Department.Name
                };

                finalList.Add(singleList);
            }
            return finalList;
        }

        public async Task<List<DepartmentDTO>> GetAllDepartments()
        {
            var department = await _context.Departments.Select(x => new DepartmentDTO
            {
                Id = x.Id,
                Name = x.Name,
                Abbreviation = x.Abbreviation
            }).ToListAsync();
            return department;
        }

        public async Task<ResponseDTO> GetSingleStudent(string regNumber)
        {
            var student = await _context.StudentManagementSyt.Include(x => x.Department).FirstOrDefaultAsync(x => x.RegistrationNumber == regNumber);
            if (student == null)
            {
                return null;
            }

            //var dept = await _context.Departments.Where(x => x.Id == student.DepartmentsId).FirstOrDefaultAsync();

            var studentDTO = new ResponseDTO
            {
                Id = student.Id,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                EmailAddress = student.EmailAddress,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                Department = student.Department.Name,
                RegisterationNumber = student.RegistrationNumber
            };

            return studentDTO;
        }

        public async Task<PrivateDTO> GetMoreInfo(int id)
        {
            var student = await _context.StudentManagementSyt.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                return null;
            }

            //var dept = await _context.Departments.Where(x => x.Id == student.DepartmentsId).FirstOrDefaultAsync();

            var studentDTO = new PrivateDTO
            {
                Id = student.Id,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                EmailAddress = student.EmailAddress,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                Department = student.Department.Name,
                RegisterationNumber = student.RegistrationNumber
            };

            return studentDTO;
        }

        public async Task<object> UpdateStudent(int id, UpdateUserDTO updateUserDTO)
        {
            var student = await _context.StudentManagementSyt.Where(x => x.Id == id).Include(z => z.Department).FirstOrDefaultAsync();

            student.FirstName = student.FirstName.ToLower() == updateUserDTO.FirstName.ToLower().Trim() ? student.FirstName : updateUserDTO.FirstName;
            student.MiddleName = student.MiddleName.ToLower() == updateUserDTO.MiddleName.ToLower().Trim() ? student.MiddleName : updateUserDTO.MiddleName;
            student.LastName = student.LastName.ToLower() == updateUserDTO.LastName.ToLower().Trim() ? student.LastName : updateUserDTO.LastName;
            student.EmailAddress = student.EmailAddress.ToLower() == updateUserDTO.EmailAddress.ToLower().Trim() ? student.EmailAddress : updateUserDTO.EmailAddress;
            student.Address = student.Address.ToLower() == updateUserDTO.Address.ToLower().Trim() ? student.Address : updateUserDTO.Address;
            student.PhoneNumber = student.PhoneNumber.ToLower() == updateUserDTO.PhoneNumber.ToLower().Trim() ? student.PhoneNumber : updateUserDTO.PhoneNumber;
            student.DepartmentsId = student.DepartmentsId == updateUserDTO.Department ? student.DepartmentsId : updateUserDTO.Department;

            _context.StudentManagementSyt.Update(student);
            await _context.SaveChangesAsync();
            return new { message = "Student successfully updated" };
        }

        public async Task<object> RemoveStudent(int id)
        {
            var student = await _context.StudentManagementSyt.FirstOrDefaultAsync(x => x.Id == id);
            _context.StudentManagementSyt.Remove(student);
            await _context.SaveChangesAsync();
            return new { message = "Student successfully removed" };
        }

        private string GenerateRegistrationNumber()
        {
            var now = DateTime.Now;
            var year = now.Year.ToString().Substring(2); // Last two digits of the year
            var month = now.Month.ToString("D2"); // Two digits of the month
            var day = now.Day.ToString("D2"); // Two digits of the day
            var minute = now.Minute.ToString("D2"); // Two digits of the minutes
            var second = now.Second.ToString("D2"); // Two digits of the seconds
            var random = new Random().Next(1000, 10000).ToString(); // Four random numbers

            return $"{year}{month}{day}{minute}{second}{random}";
        }
    }
}

