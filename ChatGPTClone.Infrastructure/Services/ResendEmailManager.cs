using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.Email;
using Resend;
using System.Web;

namespace ChatGPTClone.Infrastructure.Services
{
    public class ResendEmailManager : IEmailService
    {
        private readonly IResend _resend;

        private static string? _emailTemplate;

        public ResendEmailManager(IResend resend, IEnvironmentService environmentService)
        {
            _resend = resend;

            if (string.IsNullOrEmpty(_emailTemplate))
                _emailTemplate = File.ReadAllText(Path.Combine(environmentService.WebRootPath, "email-templates", "email-verification-template.html"));
        }

        public  Task EmailVerificationAsync(EmailVerificationDto emailVerificationDto, CancellationToken cancellationToken)
        {
            string html = _emailTemplate;

            var emailTitle = "E-Posta Doğrulama İşlemi - ChatGPTClone";

            html = html.Replace("{{title}}", emailTitle);
            html = html.Replace("{{greetings}}", "Merhaba Seni Aramızda Gördüğümüz İçin Mutluyuz.");

            html = html.Replace("{{message}}", "E-Posta doğrulama işleminizi tamamlamak için aşağıdaki linke tıklayınız.");

            html = html.Replace("{{verifyButtonText}}", "E-Posta Doğrula");

            var token = HttpUtility.UrlEncode(emailVerificationDto.Token);

            var emailVerificationUrl = $"https://localhost:7103/verify-email?email={emailVerificationDto.Email}&token={token}";

            html = html.Replace("{{verifyButtonLink}}", emailVerificationUrl);


            var message = new EmailMessage();
            message.From = "onboarding@resend.dev";
            message.To.Add(emailVerificationDto.Email);
            message.Subject = emailTitle;
            message.HtmlBody = html;

            return  _resend.EmailSendAsync(message);
        }
    }
}
