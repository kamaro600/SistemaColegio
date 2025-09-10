using ColegioApi.DTO;

namespace ColegioApi.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserDto dto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task DeleteUserAsync(Guid id);
    }
}
