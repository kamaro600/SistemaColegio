using ColegioApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ColegioApi.Repositories
{
  
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;
        public DbSet<Attendance> Attendances { get; set; } = null!;
    }
    public static class DbSeeder
    {
        public static void Seed(SchoolDbContext db)
        {
            if (db.Courses.Any()) return;
            var t1 = new Teacher
            {
                FirstName = "Ana",
                LastName = "Pérez",
                Email = "ana@school.test",
                Subject = "Matemáticas"
            };
            var s1 = new Student
            {
                FirstName = "Luis",
                LastName = "Gonzales",
                Email = "luis@school.test",
                Grade = "3A"
            };
            var s2 = new Student
            {
                FirstName = "María",
                LastName = "Quispe",
                Email = "maria@school.test",
                Grade = "3A"
            };
            var c1 = new Course
            {
                Name = "Matemáticas 3A",
                Schedule = "Lun 08:00 - 09:30",
                Teacher = t1
            };
            db.Teachers.Add(t1);
            db.Students.AddRange(s1, s2);
            db.Courses.Add(c1);
            db.Enrollments.Add(new Enrollment { Course = c1, Student = s1 });
            db.Enrollments.Add(new Enrollment { Course = c1, Student = s2 });
            db.SaveChanges();
        }
    }

}
