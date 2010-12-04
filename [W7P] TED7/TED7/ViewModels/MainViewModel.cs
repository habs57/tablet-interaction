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
using System.Windows.Threading;

using Pb.FeedLibrary;

namespace TED7
{
    public sealed class Lazy<T>
    {
        public Lazy(Func<T> initFunction)
        {
            this._InitFunction = initFunction;
        }

        Func<T> _InitFunction = null;

        private T _Value = default(T);
        public T Value
        {
            get
            {
                if (_Value == null)
                {
                    _Value = _InitFunction();
                }
                return _Value;
            }
        }
    }

    /// <summary>
    /// Video related functions
    /// </summary>
    public sealed class VideoPageImplmentation
    {
        private Dispatcher _Dispatcher = null;

        internal VideoPageImplmentation(Dispatcher dispatcher)
        {
            this._Dispatcher = dispatcher;

            this.Items = new ObservableCollection<ItemViewModel>();
            this.Filler = new Lazy<IFiller>(() => { return new TEDVideoFiller(this.Items, this._Dispatcher); });
            this.Provider = new Lazy<Provider>(() => { return new TEDVideoProvider(this.Filler.Value); });
        }

        public Lazy<IFiller> Filler { get; private set; }
        public Lazy<Provider> Provider { get; private set; }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>            
        public ObservableCollection<ItemViewModel> Items { get; private set; }
    }

    /// <summary>
    /// Blog related functions 
    /// </summary>
    public sealed class BlogPageImplmentation
    {
        private Dispatcher _Dispatcher = null;

        public BlogPageImplmentation(Dispatcher dispatcher)
        {
            this._Dispatcher = dispatcher;

            this.Items = new ObservableCollection<ItemViewModel>();
            this.Filler = new Lazy<IFiller>(() => { return new TEDBlogFiller(this.Items, this._Dispatcher); });
            this.Provider = new Lazy<Provider>(() => { return new TEDBlogProvider(this.Filler.Value); });
        }

        public Lazy<IFiller> Filler { get; private set; }
        public Lazy<Provider> Provider { get; private set; }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>            
        public ObservableCollection<ItemViewModel> Items { get; private set; }
    }

    public class MainViewModel : TED7ViewModelBase
    {
        #region Video View related functions

        public ObservableCollection<ItemViewModel> VideoItems 
        {
            get
            {
                return this.VideoPageImpl.Value.Items;
            }
        }

        public Lazy<VideoPageImplmentation> VideoPageImpl
        {
            get;
            private set;
        }

        #endregion Video View related functions

        #region Blog View related functions    
    
        public ObservableCollection<ItemViewModel> BlogItems
        {
            get
            {
                return this.BlogPageImpl.Value.Items;
            }
        }

        public Lazy<BlogPageImplmentation> BlogPageImpl
        {
            get;
            private set;
        }

        #endregion Blog View related functions

        public MainViewModel()
        {
            this.ApplicationTitle = "TED 7";
            this.Title = "ted seven";

            this.VideoPageImpl = new Lazy<VideoPageImplmentation>(() => { return new VideoPageImplmentation(this.Dispatcher); });
            this.BlogPageImpl = new Lazy<BlogPageImplmentation>(() => { return new BlogPageImplmentation(this.Dispatcher); });
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
            Provider videoProvider = this.VideoPageImpl.Value.Provider.Value;

            this.FeedManager.Register(videoProvider);
            videoProvider.Request();

            this.IsDataLoaded = true;
        }


    }
}