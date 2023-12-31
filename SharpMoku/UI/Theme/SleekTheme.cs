using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpMoku.UI.ThemeSpace
{
    /*
     *Credit:https://codepen.io/mudrenok/pen/gpMXgg
     */
    public class SleekTheme : Theme
    {
        public SleekTheme()
        {
            this.NotationForeColor = Color.White;
            this.CellBackColor = Color.FromArgb(52, 73, 94);
            this.BoardColor = Color.FromArgb(40, 50, 70);

            this.CellCornerRadius = 10;
            this.CellBorderStyle = BorderStyle.FixedSingle;
            this.XColor = Color.FromArgb(46, 204, 113);
            this.OColor = Color.OrangeRed;
            this.CustomPaint = new TicTacToe1();


        }
    }
}
