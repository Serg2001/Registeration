namespace Registeration.DTOs.SchoolDTOs
{
    public class CRMPupilDTO
    {
        public Guid Id;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string AccessCode { get; set; } = string.Empty;
    }
}
