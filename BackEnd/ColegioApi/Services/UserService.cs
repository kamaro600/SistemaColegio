using ColegioApi.DTO;
using ColegioApi.Entities;
using ColegioApi.Interfaces;

namespace ColegioApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo) => _userRepo = userRepo;
        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            User user = dto.Role.ToLower() switch
            {
                "teacher" => new Teacher
                {
                    FirstName = dto.FirstName,
                    LastName =
                dto.LastName,
                    Email = dto.Email
                },
                _ => new Student
                {
                    FirstName = dto.FirstName,
                    LastName =
                dto.LastName,
                    Email = dto.Email
                }
            };
            await _userRepo.AddAsync(user);
            return new UserDto
            {
                Id = user.Id,
                FullName = $"{user.FirstName}{user.LastName}",
                Email = user.Email,
                Role = dto.Role
            };
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FullName = $"{u.FirstName} {u.LastName}",
                Email = u.Email,
                Role =
            u.GetType().Name.ToLower()
            }).ToList();
        }
    }

}
