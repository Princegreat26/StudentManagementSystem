global using Microsoft.EntityFrameworkCore;
global using StudentManagementSystem.Models;
namespace StudentManagementSystem.Data
{
    public class StudentDataContext : DbContext
    {
        public StudentDataContext(DbContextOptions<StudentDataContext> options) : base(options)
        {

        }

        public DbSet<Student> StudentManagementSyt { get; set; }

        public DbSet<AdminManagement> AdminManagementSyt {  get; set; }

        public DbSet<Department> Departments {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


           modelBuilder.Entity<Department>().HasKey(x => x.Id);
            modelBuilder.Entity<AdminManagement>().HasKey(x => x.Id);
            //relationships
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Department).WithMany(x => x.Students).HasForeignKey(z => z.DepartmentsId);
            });


            /* // Seed data
             modelBuilder.Entity<Student>().HasData(
                 new Student
                 {
                     Id = 1,
                     FirstName = "McKenzie",
                     MiddleName = "Briar",
                     LastName = "Pennington",
                     Address = "6178 Nitzsche Passage, New Dorthafurt, HI 04115",
                     PhoneNumber = "49793612607891",
                     EmailAddress = "Eula_Rowe74@gmail.com",
                     RegistrationNumber = "2024123456"
                 },
                  new Student
                  {
                      Id = 2,
                      FirstName = "Carson",
                      MiddleName = "Kaison",
                      LastName = "Butler",
                      Address = "9260 Degré De la Gare, 22161 Palaiseau",
                      PhoneNumber = "335299203663",
                      EmailAddress = "Josue_Weissnat78@gmail.com",
                      RegistrationNumber = "2024654321"
                  }
             );
 */
            //Seed data
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "Artificial Intelligence",
                    Abbreviation = "AI"
                }, new Department
                {
                    Id = 2,
                    Name = "Fashion Design",
                    Abbreviation = "FD"
                }, new Department
                {
                    Id = 3,
                    Name = "Machine Learning",
                    Abbreviation = "ML"
                }, new Department
                {
                    Id = 4,
                    Name = "Fashion Merchandising",
                    Abbreviation = "FM"
                }, new Department
                {
                    Id = 5,
                    Name = "Web Development",
                    Abbreviation = "WD"
                }, new Department
                {
                    Id = 6,
                    Name = "Fashion Photography",
                    Abbreviation = "FP"
                }, new Department
                {
                    Id = 7,
                    Name = "Software Engineering",
                    Abbreviation = "SE"
                }, new Department
                {
                    Id = 8,
                    Name = "Fashion Styling",
                    Abbreviation = "FS"
                }, new Department
                {
                    Id = 9,
                    Name = "Cyber Security",
                    Abbreviation = "CS"
                }, new Department
                {
                    Id = 10,
                    Name = "Fashion Journalism",
                    Abbreviation = "FJ"
                }, new Department
                {
                    Id = 11,
                    Name = "Mobile App Development",
                    Abbreviation = "MAD"
                }, new Department
                {
                    Id = 12,
                    Name = "Fashion History",
                    Abbreviation = "FH"
                }, new Department
                {
                    Id = 13,
                    Name = "Fashion Illustration",
                    Abbreviation = "FI"
                }, new Department
                {
                    Id = 14,
                    Name = "Blockchain Technology",
                    Abbreviation = "BT"
                }, new Department
                {
                    Id = 15,
                    Name = "Fashion Business",
                    Abbreviation = "FB"
                }
                );
        }
    }
}
