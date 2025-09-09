using ColegioApi.Entities;
using ColegioApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ColegioApi.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly SchoolDbContext _context;

        public AttendanceRepository(SchoolDbContext context)
        {
            _context = context;
        }

        // Insertar nueva asistencia
        public async Task AddAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }

        // Actualizar asistencia existente
        public async Task UpdateAsync(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();
        }

        // Buscar asistencia por curso, estudiante y fecha
        public async Task<Attendance?> GetByCourseAndStudentAsync(Guid courseId, Guid studentId, DateTime date)
        {
            return await _context.Attendances
                .FirstOrDefaultAsync(a =>
                    a.CourseId == courseId &&
                    a.StudentId == studentId &&
                    a.Date == date.Date);
        }

        public Task<Attendance?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return null;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
