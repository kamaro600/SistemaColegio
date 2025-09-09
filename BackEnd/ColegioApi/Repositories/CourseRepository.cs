using ColegioApi.Entities;
using ColegioApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ColegioApi.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolDbContext _db;
        public CourseRepository(SchoolDbContext db) => _db = db;
        public async Task AddAsync(Course entity)
        {
            await  _db.Courses.AddAsync(entity); 
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var c = await _db.Courses.FindAsync(id);
            if (c != null)
            {
                _db.Courses.Remove(c); await
                _db.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Course>> GetAllAsync() => await _db.Courses.Include(c => c.Enrollments).ToListAsync();
        public async Task<Course?> GetAsync(Guid id) => await _db.Courses.FindAsync(id);
        public async Task<Course?> GetWithDetailsAsync(Guid id) => await _db.Courses.Include(c => c.Enrollments).ThenInclude(e =>  e.Student).Include(c => c.Teacher).FirstOrDefaultAsync(c => c.Id == id);
        public async Task UpdateAsync(Course entity)
        {
            _db.Courses.Update(entity); await _db.SaveChangesAsync();
        }
    }

}
