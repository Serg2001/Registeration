namespace Registeration.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int RegionId { get; set; }
        public Region Region { get; set; }

    }
}
