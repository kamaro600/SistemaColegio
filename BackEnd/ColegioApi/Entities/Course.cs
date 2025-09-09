namespace ColegioApi.Entities
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Schedule { get; set; } = string.Empty; // e.g. "Mon08:00-09:30"
        public Guid? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new  List<Enrollment>();
        public ICollection<Attendance> Attendances { get; set; } = new  List<Attendance>();

    }
}
