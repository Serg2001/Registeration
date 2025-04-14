namespace Registeration.DTOs
{
    public class TeacherDto
    {
        public string RegionName { get; set; }
        public string SchoolName { get; set; }

        public string Name { get; set; }
        public string SurName { get; set; }
        public string MiddleName { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string MainSubject { get; set; }

        public List<string> Subjects { get; set; }
    }

}
