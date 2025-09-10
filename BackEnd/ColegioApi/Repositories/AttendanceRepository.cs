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

        public async Task<Attendance?> GetAsync(Guid id)
        {
            return await _context.Attendances.FindAsync(id);
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _context.Attendances.ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var c = await _context.Attendances.FindAsync(id);
            if (c != null)
            {
                _context.Attendances.Remove(c);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpsertAttendancesAsync(List<Attendance> attendances)
        {
            var today = DateTime.UtcNow.Date;

            foreach (var attendance in attendances)
            {
                var existingAttendance = await _context.Attendances
                    .FirstOrDefaultAsync(a => a.CourseId == attendance.CourseId &&
                                              a.StudentId == attendance.StudentId &&
                                              a.Date.Date == today);

                if (existingAttendance != null)
                {
                    existingAttendance.Present = attendance.Present;
                }
                else
                {
                    _context.Attendances.Add(attendance);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
