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
            foreach (var a in attendances)
            {
                await _courseService.RegisterAttendanceAsync(courseId,
                a.StudentId, a.Date, a.Present);
            }
            return Ok();
        }

    }

}
