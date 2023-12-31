using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharpMoku.GomokuCellAttribute;

namespace SharpMoku.UI.LabelCustomPaint
{
    public class GoMokuPaintBuilder
    {
        private String whiteStoneImagePath = "";
        private String blackStoneImagePath = "";
        private Color whiteStoneBorderColor = Color.White;
        private Color blackStoneBorderColor = Color.Black;
        // private String boardBackgroundImagePath = "";
        // private Color boardBackColor = Color.Yellow;
        private Color whiteStoneBackColor = Color.White;
        private Color blackStoneBackColor = Color.Black;
        private Pen penTable = null;
        private Pen penBorder = null;
        public GoMokuPaintBuilder WhiteStoneImagePath(String path)
        {
            this.whiteStoneImagePath = path;
            return this;
        }
        public GoMokuPaintBuilder BlackStoneImagePath(String path)
        {
            this.blackStoneImagePath = path;
            return this;
        }
        /*
        public GoMokuPaintBuilder BoardBackgroundImagePath(String path)
        {
            this.boardBackgroundImagePath = path;
            return this;
        }
        */
        public GoMokuPaintBuilder WhiteStoneBorderColor(Color borderColor)
        {
            this.whiteStoneBorderColor = borderColor;
            return this;
        }

        public GoMokuPaintBuilder BlackStoneBorderColor(Color borderColor)
        {
            this.blackStoneBorderColor = borderColor;
            return this;
        }
        public GoMokuPaintBuilder WhiteStoneBackColor(Color backColor)
        {
            this.whiteStoneBackColor = backColor;
            return this;
        }

        public GoMokuPaintBuilder BlackStoneBackColor(Color backColor)
        {
            this.blackStoneBackColor = backColor;
            return this;
        }

        public GoMokuPaintBuilder PenTable(Pen pen)
        {
            this.penTable = pen;
            return this;
        }
        public GoMokuPaintBuilder PenBorder(Pen pen)
        {
            this.penBorder = pen;
            return this;
        }

        /*
        public GoMokuPaintBuilder BoardBackColor(Color backColor)
        {
            this.boardBackColor = backColor;
            return this;
        }
        */

        public GoMokuPaint Build()
        {
            /*
            if(!IsValidConfigured)
            {
                throw new Exception ("The configured value is not valid ")
            }
            */
            GoMokuPaint gomokuPaint = new GoMokuPaint(
                this.whiteStoneImagePath,
                this.blackStoneImagePath,
                this.whiteStoneBackColor,
                this.whiteStoneBorderColor,
                this.blackStoneBackColor,
                this.blackStoneBorderColor,
                this.penTable,
                this.penBorder);
            return gomokuPaint;
        }
    }
    public class GoMokuPaint : IExtendLabelCustomPaint
    {
        /*
         * Wooden texture credit::<a href='https://www.freepik.com/photos/rustic-wood'>Rustic wood photo created by lifeforstock - www.freepik.com</a>
         * <a href='https://www.freepik.com/photos/wood-background'>Wood background photo created by rawpixel.com - www.freepik.com</a>
         */
        private String whiteStoneImagePath = "";
        private String blackStoneImagePath = "";
        // private String boardBackgroundImagePath = "";
        private Color whiteStoneBackColor = Color.White;
        private Color blackStoneBackColor = Color.Black;
        private Color whiteStoneBorderColor = Color.White;
        private Color blackStoneBorderColor = Color.Black;

        // private Color boardBackColor = Color.Yellow;
        private Pen penTable = new Pen(Color.Black, 2f);
        private Pen penBorder = null;// new Pen(Color.Black, 4f);

        public GoMokuPaint(
            String whiteStoneImagePath,
            String blackStoneImagePath,
            Color whiteStoneBackColor,
            Color whiteStoneBorderColor,
            Color blackStoneBackColor,
            Color blackStoneBorderColor,
            Pen penTable,
            Pen penBorder)
        {
            //  this.boardBackgroundImagePath = boardBackgroundImagePath;
            this.whiteStoneImagePath = whiteStoneImagePath;
            this.blackStoneImagePath = blackStoneImagePath;
            //  this.boardBackColor = boardBackColor;
            this.whiteStoneBackColor = whiteStoneBackColor;
            this.blackStoneBackColor = blackStoneBackColor;
            this.whiteStoneBorderColor = whiteStoneBorderColor;
            this.blackStoneBorderColor = blackStoneBorderColor;
            if (penTable != null)
            {
                this.penTable = penTable;
            }
            if (penBorder != null)
            {
                this.penBorder = penBorder;
            }

        }

