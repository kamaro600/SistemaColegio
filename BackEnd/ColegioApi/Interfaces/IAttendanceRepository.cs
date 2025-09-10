using ColegioApi.Entities;

namespace ColegioApi.Interfaces
{
    public interface IAttendanceRepository: IRepository<Attendance>
    {
        Task<Attendance?> GetByCourseAndStudentAsync(Guid courseId, Guid studentId, DateTime date);
        Task UpsertAttendancesAsync(List<Attendance> attendances);
    }
}
