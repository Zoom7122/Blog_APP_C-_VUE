using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Exceptions
{
    public class RegisterException : Exception
    {
        public RegisterException() { }
        public RegisterException(string message) : base(message) { }
        public RegisterException(string message, Exception inner) : base(message, inner) { }
    }
}
