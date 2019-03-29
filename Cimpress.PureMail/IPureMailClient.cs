using System.Threading.Tasks;

namespace Cimpress.PureMail
{
    public interface IPureMailClient
    {
        Task SendTemplatedEmail<T>(string accessToken, string templateId, T payload);
    }
}
