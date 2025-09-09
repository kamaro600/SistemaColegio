namespace ColegioApi.Entities
{
    public class Enrollment
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
        public Guid StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
