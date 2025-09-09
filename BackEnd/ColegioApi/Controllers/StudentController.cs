using ColegioApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColegioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IUserRepository _userRepo;
        public StudentController(ICourseService courseService, IUserRepository
        userRepo)
        {
            _courseService = courseService;
            _userRepo = userRepo;
        }

        [HttpGet("{studentId}/courses")]
        public async Task<IActionResult> GetCourses(Guid studentId)
        {
            var courses = await _courseService.GetAllCoursesAsync();
            var filtered = courses.Where(c => c.StudentIds.Contains(studentId));
            return Ok(filtered);
        }


    }
}
