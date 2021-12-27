using System.Threading.Tasks;

namespace API.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string v1, string v2, string v3);
    }
}
