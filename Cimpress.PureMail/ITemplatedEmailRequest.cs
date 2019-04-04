using System.Threading.Tasks;

namespace Cimpress.PureMail
{
    public interface ITemplatedEmailRequest
    {
        ITemplatedEmailRequest SetTemplateId(string templateId);

        Task<IPureMailResponse> Send<TO>(TO payload);
    }
}