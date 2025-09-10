using ColegioApi.DTO;
using ColegioApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColegioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {

        private readonly ICourseService _courseService;
        public TeacherController(ICourseService courseService) => _courseService
        = courseService;
        [HttpPost("{courseId}/attendance")]
        public async Task<IActionResult> RegisterAttendance(Guid courseId, [FromBody] List<AttendanceDto> attendances)
        {
            await _courseService.RegisterAttendanceAsync(courseId, attendances);
            return NoContent();
        }

        [HttpGet("{courseId}/attendances")]
        public async Task<IActionResult> GetAttendances(Guid courseId)
        {
            var attendances = await _courseService.GetAttendancesByCourseAsync(courseId);
            return Ok(attendances);
        }
    }

}
