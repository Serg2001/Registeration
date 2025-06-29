namespace Registeration.DTOs.SchoolDTOs
{
    public class SchoolDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Guid RegionId { get; set; }
        public bool IsRegistered { get; set; } = false;
    }
}
