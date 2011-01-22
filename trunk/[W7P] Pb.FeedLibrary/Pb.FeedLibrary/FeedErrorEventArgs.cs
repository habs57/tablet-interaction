using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Pb.FeedLibrary
{
    /// <summary>
    /// Event Information of error in feed
    /// </summary>
    public class FeedErrorEventArgs : EventArgs
    {
        public FeedErrorEventArgs(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }

        public string Message
        {
            get;
            private set;
        }

        public Exception Exception
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return string.Format("{0}\n{1}", Message, Exception.ToString());
        }
    }
}
