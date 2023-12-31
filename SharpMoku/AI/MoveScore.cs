using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.AI
{
    // Contain move postion and calculated score from Minimax
    public struct  MoveScore
    {
        public double Score { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public MoveScore(double pScore)
        {
            Score = pScore;
            Row = -1;
            Col = -1;
        }
        public Position GetPosition()
        {
            return new Position(this.Row, this.Col);
        }
        public static  MoveScore Max(MoveScore moveScore1, MoveScore moveScore2)
        {
            if(moveScore1.Score > moveScore2.Score)
            {
                return new MoveScore(moveScore1.Score, moveScore1.Row, moveScore1.Col);
                
            }
            return new MoveScore(moveScore2.Score, moveScore2.Row, moveScore2.Col);
        }
        public static MoveScore Min(MoveScore moveScore1, MoveScore moveScore2)
        {
            if (moveScore1.Score < moveScore2.Score)
            {
                return new MoveScore(moveScore1.Score, moveScore1.Row, moveScore1.Col);
            }
            return new MoveScore(moveScore2.Score, moveScore2.Row, moveScore2.Col);
        }
        public MoveScore(double Score, int Row, int Col)
        {
            this.Score = Score;
            this.Row = Row;
            this.Col = Col;
        }
    }
}
