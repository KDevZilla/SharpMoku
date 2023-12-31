using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpMoku.UI;
using System.Diagnostics.Tracing;
using System.Diagnostics;
namespace KGoMokuUnitTest
{
    [TestClass]
    public class PictureBoardTesting
    {
        [TestMethod]
        public void GetBoardPosition()
        {

            PictureBoxGoMoKu pic = new PictureBoxGoMoKu(new SharpMoku.Board(15), 38, 38);
            bool IsUseNotation = true;
            int LastIndex = 14;
            SharpMoku.GomokuCellAttribute.GoBoardPositionEnum boardPo = pic.GetBoardPosition(0, 14);

            Trace.Assert(boardPo == SharpMoku.GomokuCellAttribute.GoBoardPositionEnum.TopRightCorner);

            boardPo = pic.GetBoardPosition(0, 0);
            Trace.Assert(boardPo == SharpMoku.GomokuCellAttribute.GoBoardPositionEnum.TopLeftCorner);

            boardPo = pic.GetBoardPosition(LastIndex, 0);
            Trace.Assert(boardPo == SharpMoku.GomokuCellAttribute.GoBoardPositionEnum.BottomLeftCorner);


            boardPo = pic.GetBoardPosition(LastIndex, LastIndex);
            Trace.Assert(boardPo == SharpMoku.GomokuCellAttribute.GoBoardPositionEnum.BottomRightCorner);


            boardPo = pic.GetBoardPosition(0, 1);
            Trace.Assert(boardPo == SharpMoku.GomokuCellAttribute.GoBoardPositionEnum.TopBorder);
            int i;
            for (i = 1; i <= 13; i++)
            {
                boardPo = pic.GetBoardPosition(0, i);
                Trace.Assert(boardPo == SharpMoku.GomokuCellAttribute.GoBoardPositionEnum.TopBorder);

                boardPo = pic.GetBoardPosition(LastIndex, i);
                Trace.Assert(boardPo == SharpMoku.GomokuCellAttribute.GoBoardPositionEnum.BottomBorder);

                boardPo = pic.GetBoardPosition(i, 0);
                Trace.Assert(boardPo == SharpMoku.GomokuCellAttribute.GoBoardPositionEnum.LeftBorder);


                boardPo = pic.GetBoardPosition(i, LastIndex);
                Trace.Assert(boardPo == SharpMoku.GomokuCellAttribute.GoBoardPositionEnum.RightBorder);



            }
        }
    }
}
