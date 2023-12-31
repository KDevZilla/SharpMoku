using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.UI
{
    public class TicTacToe1 : IExtendLabelCustomPaint
    {

        public void Paint(Graphics g, ExtendLabel pLabel)
        {


            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Color temp = pLabel.BackColor;
            if (pLabel.CellAttribute.CellValue == Board.CellValue.White)
            {


                int offset = 8;
                float lineWidth = 4.9f;
                g.DrawLine(ShareGraphicObject.Pen(pLabel.theme.XColor, lineWidth),
                                     offset,
                                     offset,
                                     pLabel.Width - offset,
                                     pLabel.Height - offset);

                g.DrawLine(ShareGraphicObject.Pen(pLabel.theme.XColor, lineWidth),
                                     offset,
                                     pLabel.Width - offset,
                                     pLabel.Height - offset,
                                     offset);
            }
            else
            {
                if (pLabel.CellAttribute.CellValue == Board.CellValue.Black)
                {
                    float characterWidth = 8;
                    RectangleF RecCircle = new RectangleF(7, 7, pLabel.Width - 14, pLabel.Height - 14);
                    g.DrawEllipse(ShareGraphicObject.Pen(pLabel.theme.OColor, characterWidth / 2), RecCircle);
                }
            }
        }
    }
}
