using System;

namespace Address_Book.Models
{
    public class Message
    {
        public Message(Exception exception)
        {
            Exception = exception;
        }

        public Message(string friendlyMessage)
        {
            FriendlyMessage = friendlyMessage;
        }

        public Exception Exception { get; set; }

        public string ExceptionMessage
        {
            get { return Exception != null ? Exception.Message : string.Empty; }
        }

        public string FriendlyMessage { get; set; }

        public string Display
        {
            get
            {
                return string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            !string.IsNullOrWhiteSpace(FriendlyMessage)
                                ? FriendlyMessage
                                : ExceptionMessage);
            }
        }
    }
}