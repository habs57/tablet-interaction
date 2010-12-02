﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace TED7
{
    public partial class VideoSearchPage : PhoneApplicationPage
    {
        public VideoSearchPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.VideoSearchViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.VideoSearchViewModel.IsDataLoaded)
            {
                FrameworkElement view = sender as FrameworkElement;
                if (view != null)
                {
                    App.VideoSearchViewModel.Dispatcher = view.Dispatcher;
                }

                App.VideoSearchViewModel.LoadData();
            }
        }
    }
}