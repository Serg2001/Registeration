using Registeration.DTOs.OtherPupilDTOs;
using Registeration.DTOs.OtherTeacherDTOs;
using Registeration.DTOs.SchoolDTOs;

namespace Registeration.Services.FormStateServices
{
    public class FormStateService
    {
        public RegistrationFormDTO FormModel { get; set; } = new();

        public OtherPupilDTO OtherPupilFormModel { get; set; } = new();

        public OtherTeacherDTO OtherTeacherFormModel { get; set; } = new();
    }
}
