namespace StudentManagementSystem.Services.StudentManagementServices
{
    public class Registration
    {
        private readonly DbContext _context;

        public Registration(DbContext context)
        {
            _context = context;
        }

        //public string GenerateRegistrationNumber()
        //{
        //    //string prefix = "2024";
        //    //string uniqueSuffix;
        //    //string registrationNumber;

        //    //do
        //    //{
        //    //    uniqueSuffix = new Random().Next(100000, 999999).ToString();
        //    //    registrationNumber = $"{prefix}{uniqueSuffix}";
        //    //} while (StudentManagements.Any(s => s.RegistrationNumber == registrationNumber));

        //    //return registrationNumber;
        //}
    }
}
