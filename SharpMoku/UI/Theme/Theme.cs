using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using SharpMoku.UI;
using SharpMoku.UI.LabelCustomPaint;

namespace SharpMoku.UI.ThemeSpace
{
    public abstract class Theme
    {

        public Color NotationForeColor { get; set; } = Color.Black;
        public int CellHeight;
        public int CellWidth;
        public int CellCornerRadius;
        public IExtendLabelCustomPaint CustomPaint;
        public int SpaceBetweenBorderSize = -1;
        public Color CellBackColor;
        public BorderStyle CellBorderStyle;
        public GomokuCellAttribute CellAttribute = new GomokuCellAttribute();
        public Color XColor { get; set; }
        public Color OColor { get; set; }
        public Color BoardColor;
        public int BoardSize;
        public Bitmap BoardImage
        {
            get
            {
                if(_BoardImage ==null)
                {
                    LoadBoardImage();
                }
                return _BoardImage;
            }
          
        }

        public void ApplyThemeToCell(ExtendLabel label)
        {
            label.Width = CellWidth;
            label.Height = CellHeight;
            label.CornerRadius = CellCornerRadius;
            label.CustomPaint = this.CustomPaint;
            label.BorderStyle = this.CellBorderStyle;
            label.theme = this;
            label._BackColor = this.CellBackColor;

        }
        public string BoardImageFile = "";
        public Boolean HasImage
        {
            get
            {
                return BoardImageFile.Trim() != "";
            }
        }
        private void LoadBoardImage()
        {

            if(BoardImageFile == "")
            {
                return;
            }

            Bitmap B = (Bitmap)Image.FromFile(BoardImageFile);
            B.SetResolution(96, 96);
            
            _BoardImage  = B;
        }

        private Bitmap _BoardImage = null;
        public void ApplyThemeToBoard(Panel p)
        {
            p.Paint += Panel1_Paint;
        }
        public void ApplyThemeToBoard(PictureBox p)
        {
            p.BackColor = this.BoardColor;
            p.Paint += Panel1_Paint;
        }

        private void P_Paint(object sender, PaintEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {


            int LastIndex = this.BoardSize - 1;
           
        }

    }
  




    public class ThemeFactory
    {

        public enum ThemeEnum
        {
            Gomoku1 = 0,
            Gomoku2,
            Gomoku3,
            Gomoku4,
            Gomoku5,
            TicTacToe1,
            TicTacToe2,
            TicTacToe3,
            TableTennis,


        }

        //Credit board file name
        //https://yewang.github.io/besogo/img/shinkaya1.jpg
        private static String BoardFileName = Utility.FileUtility.ResourcesPath + @"\shinkaya1.jpg";

        //Credit Stone image
        //https://github.com/featurecat/lizzie
        private static String WhiteStoneFilePath = Utility.FileUtility.ResourcesPath + @"\lizzie_White0.png";
        private static String BlackStoneFilePath = Utility.FileUtility.ResourcesPath + @"\lizzie_Black0.png";
        private static Dictionary<ThemeEnum, Color> dicBackColor = null;
        private static Dictionary<ThemeEnum, Color> dicForeColor = null;