        private void SetLeftBorder(int beginWidth,
            int beginHeight,
            int endWidth,
            int endHeight,
            ref Point pfromPointBorderX,
            ref Point ptoPointBorderX,
            ref Point pfromPointBorderY,
            ref Point ptoPointBorderY)
        {
            pfromPointBorderX = new Point(beginWidth + 1, beginHeight);
            ptoPointBorderX = new Point(beginWidth + 1, beginHeight);

            pfromPointBorderY = new Point(beginWidth + 1, beginHeight);
            ptoPointBorderY = new Point(beginWidth + 1, endHeight);
        }

        private void SetRightBorder(int beginWidth,
    int beginHeight,
    int endWidth,
    int endHeight,
    ref Point pfromPointBorderX,
    ref Point ptoPointBorderX,
    ref Point pfromPointBorderY,
    ref Point ptoPointBorderY)
        {
            pfromPointBorderX = new Point(endWidth - 1, beginHeight);
            ptoPointBorderX = new Point(endWidth - 1, beginHeight);

            pfromPointBorderY = new Point(endWidth - 1, beginHeight);
            ptoPointBorderY = new Point(endWidth - 1, endHeight);
        }

        private void SetTopBorder(int beginWidth,
    int beginHeight,
    int endWidth,
    int endHeight,
    ref Point pfromPointBorderX,
    ref Point ptoPointBorderX,
    ref Point pfromPointBorderY,
    ref Point ptoPointBorderY)
        {
            int Space = 0;
            pfromPointBorderX = new Point(beginWidth + Space, beginHeight);
            ptoPointBorderX = new Point(endWidth - Space, beginHeight);

            pfromPointBorderY = new Point(beginWidth + Space, beginHeight);
            ptoPointBorderY = new Point(beginWidth + Space, beginHeight);
        }

