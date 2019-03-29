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
            var response = pureMailClient.SendTemplatedEmail(args[0], "demo-template", new
            {
                property = "customised-value"
            });

            response.Wait();
            Console.WriteLine("Sent");
        }
    }
}