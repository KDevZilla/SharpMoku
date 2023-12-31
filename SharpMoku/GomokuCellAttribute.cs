using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku
{
    public class GomokuCellAttribute
    {

        public GoBoardPositionEnum GoboardPosition = GoBoardPositionEnum.Center;
        public enum GoBoardPositionEnum
        {
            LeftNotation,
            RightNotation,
            TopNotation,
            BottomNotation,
            TopLeftCorner,
            TopRightCorner,
            BottomLeftCorner,
            BottomRightCorner,
            LeftBorder,
            RightBorder,
            TopBorder,
            BottomBorder,
            Center
        }
        public int Row { get; set; }
        public int Col { get; set; }
        public Boolean IsIntersection = false;
        public Board.CellValue CellValue { get; set; }
        public Boolean IsNeighborCell { get; set; } = false;

    }
}
