using System;
using System.Runtime.Serialization;

namespace TesteBackEndWebMotors.Library.CustomException
{
    public class NossaException : Exception
    {
        public NossaException()
        {
        }

        public NossaException(string message) : base(message)
        {
        }

        public NossaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NossaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
