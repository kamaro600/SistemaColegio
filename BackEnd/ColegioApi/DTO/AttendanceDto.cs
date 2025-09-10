namespace ColegioApi.DTO
{
    public class AttendanceDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool Present { get; set; }
    }
}
