using ColegioApi.DTO;
using ColegioApi.Entities;
using ColegioApi.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ColegioApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly IAttendanceRepository _attendanceRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IUserRepository _userRepo;
        public CourseService(ICourseRepository courseRepo, IUserRepository userRepo, IAttendanceRepository attendanceRepo)
        {
            _courseRepo = courseRepo;
            _userRepo = userRepo;
            _attendanceRepo = attendanceRepo;
        }
        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto dto)
        {
            var course = new Course { Name = dto.Name, Schedule = dto.Schedule };
            if (dto.TeacherId.HasValue)
            {
                var teacher = await _userRepo.GetAsync(dto.TeacherId.Value) as
                Teacher;
                if (teacher != null) course.Teacher = teacher;
            }


            foreach (var sid in dto.StudentIds)
            {
                var student = await _userRepo.GetAsync(sid) as Student;
                if (student != null)
                {
                    course.Enrollments.Add(new Enrollment
                    {
                        Course = course,
                        Student = student
                    });
                }
            }
            await _courseRepo.AddAsync(course);
            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Schedule = course.Schedule,
                TeacherId = course.TeacherId,
                StudentIds = course.Enrollments.Select(e => e.StudentId).ToList()
            };
        }
        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepo.GetAllAsync();
            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                Schedule = c.Schedule,
                TeacherId = c.TeacherId,
                StudentIds = c.Enrollments.Select(e => e.StudentId).ToList()
            }).ToList();
        }
        public async Task RegisterAttendanceAsync(Guid courseId, Guid studentId, DateTime date, bool present)
        {
            var course = await _courseRepo.GetAsync(courseId);
            if (course == null) throw new Exception("Curso no encontrado");

            var attendance = new Attendance
            {
                CourseId = courseId,
                StudentId = studentId,
                Date = date.Date,
                Present = present
            };

            // inserción directa
            await _attendanceRepo.AddAsync(attendance);
        }
    }

}
