using System;

namespace AcmeStudios.ApiRefactor
{
    public class ItemUpdateException : Exception
    {
        public ItemUpdateException()
        {
        }

        public ItemUpdateException(string message)
            : base(message)
        {
        }

        public ItemUpdateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
