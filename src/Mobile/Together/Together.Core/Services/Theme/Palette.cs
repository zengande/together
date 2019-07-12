using System;
using System.Collections.Generic;
using System.Text;
using Together.Core.Models;
using Xamarin.Forms;

namespace Together.Core.Services.Theme
{
    public class Palette : IPalette
    {
        public ThemeColors GetColors(ImageSource imageSource)
        {
            ThemeColors themeColors = new ThemeColors();

            return themeColors;
        }
    }
}
