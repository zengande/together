using System;
using System.Collections.Generic;
using System.Text;
using Together.Core.Models;
using Xamarin.Forms;

namespace Together.Core.Services.Theme
{
    public interface IPalette
    {
        ThemeColors GetColors(ImageSource imageSource);
    }
}