        private void SetBottomBorder(int beginWidth,
    int beginHeight,
    int endWidth,
    int endHeight,
    ref Point pfromPointBorderX,
    ref Point ptoPointBorderX,
    ref Point pfromPointBorderY,
    ref Point ptoPointBorderY)
        {

            int Space = 0;
            pfromPointBorderX = new Point(beginWidth + Space, endHeight);
            ptoPointBorderX = new Point(endWidth - Space, endHeight);

            pfromPointBorderY = new Point(beginWidth + Space, endHeight);
            ptoPointBorderY = new Point(beginWidth + Space, endHeight);
        }
        public void Paint(Graphics g, ExtendLabel pLabel)
        {

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingMode = CompositingMode.SourceCopy;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;


            Point fromPointY = Point.Empty;
            Point toPointY = Point.Empty;
            Point fromPointX = Point.Empty;
            Point toPointX = Point.Empty;

            Point fromPointBorderY = new Point(-1, -1);
            Point toPointBorderY = new Point(-1, -1);
            Point fromPointBorderX = new Point(-1, -1);
            Point toPointBorderX = new Point(-1, -1);


            Point fromPointBorderY2 = new Point(-1, -1);
            Point toPointBorderY2 = new Point(-1, -1);
            Point fromPointBorderX2 = new Point(-1, -1);
            Point toPointBorderX2 = new Point(-1, -1);

            int beginWidth = 0;
            int middleWidth = pLabel.Width / 2;
            int endWidth = pLabel.Width;
            int beginHeight = 0;
            int middleHeight = pLabel.Height / 2;
            int endHeight = pLabel.Height;

            switch (pLabel.CellAttribute.GoboardPosition)
            {
                case GoBoardPositionEnum.Center:
                    fromPointX = new Point(middleWidth, beginHeight);
                    toPointX = new Point(middleWidth, endHeight);

                    fromPointY = new Point(beginWidth, middleHeight);
                    toPointY = new Point(endWidth, middleHeight);
                    break;
                case GoBoardPositionEnum.TopLeftCorner:
                    fromPointX = new Point(middleWidth, middleHeight);
                    toPointX = new Point(middleWidth, endHeight);

                    fromPointY = new Point(middleWidth, middleHeight);
                    toPointY = new Point(endWidth, middleHeight);

                    SetLeftBorder(beginWidth, beginHeight, endWidth, endHeight,
                        ref fromPointBorderX,
                        ref toPointBorderX,
                        ref fromPointBorderY,
                        ref toPointBorderY);

                    SetTopBorder(beginWidth, beginHeight, endWidth, endHeight,
ref fromPointBorderX2,
ref toPointBorderX2,
ref fromPointBorderY2,
ref toPointBorderY2);

                    break;
                case GoBoardPositionEnum.TopRightCorner:
                    fromPointX = new Point(middleWidth, middleHeight);
                    toPointX = new Point(middleWidth, endHeight);

                    fromPointY = new Point(beginWidth, middleHeight);
                    toPointY = new Point(middleWidth, middleHeight);

                    SetRightBorder(beginWidth, beginHeight, endWidth, endHeight,
    ref fromPointBorderX,
    ref toPointBorderX,
    ref fromPointBorderY,
    ref toPointBorderY);

                    SetTopBorder(beginWidth, beginHeight, endWidth, endHeight,
ref fromPointBorderX2,
ref toPointBorderX2,
ref fromPointBorderY2,
ref toPointBorderY2);
                    break;
                case GoBoardPositionEnum.BottomLeftCorner:
                    fromPointX = new Point(middleWidth, beginHeight);
                    toPointX = new Point(middleWidth, middleHeight);

                    fromPointY = new Point(middleWidth, middleHeight);
                    toPointY = new Point(endWidth, middleHeight);

                    SetLeftBorder(beginWidth, beginHeight, endWidth, endHeight,
    ref fromPointBorderX,
    ref toPointBorderX,
    ref fromPointBorderY,
    ref toPointBorderY);

                    SetBottomBorder(beginWidth, beginHeight, endWidth, endHeight,
ref fromPointBorderX2,
ref toPointBorderX2,
ref fromPointBorderY2,
ref toPointBorderY2);
                    break;
                case GoBoardPositionEnum.BottomRightCorner:

                    fromPointX = new Point(middleWidth, beginHeight);
                    toPointX = new Point(middleWidth, middleHeight);

                    fromPointY = new Point(beginWidth, middleHeight);
                    toPointY = new Point(middleWidth, middleHeight);

                    SetRightBorder(beginWidth, beginHeight, endWidth, endHeight,
ref fromPointBorderX,
ref toPointBorderX,
ref fromPointBorderY,
ref toPointBorderY);
                    SetBottomBorder(beginWidth, beginHeight, endWidth, endHeight,
ref fromPointBorderX2,
ref toPointBorderX2,
ref fromPointBorderY2,
ref toPointBorderY2);
                    break;
                case GoBoardPositionEnum.TopBorder:
                    fromPointX = new Point(middleWidth, middleHeight);
                    toPointX = new Point(middleWidth, endHeight);

                    fromPointY = new Point(beginWidth, middleHeight);
                    toPointY = new Point(endWidth, middleHeight);

                    SetTopBorder(beginWidth, beginHeight, endWidth, endHeight,
ref fromPointBorderX,
ref toPointBorderX,
ref fromPointBorderY,
ref toPointBorderY);
                    break;
                case GoBoardPositionEnum.BottomBorder:
                    fromPointX = new Point(middleWidth, middleHeight);
                    toPointX = new Point(middleWidth, beginHeight);

                    fromPointY = new Point(beginWidth, middleHeight);
                    toPointY = new Point(endWidth, middleHeight);

                    SetBottomBorder(beginWidth, beginHeight, endWidth, endHeight,
ref fromPointBorderX,
ref toPointBorderX,
ref fromPointBorderY,
ref toPointBorderY);
                    break;
                case GoBoardPositionEnum.LeftBorder:
                    fromPointX = new Point(middleWidth, beginHeight);
                    toPointX = new Point(middleWidth, endHeight);

                    fromPointY = new Point(middleWidth, middleHeight);
                    toPointY = new Point(endWidth, middleHeight);

                    SetLeftBorder(beginWidth, beginHeight, endWidth, endHeight,
                        ref fromPointBorderX,
                        ref toPointBorderX,
                        ref fromPointBorderY,
                        ref toPointBorderY);

                    break;
                case GoBoardPositionEnum.RightBorder:
                    fromPointX = new Point(middleWidth, beginHeight);
                    toPointX = new Point(middleWidth, endHeight);

                    fromPointY = new Point(beginWidth, middleHeight);
                    toPointY = new Point(middleWidth, middleHeight);

                    SetRightBorder(beginWidth, beginHeight, endWidth, endHeight,
ref fromPointBorderX,
ref toPointBorderX,
ref fromPointBorderY,
ref toPointBorderY);


                    break;
            }


            g.DrawLine(penTable, fromPointX, toPointX);
            g.DrawLine(penTable, fromPointY, toPointY);
            if (penBorder != null)
            {
                if (fromPointBorderX.X != -1)
                {
                    g.DrawLine(penBorder, fromPointBorderX, toPointBorderX);
                    g.DrawLine(penBorder, fromPointBorderY, toPointBorderY);

                }
                if (fromPointBorderX2.X != -1)
                {
                    g.DrawLine(penBorder, fromPointBorderX2, toPointBorderX2);
                    g.DrawLine(penBorder, fromPointBorderY2, toPointBorderY2);

                }
            }
            //Tuple<int, int> positionIntersec = new Tuple<int, int> ()
            g.CompositingMode = CompositingMode.SourceOver;

            if (pLabel.CellAttribute.IsIntersection)
            {
                RectangleF RecCircleIntersecton = new RectangleF(middleWidth - 4, middleHeight - 4, 8, 8);
                g.FillEllipse(ShareGraphicObject.SolidBrush(penTable.Color), RecCircleIntersecton);
            }

            int Space = 1;
            //RectangleF RecCircle = new RectangleF(Space, Space, pLabel.Width - (Space * 2), pLabel.Height - (Space * 2));
            RectangleF RecCircle = new RectangleF(Space, Space, pLabel.Width - (Space * 2), pLabel.Height - (Space * 2));
            /*
            RectangleF RecCircleImage = new RectangleF(RecCircle.X + 0.25f,
                RecCircle.Y + 0.25f,
                RecCircle.Width - 0.5f,
                RecCircle.Height - 0.5f);
                */
            RectangleF RecCircleImage = new RectangleF(RecCircle.X,
    RecCircle.Y,
    RecCircle.Width - 0.5f,
    RecCircle.Height - 0.5f);

            if (pLabel.CellAttribute.CellValue == Board.CellValue.White)
            {
                //DrawShadow(g, RecCircle);
                if (this.whiteStoneImagePath.Trim() != "")
                {

                    g.DrawImage(ShareGraphicObject.BitmapFilePath(this.whiteStoneImagePath), RecCircleImage);
                }
                else
                {
                    g.FillEllipse(ShareGraphicObject.SolidBrush(whiteStoneBackColor), RecCircle);
                    g.DrawEllipse(ShareGraphicObject.Pen(whiteStoneBorderColor, 0.2f), RecCircle);

                }

            }
            if (pLabel.CellAttribute.CellValue == Board.CellValue.Black)
            {
                // DrawShadow(g, RecCircle);

                if (this.blackStoneImagePath.Trim() != "")
                {
                    g.DrawImage(ShareGraphicObject.BitmapFilePath(this.blackStoneImagePath), RecCircleImage);
                }
                else
                {
                    //var shadowRectagle = new RectangleF(RecCircle.X + 3, RecCircle.Y + 3, RecCircle.Width-1, RecCircle.Height-1);
                    //g.FillEllipse(ShareGraphicObject.SolidBrush(Color.LightGray ), shadowRectagle);

                    g.FillEllipse(ShareGraphicObject.SolidBrush(blackStoneBackColor), RecCircle);
                    g.DrawEllipse(ShareGraphicObject.Pen(blackStoneBorderColor, 0.2f), RecCircle);

                }

            }

            // Uncomment this code if you need to see Neighbor cell
            //DANGER
            /*
            if(pLabel.CellAttribute.IsNeighborCell)
            {
                //We Actually don't use this for actual gaming
                //We paint NeighborCell to shows its position for explaining on Tutorial and for debugging purpose only
                g.FillEllipse(ShareGraphicObject.SolidBrush(Color.Gray), RecCircle);

            }
            */
        }