        public static Color BackColor(ThemeEnum theme)
        {
            if(dicBackColor == null)
            {
                dicBackColor = new Dictionary<ThemeEnum, Color>();
                dicBackColor.Add(ThemeEnum.Gomoku1,Color.FromArgb (253, 188, 68));
                dicBackColor.Add(ThemeEnum.Gomoku2, Color.FromArgb(167, 145, 82));
                dicBackColor.Add(ThemeEnum.Gomoku3 , Color.FromArgb(0, 43, 54));
                dicBackColor.Add(ThemeEnum.Gomoku4, Color.FromArgb(215, 192, 174));
                dicBackColor.Add(ThemeEnum.Gomoku5, Color.FromArgb(196, 215, 178));
                dicBackColor.Add(ThemeEnum.TicTacToe1, Color.FromArgb(40, 50, 70));
                dicBackColor.Add(ThemeEnum.TicTacToe2, Color.FromArgb(53, 152, 219));
                dicBackColor.Add(ThemeEnum.TicTacToe3, Color.White);


                dicBackColor.Add(ThemeEnum.TableTennis , Color.FromArgb(30, 143, 213));
           // https://colorhunt.co/palette/9babb8eee3cbd7c0ae967e76


            }
            return dicBackColor[theme];

        }
        public static Color ForeColor(ThemeEnum theme)
        {
            if(dicForeColor ==null)
            {
                dicForeColor = new Dictionary<ThemeEnum, Color>();
                dicForeColor.Add(ThemeEnum.Gomoku1, Color.Black );
                dicForeColor.Add(ThemeEnum.Gomoku2, Color.FromArgb(42, 68, 61));
                dicForeColor.Add(ThemeEnum.Gomoku3, Color.White);                
                dicForeColor.Add(ThemeEnum.Gomoku4, Color.FromArgb(96, 108, 93));
                dicForeColor.Add(ThemeEnum.Gomoku5 , Color.Black);
                dicForeColor.Add(ThemeEnum.TicTacToe1, Color.White);
                dicForeColor.Add(ThemeEnum.TicTacToe2, Color.White);
                dicForeColor.Add(ThemeEnum.TicTacToe3, Color.Black);
                dicForeColor.Add(ThemeEnum.TableTennis, Color.White);
            }
            return dicForeColor[theme];
        }
        public static Theme Create(ThemeEnum theme)
        {
            
            switch (theme)
            {
                case ThemeEnum.Gomoku1:

                    return new GomokuThemeBuilder()
                        .BoardImageFile(BoardFileName )
                        .WhiteStoneImagePath(WhiteStoneFilePath )
                        .BlackStoneImagePath(BlackStoneFilePath)
                        .Build();

                case ThemeEnum.Gomoku2:
                    return new GomokuThemeBuilder()
                        .BoardBackColor(Color.FromArgb(167, 145, 82))
                        .WhiteStoneBackColor(Color.FromArgb(238, 232, 213))
                        .WhiteStoneBorderColor(Color.Black)
                        .BlackStoneBackColor(Color.FromArgb(0, 43, 54))
                        .NotationForecolor (Color.FromArgb (42,68,61))
                        .PenTable (ShareGraphicObject.Pen (Color.FromArgb (37,61,54),0.8f))
                        .PenBorder (ShareGraphicObject.Pen(Color.FromArgb(37, 61, 54), 2f))
                        .Build();
                case ThemeEnum.Gomoku3:
                    return  new GomokuThemeBuilder()
                            .BoardBackColor(Color.FromArgb(0, 43, 54))
                            .WhiteStoneBackColor(Color.FromArgb(238, 232, 213))
                            .WhiteStoneBorderColor(Color.FromArgb(88, 110, 117))
                            .BlackStoneBackColor(Color.FromArgb(88, 110, 117))
                            .BlackStoneBorderColor(Color.FromArgb(88, 110, 117))
                            .NotationForecolor (Color.White)
                            .PenTable(ShareGraphicObject.Pen(Color.FromArgb(66, 93, 102), 1.8f))
                            .Build();

                case ThemeEnum.Gomoku4:
                  return new   GomokuThemeBuilder()
                           .BoardBackColor(Color.FromArgb(215, 192, 174))
                           .WhiteStoneBackColor(Color.FromArgb(150, 126, 118))
                           .WhiteStoneBorderColor(Color.FromArgb(150, 126, 118))
                           .BlackStoneBackColor(Color.FromArgb(238, 227, 203))
                           .BlackStoneBorderColor(Color.FromArgb(238, 227, 203))
                           .NotationForecolor(Color.FromArgb(96, 108, 93))
                           .PenTable(ShareGraphicObject.Pen(Color.FromArgb(66, 93, 102), 1.8f))
                           .Build();

                case ThemeEnum.Gomoku5:
                    return new GomokuThemeBuilder()

                             
                             .BoardBackColor(Color.FromArgb(196, 215, 178))
                             .BlackStoneBackColor(Color.FromArgb(247, 255, 229))
                             .BlackStoneBorderColor(Color.FromArgb(247, 255, 229))
                             .WhiteStoneBackColor(Color.FromArgb(160, 196, 157))
                             .WhiteStoneBorderColor(Color.FromArgb(160, 196, 157))
                             .NotationForecolor(Color.Black)
                             .PenTable(ShareGraphicObject.Pen(Color.FromArgb(66, 93, 102), 1.8f))
                             .Build();
                case ThemeEnum.TicTacToe1:
                    return new SleekTheme();
                case ThemeEnum.TicTacToe2:
                    return new TicTacToe2Theme();
                case ThemeEnum.TicTacToe3:
                    return new TicTacToe3Theme();
                case ThemeEnum.TableTennis:
                    return new TableTennisTheme();

                default:
                    throw new ArgumentException("ThemeEnum is not valid");
            }
            
        }
    }
}
