using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpMoku.UI.ThemeSpace
{
    public class TableTennisTheme : Theme
    {
        public TableTennisTheme()
        {

            this.CellCornerRadius = 0;
            this.CellBorderStyle = BorderStyle.FixedSingle;
            this.BoardColor = Color.FromArgb(30, 143, 213);
            this.XColor = Color.Orange; // Color.Teal;
            this.OColor = Color.White;
            this.NotationForeColor = Color.White;
            this.CustomPaint = new ClassicPaint();

        }
    }
}
