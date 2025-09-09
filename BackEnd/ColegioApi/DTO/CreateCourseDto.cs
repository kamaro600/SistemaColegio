namespace ColegioApi.DTO
{
    public class CreateCourseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Schedule { get; set; } = string.Empty;
        public Guid? TeacherId { get; set; }
        public List<Guid> StudentIds { get; set; } = new();
    }
}
