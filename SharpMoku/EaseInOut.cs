using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku
{
    public class EaseInOut
    {
        static int width = 75;
        static int frameDelay;
        //Credit
        //https://stackoverflow.com/questions/13462001/ease-in-and-ease-out-animation-formula
        //https://en.wikipedia.org/wiki/B%C3%A9zier_curve
        public static float BezierBlendEaseInOut(float time)
        {
            return time * time * (3.0f - 2.0f * time);
        }

        public static float QuadEaseInOut(float time)
        {
            float result = 0;
            if(time < 0.5f)
            {
                result= 2.0f * time * time;
            } else
            {
                result=-1.0f + (4.0f - 2.0f * time) * time;
            }
            return result;
            
        }

        public static void displayEase(float time)
        {
            float ease = QuadEaseInOut(time);

            int nextWidth = (int)(ease * width);
            Console.Write("{0}{2}{1}\r", new string(' ', nextWidth), new String(' ', width - nextWidth), 'o');
        }
        public static String  TestEastIn(System.Windows.Forms.Button B)
        {
            frameDelay = 10;
            int count = 0;
            float step = 0.01f;
            int i;
            StringBuilder strB = new StringBuilder();
                float time = 0.0f;
                while(time < 1)
                {
                    time += step;
                    float ease = BezierBlendEaseInOut(time);
                    System.Threading.Thread.Sleep(frameDelay);
                strB.Append(ease.ToString())
                .Append(Environment.NewLine);

                B.Left += (int)(ease * 10);
                }

                
            return strB.ToString();
        }
      
        

    }
}
