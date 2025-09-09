namespace ColegioApi.DTO
{
    public class AttendanceDto
    {
        public Guid StudentId { get; set; }
        public DateTime Date { get; set; }
        public bool Present { get; set; }
    }
}
