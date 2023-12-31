using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.Utility
{
    public class SerializeUtility
    {
        public static void CreateNewSettings(String filename)
        {
            SharpMokuSettings sta = new SharpMokuSettings();
            Serailze(sta, filename);
        }
        public static void SerializeSettings(SharpMokuSettings setting, String filename)
        {
            Serailze(setting, filename);
        }
        public static SharpMokuSettings DeserializeSettings(String filename)
        {
            object obj = Deserialize(filename);
            SharpMokuSettings setting = (SharpMokuSettings)obj;
            return setting;
        }

        public static void SerializeBoard(SharpMoku.Board board, String filename)
        {
            Serailze(board, filename);
        }
        public static SharpMoku.Board DeserializeBoard(String filename)
        {
            object obj = Deserialize(filename);
            SharpMoku.Board board = (Board)obj;
            return board;
        }
        private static void Serailze(object obj, String filename)
        {
            System.IO.Stream ms = File.OpenWrite(filename);
            //Format the object as Binary  

            BinaryFormatter formatter = new BinaryFormatter();
            //It serialize the employee object  
            formatter.Serialize(ms, obj);
            ms.Flush();
            ms.Close();
            ms.Dispose();
        }

        private static object Deserialize(String filename)
        {
            //Format the object as Binary  
            BinaryFormatter formatter = new BinaryFormatter();

            //Reading the file from the server  
            FileStream fs = File.Open(filename, FileMode.Open);

            object obj = formatter.Deserialize(fs);
            // Statistics sta = (Statistics)obj;
            fs.Flush();
            fs.Close();
            fs.Dispose();
            return obj;

        }
    }
}
