using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace SharpMoku
{
    public class ShareGraphicObject
    {
        // This class is a flyweight that contain all of the grahpic object





        private static String GetHexfromColor(Color color)
        {
            return color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

        }

        private static Dictionary<Color, SolidBrush > solidBrushDictionary = null;
        private static Dictionary<String, Bitmap> bitmapFilePathDictionary = null;
        private static Font _GoMokuBoardFont = null;
        public static Font GoMokuBoardFont
        {
            get
            {
                if (_GoMokuBoardFont == null)
                {
                    FontFamily fontFamily = new FontFamily("Segoe UI");
                       _GoMokuBoardFont = new Font(
                       fontFamily,
                       20,
                       FontStyle.Bold,
                       GraphicsUnit.Pixel);
                }
                return _GoMokuBoardFont;
            }
            set
            {
                _GoMokuBoardFont = value;
            }
        }
        public static Bitmap BitmapFilePath(String filePath)
        {
            if (bitmapFilePathDictionary == null)
            {
                bitmapFilePathDictionary = new Dictionary<string, Bitmap>();
            }
            if(!bitmapFilePathDictionary.ContainsKey(filePath))
            {
                Bitmap bitmap = new Bitmap(filePath);
                bitmapFilePathDictionary.Add(filePath, bitmap);
            }
            return bitmapFilePathDictionary[filePath];
        }
        public static SolidBrush  SolidBrush(Color color)
        {
            if (solidBrushDictionary == null)
            {
                solidBrushDictionary = new Dictionary<Color, SolidBrush >();

            }
            if (!solidBrushDictionary.ContainsKey(color))
            {
                solidBrushDictionary.Add(color, new SolidBrush(color));
            }

            return solidBrushDictionary[color];
        }

        private static Dictionary<String, Pen> penDictionary = null;
        public static Pen Pen(Color color,float width)
        {
            if (penDictionary  == null)
            {
                penDictionary = new Dictionary<String, Pen>();

            }
            string key = GetHexfromColor(color) + "_" + width.ToString();
            if (!penDictionary.ContainsKey(key))
            {
                Pen pen = new Pen(color, width);
                penDictionary.Add(key, pen);
                
            }
            return penDictionary[key];

        }
    }
}
