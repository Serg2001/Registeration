using Registeration.DTOs;

namespace Registeration.Services
{
    public interface IPupilService
    {
        Task<PupilInfo19?> GetPupilBySocNumber(string SocNumber);
    }
}
