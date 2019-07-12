﻿using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Together.Core.ViewModels.Discover;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Together.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiscoverPage : MvxContentPage<DiscoverPageViewModel>
    {
        public DiscoverPage()
        {
            InitializeComponent();
        }
    }
}