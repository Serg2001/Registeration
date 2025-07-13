

namespace Registeration.Models.CrmModels
{
    public class CRMPupil
    {
        public Guid Id { get; set; } // Optional DB PK
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
    }

}
