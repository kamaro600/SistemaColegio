using ColegioApi.DTO;

namespace ColegioApi.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto);
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task RegisterAttendanceAsync(Guid courseId, Guid studentId, DateTime
        date, bool present);
    }

}
