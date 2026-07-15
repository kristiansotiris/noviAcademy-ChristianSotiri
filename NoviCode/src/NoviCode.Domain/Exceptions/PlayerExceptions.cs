using System;
using System.Collections.Generic;
using System.Text;

namespace NoviCode.Exceptions
{
    public abstract class PlayerExceptions : Exception
    {
        public abstract string ErrorCode { get; }

        public PlayerExceptions(string message) : base(message){ }
        public PlayerExceptions(string message, Exception exception) : base(message, exception){ }

    }
}
