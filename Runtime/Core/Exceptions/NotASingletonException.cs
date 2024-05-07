using System;

namespace BaseTool
{
    internal class NotASingletonException : Exception
    {
        public NotASingletonException() : base() { }

        public NotASingletonException(string message) : base(message) { }
    }
}
