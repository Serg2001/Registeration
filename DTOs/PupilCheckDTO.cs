using Registeration.Enums;

namespace Registeration.DTOs
{
    public class PupilCheckDTO
    {
        public Guid SchoolId { get; set; }
        public GradeLevel Grade { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }

    }
}
