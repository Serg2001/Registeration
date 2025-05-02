namespace Registeration.DTOs
{
    public class SchoolDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int RegionId { get; set; }
    }
}
