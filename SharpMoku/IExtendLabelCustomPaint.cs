using SharpMoku.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SharpMoku.UI.ExtendLabel;
using static SharpMoku.GomokuCellAttribute;

namespace SharpMoku
{




    public class Connect5Paint : IExtendLabelCustomPaint
    {

        public void Paint(Graphics g, ExtendLabel pLabel)
        {

            // Pen P;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            int Radius = 8;
            RectangleF RecCircle = new RectangleF(Radius, Radius, pLabel.Width - Radius * 2, pLabel.Height - Radius * 2);

            Color c = Color.White;
            switch (pLabel.CellAttribute.CellValue)
            {
                case Board.CellValue.Black:
                    c = pLabel.theme.XColor;
                    break;
                case Board.CellValue.White:
                    c = pLabel.theme.OColor;
                    break;
            }

            g.FillEllipse(ShareGraphicObject.SolidBrush(c), RecCircle);
        }
    }

}
