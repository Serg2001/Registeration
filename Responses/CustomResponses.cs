namespace Registeration.Responses
{
    public class CustomResponses
    {
        public record RegisterationResponse(bool Flag = false, string Message = null!);
    }
}
