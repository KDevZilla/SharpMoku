using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku
{

    [Serializable]
    public class Position
    {
        public int Row = -1;
        public int Col = -1;
        public static Position Empty
        {
            get { return new Position(-1, -1); }
        }
        public bool IsEmpty
        {
            get { return Row == -1 && Col == -1; }
        }
        public Position(int pRow, int pCol)
        {
            Row = pRow;
            Col = pCol;
        }
        public int GetHashCode()
        {
            return (Row.ToString() + "_" + Col.ToString()).GetHashCode();
        }
        public Position Clone()
        {
            Position NewPostion = new Position(this.Row, this.Col);
            return NewPostion;
        }
        public string PositionString()
        {
            return Row.ToString() + "," + Col.ToString();
        }
        public bool IsEqual(Position pos2)
        {
            if (pos2 == null)
            {
                return false;
            }
            return this.PositionString() == pos2.PositionString();
        }
        public bool Is(int row, int column)
        {
            return Row == row && Col == column;
        }

    }


}
