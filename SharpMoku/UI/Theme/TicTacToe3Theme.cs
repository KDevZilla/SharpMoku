using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.UI.ThemeSpace
{
    public class TicTacToe3Theme : Theme
    {
        public TicTacToe3Theme()
        {

            this.NotationForeColor = Color.White ;
            this.CellBackColor = Color.White ;
            this.BoardColor = Color.White;
            this.XColor = Color.FromArgb(230, 107, 38); 
            this.OColor = Color.FromArgb(20, 185, 150);
            this.CustomPaint = new LabelCustomPaint.TicTacToe3();

        }
    }

}
