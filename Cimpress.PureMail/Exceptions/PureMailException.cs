namespace InvoiceDataStore.BL.Clients.PureMail
{
    using System;

    public class PureMailException : Exception
    {
        private readonly string additional;

        public PureMailException(string message, string additional)
            : base(message)
        {
            this.additional = additional;
        }

        public string AdditionInfo()
        {
            return this.additional;
        }
    }
}
