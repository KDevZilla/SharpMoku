using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.UI.LabelCustomPaint
{
    public class TicTacToe2 : IExtendLabelCustomPaint
    {

        public void Paint(Graphics g, ExtendLabel pLabel)
        {


            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Color temp = pLabel.BackColor;
            Rectangle rec = pLabel.ClientRectangle;
            var BorderRec = new RectangleF(rec.X + 0.5f, rec.Y + 0.5f, rec.Width - 1, rec.Height - 1);

            g.DrawRectangle(ShareGraphicObject.Pen(Color.White, 1), BorderRec.X, BorderRec.Y, BorderRec.Width, BorderRec.Height);
            if (pLabel.CellAttribute.CellValue == Board.CellValue.White)
            {

                int offset = 12;
                float lineWidth = 3f;
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

                    RectangleF RecCircle = new RectangleF(12, 12, pLabel.Width - 24, pLabel.Height - 24);

                    g.DrawEllipse(ShareGraphicObject.Pen(pLabel.theme.OColor, characterWidth / 2), RecCircle);


                }
            }
        }
    }
}

