using Registeration.Enums;

namespace Registeration.DTOs.CrmDTOs
{
    public class PupilCheckDTO
    {
        public Guid SchoolId { get; set; }
        public EGrade Grade { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;

    }
}
