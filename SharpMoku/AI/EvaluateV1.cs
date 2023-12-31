using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.AI
{
    //This is just a random evaluate function for random bot
    class EvaluateV1 : IEvaluate
    {
        static Random random = new Random();
        private  static int GetRandomNumber(int min, int max)
        {
            lock (random) // synchronize
            {
                return random.Next(min, max);
            }
        }
        public double evaluateBoard(Board board, bool IsMyturn)
        {
            return GetRandomNumber(1, 1000);

        }

        public int getScore(Board board)
        {
            return GetRandomNumber(1, 1000);

        }
    }
}
