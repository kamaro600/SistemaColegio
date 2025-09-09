namespace ColegioApi.Entities
{
    public class Attendance
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
        public Guid StudentId { get; set; }
        public Student? Student { get; set; }
        public DateTime Date { get; set; }
        public bool Present { get; set; }
    }
}
