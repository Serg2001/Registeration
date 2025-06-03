namespace Registeration.Models
{
    public class CRMPupil
    {
        public Guid Id { get; set; } // Optional DB PK
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
    }

}
