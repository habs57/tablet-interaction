using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

using System.Windows.Threading;

using SimpleMVVM;

using Pb.FeedLibrary;

namespace TED7
{
    public abstract class TED7ViewModelBase : ViewModelBase
    {
        public Dispatcher Dispatcher
        {
            get;
            internal set;
        }

        private static FeedManager _FeedManager = null;
        protected FeedManager FeedManager
        {
            get
            {
                if (_FeedManager == null)
                {
                    _FeedManager = new FeedManager();
                }
                return _FeedManager;
            }
        }

        /// <summary>
        /// Application Title (Commonly used in application)
        /// </summary>
        private static string _ApplicationTitle = null;
        public string ApplicationTitle
        {
            get
            {
                return _ApplicationTitle;
            }

            set
            {
                if (_ApplicationTitle == null)
                {
                    _ApplicationTitle = value;
                    NotifyPropertyChanged("ApplicationTitle");
                }
            }
        }

        /// <summary>
        /// Title for page, control, item...
        /// </summary>
        private string _Title = null;
        public string Title
        {
            get
            {
                return _Title;                
            }

            set
            {
                if (_Title == null)
	            {
                    _Title = value;
                    NotifyPropertyChanged("Title");
	            }
            }
        }
    }
}
