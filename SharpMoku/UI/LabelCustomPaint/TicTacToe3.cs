using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMoku;
using SharpMoku.Utility;
using PositionEnum = SharpMoku.GomokuCellAttribute.GoBoardPositionEnum;

namespace SharpMoku.UI.LabelCustomPaint
{
    class TicTacToe3 : IExtendLabelCustomPaint
    {

        public void Paint(Graphics g, ExtendLabel pLabel)
        {

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Color labelBackColor = Color.White; // pLabel.BackColor;

            Rectangle rec = pLabel.ClientRectangle;
            var BorderRec = new RectangleF(rec.X + 0.5f, rec.Y + 0.5f, rec.Width - 1, rec.Height - 1);

            g.DrawRectangle(ShareGraphicObject.Pen(Color.Black, 1), BorderRec.X, BorderRec.Y, BorderRec.Width, BorderRec.Height);

            var boardPosition = pLabel.CellAttribute.GoboardPosition;
            bool isNeedToRemoveTopBorder = boardPosition.In(PositionEnum.TopLeftCorner,
                PositionEnum.TopBorder,
                PositionEnum.TopRightCorner);


            bool isNeedToRemoveLeftBorder = boardPosition.In(PositionEnum.TopLeftCorner,
                PositionEnum.LeftBorder,
                PositionEnum.BottomLeftCorner);

            bool isNeedToRemoveRightBorder = boardPosition.In(PositionEnum.TopRightCorner,
                PositionEnum.RightBorder,
                PositionEnum.BottomRightCorner);

            bool isNeedToRemoveBottomBorder = boardPosition.In(PositionEnum.BottomLeftCorner,
                PositionEnum.BottomBorder,
                PositionEnum.BottomRightCorner);

            if (isNeedToRemoveTopBorder)
            {
                g.DrawLine(ShareGraphicObject.Pen(labelBackColor, 2),
                    new PointF(BorderRec.X, BorderRec.Y),
                    new PointF(BorderRec.X + BorderRec.Width, BorderRec.Y));
            }

            if (isNeedToRemoveLeftBorder)
            {
                g.DrawLine(ShareGraphicObject.Pen(labelBackColor, 2),
                    new PointF(BorderRec.X, BorderRec.Y),
                    new PointF(BorderRec.X, BorderRec.Y + BorderRec.Height));
            }
            if (isNeedToRemoveRightBorder)
            {
                g.DrawLine(ShareGraphicObject.Pen(labelBackColor, 2),
                    new PointF(BorderRec.X + BorderRec.Width, BorderRec.Y),
                    new PointF(BorderRec.X + BorderRec.Width, BorderRec.Y + BorderRec.Height));
            }
            if (isNeedToRemoveBottomBorder)
            {
                g.DrawLine(ShareGraphicObject.Pen(labelBackColor, 2),
                    new PointF(BorderRec.X, BorderRec.Y + BorderRec.Height),
                    new PointF(BorderRec.X + BorderRec.Width, BorderRec.Y + BorderRec.Height));
            }


            if (pLabel.CellAttribute.CellValue == Board.CellValue.White)
            {

                int offset = 8;
                float lineWidth = 3.3f;
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

                    RectangleF RecCircle = new RectangleF(8, 8, pLabel.Width - 16, pLabel.Height - 16);

                    g.DrawEllipse(ShareGraphicObject.Pen(pLabel.theme.OColor, 3), RecCircle);


                }
            }
        }
    }
}