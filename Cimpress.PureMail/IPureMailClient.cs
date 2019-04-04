namespace Cimpress.PureMail
{
    public interface IPureMailClient
    {
        ITemplatedEmailRequest TemplatedEmail(string accessToken);
    }
}
