using System.Threading.Tasks;

namespace Cimpress.PureMail
{
    public interface ITemplatedEmailRequest
    {
        ITemplatedEmailRequest SetTemplateId(string templateId);
        
        ITemplatedEmailRequest WithWhitelistedRelation(string whiteListEntry);
        
        ITemplatedEmailRequest WithBlacklistedRelation(string blackListEntry);

        Task<IPureMailResponse> Send<TO>(TO payload);
    }
}