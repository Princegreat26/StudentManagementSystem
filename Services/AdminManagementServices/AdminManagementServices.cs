using Microsoft.IdentityModel.Tokens;
using StudentManagementSystem.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace StudentManagementSystem.Services.AdminManagementServices
{

    public class AdminManagementServices : IAdminManagementServices
    {
        private readonly ILogger<AdminManagementServices> _logger;
        private readonly IConfiguration _configuration;
        private readonly StudentDataContext _dataContext;

        public AdminManagementServices(ILogger<AdminManagementServices> logger, IConfiguration configuration, StudentDataContext dataContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dataContext = dataContext;
        }

        public async Task<string> Login(LoginDTO request) //, string email, bool isStudent = true,int AmmountOwed = 50000000)
        {
            if (request.Username.IsNullOrEmpty() || request.Password.IsNullOrEmpty())
            {
                return "Please enter a username or password";
            }

            var findUser = await _dataContext.AdminManagementSyt.Where(x => x.Username == request.Username).FirstOrDefaultAsync();

            if (findUser == null)
            {
                return "Incorrect Username or Password!!";
            }

            if (VerifyPasswordHash(request.Password, findUser.PasswordHash, findUser.PasswordSalt))
            {
                return CreateToken(findUser);
            }

            return "Incorrect Username or Password!!";
        }

        public async Task<bool> SignUp(SignUpDTO request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var model = new AdminManagement();

            model.Name = request.Name;
            model.Username = request.Username;
            model.Email = request.Email;
            model.PasswordSalt = passwordSalt;
            model.PasswordHash = passwordHash;

            await _dataContext.AdminManagementSyt.AddAsync(model);
            await _dataContext.SaveChangesAsync();

            //var sendMail = Login(new AdminLogin(),"email",)
            ;
            return true;
        }

        private string CreateToken(AdminManagement adminManagement)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, adminManagement.Username),
                new Claim(ClaimTypes.Role, "Administrator")
            };

            var keyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            if (keyBytes.Length < 64)
            {
                throw new ArgumentException("The key must be at least 64 bytes long.");
            }

            var key = new SymmetricSecurityKey(keyBytes);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
