using AutoBill.Email;
using System.Threading.Tasks;

namespace AutoBill.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendAsync(IEmailConfiguration emailConfiguration, EmailMessage emailMessage);
    }
}