        private void DrawShadow(Graphics g, RectangleF rectangle)
        {
            //Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Color color = Color.Blue;
            Color shadow = Color.FromArgb(255, 16, 16, 16);
            /*
            for (int i = 0; i < 8; i++)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(80 - i * 10, shadow)))
                {
                    g.FillEllipse(brush, panel1.ClientRectangle.X + i * 2,
                                         panel1.ClientRectangle.Y + i, 60, 60);
                }
            using (SolidBrush brush = new SolidBrush(color))
                g.FillEllipse(brush, panel1.ClientRectangle.X, panel1.ClientRectangle.Y, 60, 60);
                */
            // move to the right to use the same coordinates again for the drawn shape
            // g.TranslateTransform(80, 0);
            // g.TranslateTransform(5, 5);
            for (int i = 0; i < 8; i++)
                //using (Pen pen = new Pen(Color.FromArgb(80 - i * 10, shadow), 0.5f))
                using (Pen pen = new Pen(Color.FromArgb(80 - i * 10, shadow), 0.75f))
                {
                    rectangle = new RectangleF(rectangle.Left + (i * 0.75f) + 0.1f,
                        rectangle.Top + (i * 0.75f) + 0.1f,
                        rectangle.Width,
                        rectangle.Height);
                    g.DrawEllipse(pen, rectangle);
                }
            /*
            using (Pen pen = new Pen(color))
                g.DrawEllipse(pen, panel1.ClientRectangle.X, panel1.ClientRectangle.Y, 60, 60);
                */
        }
    }
}
