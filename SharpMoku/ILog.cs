using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku
{
    public interface ILog
    {
        void Log(String Message);
        void ClearLog();
        String GetLogMessage();
    }
    public class SimpleLog : ILog
    {
        //Please change it to be the real log framework such as log4Net
        private string fileName = "";
        public SimpleLog(String fileName)
        {
            this.fileName = fileName;
        }
        public void ClearLog()
        {
            try
            {

                System.IO.File.Delete(fileName);

            }
            catch (Exception ex)
            {

            }
        }
        public void Log(String Message)
        {
            // return;


            if (!Global.CurrentSettings.IsWriteLog)
            {
                return;
            }

            try
            {
                // return;
                using (StreamWriter SW = new StreamWriter(fileName, true))
                {
                    Message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ssss ") + Message;
                    SW.WriteLine(Message);
                }

            }
            catch (Exception ex)
            {

            }
        }
        /*
        public static void WriteLog(Exception ex)
        {
            WriteLog(ex.ToString());
        }
        */
        public static void WriteLog(string Message)
        {


        }




        public string GetLogMessage()
        {
            throw new NotImplementedException();
        }
    }
    public class StringBuilderLog : ILog
    {
        private StringBuilder _Builder = new StringBuilder();
        public void ClearLog()
        {
            _Builder = new StringBuilder();
        }
        public void Log(string Message)
        {

            _Builder.Append(Message).Append(Environment.NewLine);
        }
        public String GetLogMessage()
        {
            return _Builder.ToString();
        }
    }
}
