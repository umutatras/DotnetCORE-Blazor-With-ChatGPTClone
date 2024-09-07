﻿using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.Email;
using Resend;
using System.Web;

namespace ChatGPTClone.Infrastructure.Services
{
    public class ResendEmailManager : IEmailService
    {
        private readonly IResend _resend;
      
        private readonly string _emailTemplate=string.Empty;
        public ResendEmailManager(IResend resend, IEnvironmentService environmentService)
        {
            _resend = resend;
           
            _emailTemplate = File.ReadAllText(Path.Combine(environmentService.WebRootPath, "email-templates", "email-verification-template.html"));
        }

        public  Task EmailVerificationAsync(EmailVerificationDto emailVerificationDto, CancellationToken cancellationToken)
        {
            string html = _emailTemplate;

            var emailTitle = "E-Posta Doğrulama İşlemi - ChatGPTClone";

            html = html.Replace("{{title}}", emailTitle);

            html = html.Replace("{{message}}", "E-Posta doğrulama işleminizi tamamlamak için aşağıdaki linke tıklayınız.");

            html = html.Replace("{{verifyButtonText}}", "E-Posta Doğrula");

            var token = HttpUtility.UrlEncode(emailVerificationDto.Token);

            var emailVerificationUrl = $"www.google.com.tr/verify-email?email={emailVerificationDto.Email}&token={token}";

            html = html.Replace("{{verifyButtonLink}}", emailVerificationUrl);


            var message = new EmailMessage();
            message.From = "noreply@yazilim.academy";
            message.To.Add(emailVerificationDto.Email);
            message.Subject = emailTitle;
            message.HtmlBody = html;

            return  _resend.EmailSendAsync(message);
        }
    }
}
