using UnityEngine;

namespace BaseTool
{
    public class MessageAttribute : PropertyAttribute
    {
        public readonly string Message;
        public readonly MessageType Type;

        public enum MessageType
        {
            None,
            Info,
            Warning,
            Error
        }

        public MessageAttribute(string message, MessageType type = MessageType.Info)
        {
            Message = message;
            Type = type;
        }
    }

    public class InfoMessageAttribute : MessageAttribute
    {
        public InfoMessageAttribute(string message) : base(message, MessageType.Info) { }
    }

    public class WarningMessageAttribute : MessageAttribute
    {
        public WarningMessageAttribute(string message) : base(message, MessageType.Warning) { }
    }

    public class ErrorMessageAttribute : MessageAttribute
    {
        public ErrorMessageAttribute(string message) : base(message, MessageType.Error) { }
    }
}
