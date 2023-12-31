using SharpMoku.UI.LabelCustomPaint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpMoku.UI.ThemeSpace
{
    public class GomokuThemeBuilder
    {
        private String boardImageFile = "";
        private String whiteStoneImagePath = "";
        private String blackStoneImagePath = "";
        private Color boardBackColor = Color.Yellow;
        private Color whiteStoneBorderColor = Color.White;
        private Color blackStoneBorderColor = Color.Black;
        private Color whiteStoneBackColor = Color.White;
        private Color blackStoneBackColor = Color.Black;
        private Color notationForecolor = Color.Black;
        private Pen penTable = null;
        private Pen penBorder = null;

        public GomokuThemeBuilder WhiteStoneImagePath(String path)
        {
            this.whiteStoneImagePath = path;
            return this;
        }
        public GomokuThemeBuilder BlackStoneImagePath(String path)
        {
            this.blackStoneImagePath = path;
            return this;
        }

        public GomokuThemeBuilder WhiteStoneBorderColor(Color borderColor)
        {
            this.whiteStoneBorderColor = borderColor;
            return this;
        }
        public GomokuThemeBuilder WhiteStoneBackColor(Color backColor)
        {
            this.whiteStoneBackColor = backColor;
            return this;
        }
        public GomokuThemeBuilder BlackStoneBorderColor(Color borderColor)
        {
            this.blackStoneBorderColor = borderColor;
            return this;
        }
        public GomokuThemeBuilder BlackStoneBackColor(Color backColor)
        {
            this.blackStoneBackColor = backColor;
            return this;
        }

        public GomokuThemeBuilder BoardBackColor(Color backColor)
        {
            this.boardBackColor = backColor;
            return this;
        }

        public GomokuThemeBuilder NotationForecolor(Color foreColor)
        {
            this.notationForecolor = foreColor;
            return this;
        }
        public GomokuThemeBuilder PenTable(Pen pen)
        {
            this.penTable = pen;
            return this;
        }
        public GomokuThemeBuilder PenBorder(Pen pen)
        {
            this.penBorder = pen;
            return this;
        }
        public GomokuTheme Build()
        {

            GomokuTheme theme = new GomokuTheme(
                this.boardImageFile,
                this.whiteStoneImagePath,
                this.blackStoneImagePath,
                this.boardBackColor,
                this.whiteStoneBackColor,
                this.whiteStoneBorderColor,
                this.blackStoneBackColor,
                this.blackStoneBorderColor,
                this.notationForecolor,
                this.penTable,
                this.penBorder);
            return theme;
        }
        public GomokuThemeBuilder BoardImageFile(String path)
        {

            this.boardImageFile = path;
            return this;
        }
    }
    public class GomokuTheme : Theme
    {
        public GomokuTheme(

            String boardImageFile,
            String whiteStoneImagePath,
            String blackStoneImagePath,
            Color boardBackcolor,
            Color whiteStoneBackColor,
            Color whiteStoneBorderColor,
            Color blackStoneBackColor,
            Color blackStoneBorderColor,
            Color notationForecolor,
            Pen penTable,
            Pen penBorder)
        {
            this.SpaceBetweenBorderSize = 0;
            this.CellBorderStyle = BorderStyle.None;
            this.CellBackColor = Color.Transparent;
            this.CellCornerRadius = 0;



            this.CustomPaint = new GoMokuPaint(whiteStoneImagePath,
                blackStoneImagePath,
                whiteStoneBackColor,
                whiteStoneBorderColor,
                blackStoneBackColor,
                blackStoneBorderColor,
                penTable,
                penBorder);

            this.NotationForeColor = notationForecolor;

            this.BoardImageFile = boardImageFile;
            this.BoardColor = boardBackcolor;

        }
    }
}
