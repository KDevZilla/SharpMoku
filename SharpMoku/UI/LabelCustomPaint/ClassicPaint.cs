using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.UI
{
    public class ClassicPaint : IExtendLabelCustomPaint
    {
        public void Paint(Graphics g, ExtendLabel pLabel)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            FontFamily family = new FontFamily("Segoe UI");


            Font font = new Font(family, 24.25f, FontStyle.Bold);


            Color ForeColor = pLabel.theme.OColor;
            if (pLabel.CellAttribute.CellValue == Board.CellValue.Black)
            {
                ForeColor = pLabel.theme.XColor;
            }
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;


            if (pLabel.CellAttribute.CellValue != Board.CellValue.Empty)
            {
                int xOffset = 3;
                int yOffset = 3;
                Rectangle rectangleCircle = new Rectangle(
                   pLabel.ClientRectangle.X + xOffset,
                   pLabel.ClientRectangle.Y + yOffset,
                   pLabel.ClientRectangle.Width - (xOffset * 2),
                   pLabel.ClientRectangle.Height - (yOffset * 2));


                g.FillEllipse(ShareGraphicObject.SolidBrush(ForeColor), rectangleCircle);
            }
            g.DrawRectangle(ShareGraphicObject.Pen(Color.Black, 2f), pLabel.ClientRectangle);



            // Don't use TextRendered, your text will not be in center alginment
            /*
            TextRenderer.DrawText(g, pLabel.CustomDrawValue , font, 
                                     pLabel.ClientRectangle, 
                                     ForeColor );
                                     
            */
            // DrawShadow(g, pLabel.ClientRectangle);
        }



    }
}
