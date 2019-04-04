namespace Cimpress.PureMail
{
    public class PureMailClientOptions
    {
        public string ServiceBaseUrl { get; set; }
        
        public string AcceptPreference { get; set; }

        public PureMailClientOptions()
        {
            AcceptPreference = "image/*;q=0.1,application/pdf;q=0.95,application/links+json;q=0.9,application/hal+json;q=0.8,application/json;q=0.7,*/*;q=0.6";
            ServiceBaseUrl = "https://puremail.trdlnk.cimpress.io";
        }
    }
}
