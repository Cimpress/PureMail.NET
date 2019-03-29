# PureMail.NET

This is a client package making use of Stereotype service easier from C# code base.

## Usage

Install the package

    dotnet add package PureMail.NET
    
and make sure to add the import    

    using Cimpress.PureMail;

and then 

    var pureMailClient = new PureMailClient();
    pureMailClient.SendTemplatedEmail(args[0], "demo-template", new {
        property = "customised-value"
    });s
            