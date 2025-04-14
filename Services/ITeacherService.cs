using Registeration.DTOs;

namespace Registeration.Services
{
    public interface ITeacherService
    {
        Task<TeacherDto?> GetTeacherBySocNumber(string socNumber);
    }
}
