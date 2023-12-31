using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpMoku.UI.ThemeSpace
{
    public class TicTacToe2Theme : Theme
    {
        public TicTacToe2Theme()
        {
           
            this.NotationForeColor = Color.White     ;
            this.CellBackColor = Color.White ;
            this.BoardColor = Color.FromArgb (53,152,219)   ;
            this.XColor = Color.White   ;
            this.OColor = Color.White;
            this.CustomPaint = new LabelCustomPaint.TicTacToe2();


        }
    }

}
