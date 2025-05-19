using Registeration.DTOs;

namespace Registeration.Services
{
    public class FormStateService
    {
        public RegistrationFormDTO FormModel { get; set; } = new();

        public OtherPupilDTO OtherPupilFormModel { get; set; } = new();
    }
}
