using System;

namespace SpyStore.DAL.Exceptions
{
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException() { }
        public InvalidQuantityException(string message) : base(message) { }
        public InvalidQuantityException(string message, Exception innerException) : base(message, innerException) { }
    }
}
