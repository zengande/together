using System;
using System.Collections.Generic;
using System.Text;
using Together.Core.Models;
using Together.Core.Services.Theme;
using Xamarin.Forms;

namespace Together.Core.ViewModels
{
    public class EmptyPageViewModel : BaseViewModel<string>
    {
        private readonly IPalette _palette;
        public EmptyPageViewModel(IPalette palette)
        {
            _palette = palette;
        }

        public override void Prepare(string picPath)
        {
            if (picPath != null)
            {
                var colors = _palette.GetColors(new UriImageSource { Uri = new Uri(picPath) });
                Device.BeginInvokeOnMainThread(() => Theme = colors);
            }
        }

        private ThemeColors theme;
        public ThemeColors Theme
        {
            get => theme;
            set => SetProperty(ref theme, value);
        }
    }
}
