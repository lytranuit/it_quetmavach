﻿using System.Runtime.Serialization;

namespace it.Areas.Admin.Controllers
{
    [Serializable]
    internal class DuplicateKeyException : Exception
    {
        public DuplicateKeyException()
        {
        }

        public DuplicateKeyException(string? message) : base(message)
        {
        }

        public DuplicateKeyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DuplicateKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}