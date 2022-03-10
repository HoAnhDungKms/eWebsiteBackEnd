using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Exceptions
{
    public class eException : Exception
    {
        public eException()
        {
        }

        public eException(string message)
            : base(message)
        {
        }

        public eException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
