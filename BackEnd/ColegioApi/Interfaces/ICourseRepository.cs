using ColegioApi.Entities;

namespace ColegioApi.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course?> GetWithDetailsAsync(Guid id);
    }
}
