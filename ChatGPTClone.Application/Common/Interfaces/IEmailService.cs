namespace ChatGPTClone.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task EmailVerificationAsync(CancellationToken token);
    }
}
