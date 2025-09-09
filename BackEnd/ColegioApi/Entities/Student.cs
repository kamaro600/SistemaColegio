namespace ColegioApi.Entities
{
    public class Student : User
    {      
        public string? Grade { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new
        List<Enrollment>();
    }

}
