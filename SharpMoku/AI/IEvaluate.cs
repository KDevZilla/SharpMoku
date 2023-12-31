using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.AI
{

    public interface IEvaluate
    {
        double evaluateBoard(SharpMoku.Board board, Boolean IsMyturn);
        int getScore(SharpMoku.Board board);

    }

}