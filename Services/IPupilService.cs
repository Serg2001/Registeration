using Registeration.DTOs;

namespace Registeration.Services
{
    public interface IPupilService
    {
        Task<PupilInfo?> GetPupilBySocNumber(string SocNumber);
    }
}
