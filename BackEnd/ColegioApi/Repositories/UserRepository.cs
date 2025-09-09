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
            var u = await _db.Users.FindAsync(id);
            if (u != null) { _db.Users.Remove(u); await _db.SaveChangesAsync(); }
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
