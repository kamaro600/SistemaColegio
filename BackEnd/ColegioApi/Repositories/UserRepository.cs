using ColegioApi.Entities;
using ColegioApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ColegioApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SchoolDbContext _db;
        public UserRepository(SchoolDbContext db) => _db = db;
        public async Task AddAsync(User entity)
        {
            switch (entity)
            {
                case Student s: await _db.Students.AddAsync(s); break;
                case Teacher t: await _db.Teachers.AddAsync(t); break;
                default: await _db.Users.AddAsync(entity); break;
            }
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            // Paso 1: Encuentra el usuario a eliminar.
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return;
            }

            // Paso 2: Carga explícitamente las entidades relacionadas según el tipo de usuario.
            if (user is Teacher teacher)
            {
                // Cargar los cursos de este profesor
                await _db.Entry(teacher)
                              .Collection(t => t.Courses)
                              .LoadAsync();

                // Desvincular cursos
                foreach (var course in teacher.Courses)
                {
                    course.TeacherId = null;
                }
            }

            if (user is Student student)
            {
                // Cargar las inscripciones de este estudiante
                await _db.Entry(student)
                              .Collection(s => s.Enrollments)
                              .LoadAsync();

                // Eliminar las inscripciones de la tabla intermedia
                _db.Enrollments.RemoveRange(student.Enrollments);
            }

            // Paso 3: Elimina al usuario y guarda los cambios
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<User>> GetAllAsync() => await _db.Users.ToListAsync();
        public async Task<User?> GetAsync(Guid id) => await _db.Users.FindAsync(id);
        public async Task UpdateAsync(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<Student>> GetAllStudentsAsync() => await _db.Students.Include(s => s.Enrollments).ToListAsync();
        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync() => await _db.Teachers.Include(t => t.Courses).ToListAsync();
    }

}
