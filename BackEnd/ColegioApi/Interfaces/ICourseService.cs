using ColegioApi.DTO;

namespace ColegioApi.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto);
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<IEnumerable<CourseDto>> GetAllCoursesByStudentIdAsync(Guid studentId);
        Task RegisterAttendanceAsync(Guid courseId, List<AttendanceDto> payload);
        Task DeleteCourseAsync(Guid id);
        Task<IEnumerable<AttendanceDto>> GetAttendancesByCourseAsync(Guid courseId);
    }

}
