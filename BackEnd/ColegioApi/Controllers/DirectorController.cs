using ColegioApi.DTO;
using ColegioApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColegioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        public DirectorController(IUserService userService, ICourseService
        courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var res = await _userService.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetUsers), new { id = res.Id }, res);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var res = await _userService.GetAllUsersAsync();
            return Ok(res);
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("courses")]
        public async Task<IActionResult> CreateCourse(CreateCourseDto dto)
        {
            var res = await _courseService.CreateCourseAsync(dto);
            return CreatedAtAction(nameof(GetCourses), new { id = res.Id }, res);
        }

        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses()
        {
            var res = await _courseService.GetAllCoursesAsync();
            return Ok(res);
        }

        [HttpDelete("courses/{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}
