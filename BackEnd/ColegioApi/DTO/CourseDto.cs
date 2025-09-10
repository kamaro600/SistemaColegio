namespace ColegioApi.DTO
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Schedule { get; set; } = string.Empty;
        public Guid? TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public List<Guid> StudentIds { get; set; } = new();
    }
}
