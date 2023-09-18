using System;

namespace AcmeStudios.ApiRefactor
{
    public class ItemDeleteException : Exception
    {
        public ItemDeleteException()
        {
        }

        public ItemDeleteException(string message)
            : base(message)
        {
        }

        public ItemDeleteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
