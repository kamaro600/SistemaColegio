namespace ColegioApi.Entities
{
    public class Teacher : User
    {
        public string? Subject { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

}
