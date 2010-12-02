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


namespace TED7
{
    public class VideoSearchPageViewModel : TED7ViewModelBase
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

        public VideoSearchPageViewModel()
        {
            this.Title = "Video Search";

            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

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
