using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.Utility
{
    public class FileUtility
    {
        public static string AppInfoPath
        {
            get
            {
                String ExePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
                return Path.GetDirectoryName(ExePath) + @"\AppInfo";
            }
        }
        public static string SettingPath => $@"{AppInfoPath}\Setting.bin";
        public static string ResourcesPath => $@"{AppInfoPath}\Resources";
        public static string BoardPath => $@"{AppInfoPath}\Board\";
        public static string LogFilePath => $@"{AppInfoPath}\Log.txt";


        public static bool IsFileExist(String fileName) => System.IO.File.Exists(fileName);



        public static void CopyFileIfItIsDifferentPath(String original, String destination)
        {
            Boolean NeedtoCopy = true;
            if (original.Trim().ToLower() ==
            destination.Trim().ToLower())
            {
                NeedtoCopy = false;
            }
            if (NeedtoCopy)
            {
                System.IO.File.Copy(original, destination, true);
            }
        }


        public static Image ReadImageWithoutLockFile(String fileName)
        {
            Image img;
            using (var bmpTemp = new Bitmap(fileName))
            {
                img = new Bitmap(bmpTemp);
            }
            return img;
        }
    }
}
