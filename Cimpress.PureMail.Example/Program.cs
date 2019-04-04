using System;
using Cimpress.PureMail;

namespace Example
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Provide access token as argument");
                return;
            }
            
            Console.WriteLine("Trying to request email");

            var pureMailClient = new PureMailClient();

            var pureMailResponse = pureMailClient.TemplatedEmail(args[0])
                .SetTemplateId("demo-template")
                .Send(new
                {
                    property= "asd"
                }).Result;

            Console.WriteLine($"Sent {pureMailResponse.RequestId}");
        }
    }
}