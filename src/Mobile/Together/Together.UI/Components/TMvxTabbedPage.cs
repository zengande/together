using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Core.ViewModels;
using Xamarin.Forms;

namespace Together.UI.Components
{
    public class TMvxTabbedPage<TViewModel> : MvxTabbedPage<TViewModel> where TViewModel : class, IMvxViewModel
    {
        #region SelectedIconColor
        public Color SelectedIconColor
        {
            get { return (Color)GetValue(SelectedIconColorProperty); }
            set { SetValue(SelectedIconColorProperty, value); }
        }

        public static readonly BindableProperty SelectedIconColorProperty = BindableProperty.Create(
            nameof(SelectedItemProperty),
            typeof(Color),
            typeof(TMvxTabbedPage<>),
            Color.White);

        #endregion

        #region UnselectedIconColor
        public Color UnselectedIconColor
        {
            get { return (Color)GetValue(UnelectedIconColorProperty); }
            set { SetValue(UnelectedIconColorProperty, value); }
        }

        public static readonly BindableProperty UnelectedIconColorProperty = BindableProperty.Create(
            nameof(UnselectedIconColor),
            typeof(Color),
            typeof(TMvxTabbedPage<>),
            Color.White);
        #endregion

        #region SelectedTextColor
        public Color SelectedTextColor
        {
            get { return (Color)GetValue(SelectedTextColorProperty); }
            set { SetValue(SelectedTextColorProperty, value); }
        }

        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(
            nameof(SelectedTextColor),
            typeof(Color),
            typeof(TMvxTabbedPage<>),
            Color.White);
        #endregion

        #region UnselectedTextColor
        public Color UnselectedTextColor
        {
            get { return (Color)GetValue(UnselectedTextColorProperty); }
            set { SetValue(UnselectedTextColorProperty, value); }
        }

        public static readonly BindableProperty UnselectedTextColorProperty = BindableProperty.Create(
            nameof(UnselectedTextColor),
            typeof(Color),
            typeof(TMvxTabbedPage<>),
            Color.White);
        #endregion

    }
}
