using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Together.Core.Models
{
    public class ThemeColors
    {
        public Color MainColor { get; set; }
        public Color TextColor { get; set; }
        public Color TitleColor { get; set; }
        public void SetColor(int mainrgb, int textrgb, int titlergb)
        {
            MainColor = Color.FromArgb((mainrgb >> 16) & 0x0ff, (mainrgb >> 8) & 0x0ff, mainrgb & 0x0ff);
            TextColor = Color.FromArgb((textrgb >> 16) & 0x0ff, (textrgb >> 8) & 0x0ff, textrgb & 0x0ff);
            TitleColor = Color.FromArgb((titlergb >> 16) & 0x0ff, (titlergb >> 8) & 0x0ff, titlergb & 0x0ff);
        }
    }
}
