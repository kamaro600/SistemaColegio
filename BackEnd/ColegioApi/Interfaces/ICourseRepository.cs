using ColegioApi.Entities;

namespace ColegioApi.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course?> GetWithDetailsAsync(Guid id);
        Task<IEnumerable<Attendance>> GetAttendancesByCourseAsync(Guid courseId);
    }
}
