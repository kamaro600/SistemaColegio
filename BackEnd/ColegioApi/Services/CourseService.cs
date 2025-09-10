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
        public async Task RegisterAttendanceAsync(Guid courseId, List<AttendanceDto> payload)
        {
            var course = await _courseRepo.GetAsync(courseId);
            if (course == null) throw new Exception("Curso no encontrado");


            var attendancesToUpsert = payload.Select(dto => new Attendance
            {
                Id = Guid.NewGuid(),
                CourseId = courseId,
                StudentId = dto.StudentId,
                Date = dto.Date.Date,
                Present = dto.Present
            }).ToList();

            // Aquí llamamos al repositorio para que maneje la operación en lote
            await _attendanceRepo.UpsertAttendancesAsync(attendancesToUpsert);


           /* var attendance = new Attendance
            {
                CourseId = courseId,
                StudentId = studentId,
                Date = date.Date,
                Present = present
            };

            // inserción directa
            await _attendanceRepo.AddAsync(attendance);**/
        }
        public async Task DeleteCourseAsync(Guid id)
        {
            await _courseRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesByStudentIdAsync(Guid studentId)
        {
            var courses = await _courseRepo.GetAllAsync();
            var users = await _userRepo.GetAllAsync();

            var filtered = courses.Where(c => c.Enrollments.Any(e => e.StudentId == studentId));


            var coursesWithTeacherName = filtered.Select(course =>
            {
                var teacher = users.FirstOrDefault(u => u.Id == course.TeacherId);
                return new 
                {
                    course.Id,
                    course.Name,
                    course.Schedule,
                    course.TeacherId,
                    TeacherName = teacher != null ? $"{teacher.FirstName} {teacher.LastName}" : "No Asignado",
                    StudentIds = course.Enrollments.Select(e => e.StudentId).ToList()
                };
            });

            return coursesWithTeacherName.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                Schedule = c.Schedule,
                TeacherId = c.TeacherId,
                TeacherName =c.TeacherName,
                StudentIds = c.StudentIds
            }).ToList();
        }

        public async Task<IEnumerable<AttendanceDto>> GetAttendancesByCourseAsync(Guid courseId)
        {
            // Obtener las asistencias del repositorio
            var attendances = await _courseRepo.GetAttendancesByCourseAsync(courseId);

            // Obtener todos los usuarios para asociar los IDs con los nombres
            var users = await _userRepo.GetAllAsync();

            // Mapear los registros de asistencia al DTO, incluyendo el nombre del estudiante
            return attendances.Select(a =>
            {
                var student = users.FirstOrDefault(u => u.Id == a.StudentId);
                var attendanceDto = new AttendanceDto();
                attendanceDto.StudentId = a.StudentId;
                attendanceDto.StudentName = student != null ? $"{student.FirstName} {student.LastName}" : "Desconocido";
                attendanceDto.Date = a.Date;
                attendanceDto.Present = a.Present;
                return attendanceDto;
            }).ToList();
        }
    }

}
