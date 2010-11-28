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
using SimpleMVVM;

using Pb.FeedLibrary;

namespace TED7
{
    public class MainViewModel : TED7ViewModelBase
    {       
        private TEDVideoFiller _VideoFiller = null;
        public TEDVideoFiller VideoFiller
        {
            get
            {
                if (_VideoFiller == null)
                {
                    _VideoFiller = new TEDVideoFiller(this.Items, this.Dispatcher);
                }
                return _VideoFiller;
            }
        }
        
        private TEDVideoProvider _VideoProvider = null;
        public TEDVideoProvider VideoProvider
        {
            get
            {
                if (_VideoProvider == null)
                {
                    _VideoProvider = new TEDVideoProvider(this.VideoFiller);
                }
                return _VideoProvider;
            }
        }

        public MainViewModel()
        {
            this.ApplicationTitle = "TED 7";
            this.Title = "ted seven";

            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        private string _SearchText = string.Empty;
        public string SearchText
        {
            get
            {
                return _SearchText;
            }

            set
            {
                if (_SearchText != value)
                {
                    _SearchText = value;
                    NotifyPropertyChanged("SearchText");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        #region Commands 

        #region NavigateToSearchPageCommand

        private ICommand _NavigateToSearchPageCommand = null;
        public ICommand NavigateToSearchPageCommand
        {
            get
            {
                if (_NavigateToSearchPageCommand == null)
                {
                    _NavigateToSearchPageCommand = new RelayCommand(NavigateToSearchPageCommand_Executed, p => true);
                }
                return _NavigateToSearchPageCommand;
            }            
        }

        private void NavigateToSearchPageCommand_Executed(object param)
        {
            
        }

        #endregion NavigateToSearchPageCommand

        #endregion Commands

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            this.FeedManager.Register(this.VideoProvider);

            this.VideoProvider.Request();

            this.IsDataLoaded = true;
        }


    }
}