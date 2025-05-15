namespace Registeration.DTOs
{
    public class SchoolDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Guid RegionId { get; set; }
    }
}
