using ColegioApi.Entities;

namespace ColegioApi.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
    }
}
