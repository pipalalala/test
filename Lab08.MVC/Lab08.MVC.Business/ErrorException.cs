using System;
using System.Runtime.Serialization;

namespace Lab08.MVC.Business
{
    [Serializable]
    public class ErrorException : Exception
    {
        public ErrorException()
        {
        }

        public ErrorException(string message)
            : base(message)
        {
        }

        public ErrorException(Exception exception)
            : base(exception.Message)
        {
        }

        public ErrorException(string message, Exception exception)
            : base(message + "\n" + exception.Message)
        {
        }

        protected ErrorException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}