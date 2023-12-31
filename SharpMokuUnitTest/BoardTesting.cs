using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpMoku.UI;
using System.Diagnostics.Tracing;
using System.Diagnostics;
using SharpMoku;
using System.Collections.Generic;

namespace KGoMokuUnitTest
{
    [TestClass]
    public class BoardTesting
    {
        [TestMethod]
        public void PutStoneInvalidStoneValue()
        {
            Board board = new Board(15);
            try
            {
                board.PutStone(-1, 0, 2);
                Trace.Fail("Must not allow to put into 2 value");
            }
            catch
            {

            }
        }
        [TestMethod]
        public void PutStoneInvalidPosition()
        {
            Board board = new Board(15);
            try
            {
                board.PutStone(-1, 0, Board.CellValue.Black);
                Trace.Fail("Must not allow to put into -1,0 position");
            }
            catch
            {

            }

            int[,] arrInvalidPosition = new int[,]
            {
                {-1,-1 },
                {15,0 },
                {15,15 },
                {15,-5 }
            };
            int i;
            for (i = 0; i < arrInvalidPosition.Length; i++)
            {
                try
                {
                    board.PutStone(arrInvalidPosition[i, 0],
                        arrInvalidPosition[i, 1], Board.CellValue.Black);
                    Trace.Fail(String.Format("Must not allow to put into {0},{1} position ",
                        arrInvalidPosition[i, 0],
                        arrInvalidPosition[i, 1]));
                }
                catch
                {

                }
            }

        }
        public bool IsDicContainKey(Dictionary<String, Position> dicPosition, List<Position> listPosition)
        {
            foreach (Position position in listPosition)
            {
                if (!dicPosition.ContainsKey(position.PositionString()))
                {
                    return false;
                }
            }
            return true;
        }
        [TestMethod]
        public void PutStone()
        {
            Position position = null;

            Board board = new Board(15);
            position = new Position(0, 0);
            Assert.IsTrue(board.IsEmpty);

            board.PutStone(position, Board.CellValue.Black);

            Assert.IsFalse(board.IsEmpty);

            Assert.IsTrue(board.Matrix[0, 0] == (int)Board.CellValue.Black);

            List<Position> listNeighbor = new List<Position>()
            {
                new Position (0,1),
                new Position (1,0),
                new Position (1,1)
            };

            Assert.IsTrue(board.CheckWinStatus() == Board.WinStatus.NotDecidedYet);
            Assert.IsTrue(board.listHistory.Count == 1);
            Assert.IsTrue(board.dicBlackStone.Count == 1);
            Assert.IsTrue(board.dicWhiteStone.Count == 0);
            Assert.IsTrue(board.CurrentTurn == Board.Turn.Black);
            Assert.IsTrue(board.dicBlackStone.ContainsKey(position.PositionString()));
            Assert.IsTrue(board.dicNeighbor.Count == 3);
            Assert.IsTrue(IsDicContainKey(board.dicNeighbor, listNeighbor));


            position = new Position(0, 1);
            board.PutStone(position, Board.CellValue.White);
            listNeighbor = new List<Position>()
            {
                new Position (0,2),
                new Position (1,0),
                new Position (1,1),
                new Position (1,2)
            };


            Assert.IsTrue(board.Matrix[0, 1] == (int)Board.CellValue.White);

            Assert.IsTrue(board.CheckWinStatus() == Board.WinStatus.NotDecidedYet);
            Assert.IsTrue(board.listHistory.Count == 2);
            Assert.IsTrue(board.dicBlackStone.Count == 1);
            Assert.IsTrue(board.dicWhiteStone.Count == 1);
            Assert.IsTrue(board.CurrentTurn == Board.Turn.Black);
            Assert.IsTrue(board.dicWhiteStone.ContainsKey(position.PositionString()));
            Assert.IsTrue(board.dicNeighbor.Count == 4);
            Assert.IsTrue(IsDicContainKey(board.dicNeighbor, listNeighbor));

            board = new Board(15);
            position = new Position(5, 5);
            board.PutStone(position, Board.CellValue.White);

            listNeighbor = new List<Position>()
            {
                new Position (4,4),
                new Position (4,5),
                new Position (4,6),
                new Position (5,6),
                new Position (6,6),
                new Position (6,5),
                new Position (6,4),
                new Position (5,4),
            };
            Assert.IsTrue(board.Matrix[5, 5] == (int)Board.CellValue.White);

            Assert.IsTrue(board.CheckWinStatus() == Board.WinStatus.NotDecidedYet);
            Assert.IsTrue(board.listHistory.Count == 1);
            Assert.IsTrue(board.dicBlackStone.Count == 0);
            Assert.IsTrue(board.dicWhiteStone.Count == 1);
            Assert.IsTrue(board.dicWhiteStone.ContainsKey(position.PositionString()));
            Assert.IsTrue(board.CurrentTurn == Board.Turn.Black);
            Assert.IsTrue(board.dicNeighbor.Count == 8);
            Assert.IsTrue(IsDicContainKey(board.dicNeighbor, listNeighbor));

            board.PutStone(5, 6, Board.CellValue.Black);
            listNeighbor = new List<Position>()
            {
                new Position (4,4),
                new Position (4,5),
                new Position (4,6),
                new Position (4,7),

                new Position (5,7),
                new Position (6,7),
                new Position (6,6),
                new Position (6,5),
                new Position (6,4),
                new Position (5,4),
            };
            Assert.IsTrue(board.CheckWinStatus() == Board.WinStatus.NotDecidedYet);
            Assert.IsTrue(board.listHistory.Count == 2);
            Assert.IsTrue(board.dicBlackStone.Count == 1);
            Assert.IsTrue(board.dicWhiteStone.Count == 1);
            Assert.IsTrue(board.CurrentTurn == Board.Turn.Black);
            Assert.IsTrue(board.dicNeighbor.Count == 10);
            Assert.IsTrue(IsDicContainKey(board.dicNeighbor, listNeighbor));

            board.PutStone(5, 8, Board.CellValue.White);
            listNeighbor = new List<Position>()
            {
                new Position (4,4),
                new Position (4,5),
                new Position (4,6),
                new Position (4,7),

                new Position (5,7),
                new Position (6,7),
                new Position (6,6),
                new Position (6,5),
                new Position (6,4),
                new Position (5,4),

                new Position (4,8),
                new Position (4,9),
                new Position (5,9),
                new Position (6,8),
                new Position (6,9)
            };
            Assert.IsTrue(board.CheckWinStatus() == Board.WinStatus.NotDecidedYet);
            Assert.IsTrue(board.listHistory.Count == 3);
            Assert.IsTrue(board.dicBlackStone.Count == 1);
            Assert.IsTrue(board.dicWhiteStone.Count == 2);
            Assert.IsTrue(board.CurrentTurn == Board.Turn.Black);
            Assert.IsTrue(board.dicNeighbor.Count == 15);
            Assert.IsTrue(IsDicContainKey(board.dicNeighbor, listNeighbor));

            Position lastPosition = board.LastPositionPut;
            Assert.IsTrue(lastPosition.Row == 5 && lastPosition.Col == 8);
            //  Assert.IsTrue (board.dic
        }

        [TestMethod]
        public void PutStoneAndSwitchTurn()
        {
            Board board = new Board(15);
            Assert.IsTrue(board.CurrentTurn == Board.Turn.Black);
            Assert.IsTrue(board.IsEmpty);


            board.PutStoneAndSwitchTurn(0, 0, Board.CellValue.Black);
            Assert.IsTrue(board.Matrix[0, 0] == (int)Board.CellValue.Black);

            Assert.IsTrue(board.CurrentTurn == Board.Turn.White);
            board.PutStoneAndSwitchTurn(0, 1, Board.CellValue.White);
            Assert.IsTrue(board.Matrix[0, 1] == (int)Board.CellValue.White);
            Assert.IsTrue(board.CurrentTurn == Board.Turn.Black);


        }

        [TestMethod]
        public void BoardStatusNotDicideYet()
        {
            Board board = new Board(15);
            Assert.IsTrue(board.IsEmpty);

            board.PutStone(0, 0, Board.CellValue.Black);

            Board.WinStatus winStatus = Board.WinStatus.NotDecidedYet;
            winStatus = board.CheckWinStatus();
            Assert.IsTrue(winStatus == Board.WinStatus.NotDecidedYet);



        }

        [TestMethod]
        public void WinStatusWestAndEast()
        {
            Board board = new Board(15);
            Assert.IsTrue(board.IsEmpty);


            board.PutStone(0, 0, Board.CellValue.Black);
            board.PutStone(0, 1, Board.CellValue.Black);
            board.PutStone(0, 2, Board.CellValue.Black);
            board.PutStone(0, 3, Board.CellValue.Black);
            board.PutStone(0, 4, Board.CellValue.Black);

            var winStatus = board.CheckWinStatus();
            Assert.IsTrue(winStatus == Board.WinStatus.BlackWon);


        }

        [TestMethod]
        public void WinStatusWhiteNorthAndSouth()
        {
            Board board = new Board(15);
            board.PutStone(0, 0, Board.CellValue.White);
            board.PutStone(1, 0, Board.CellValue.White);
            board.PutStone(2, 0, Board.CellValue.White);
            board.PutStone(3, 0, Board.CellValue.White);
            board.PutStone(4, 0, Board.CellValue.White);

            var winStatus = board.CheckWinStatus();
            Assert.IsTrue(winStatus == Board.WinStatus.WhiteWon);


        }

        [TestMethod]
        public void WinStatusWhiteNorthWestAndSouthEast()
        {
            Board board = new Board(15);
            board.PutStone(0, 0, Board.CellValue.White);
            board.PutStone(1, 1, Board.CellValue.White);
            board.PutStone(2, 2, Board.CellValue.White);
            board.PutStone(3, 3, Board.CellValue.White);
            board.PutStone(4, 4, Board.CellValue.White);

            var winStatus = board.CheckWinStatus();
            Assert.IsTrue(winStatus == Board.WinStatus.WhiteWon);


        }

        [TestMethod]
        public void WinStatusWhiteNorthEastAndSouthWest()
        {
            Board board = new Board(15);
            board.PutStone(0, 4, Board.CellValue.White);
            board.PutStone(1, 3, Board.CellValue.White);
            board.PutStone(2, 2, Board.CellValue.White);
            board.PutStone(3, 1, Board.CellValue.White);
            board.PutStone(4, 0, Board.CellValue.White);

            var winStatus = board.CheckWinStatus();
            Assert.IsTrue(winStatus == Board.WinStatus.WhiteWon);


        }


        [TestMethod]
        public void WinStatusBlackNorthAndSouth()
        {
            Board board = new Board(15);
            board.PutStone(0, 0, Board.CellValue.Black);
            board.PutStone(1, 0, Board.CellValue.Black);
            board.PutStone(2, 0, Board.CellValue.Black);
            board.PutStone(3, 0, Board.CellValue.Black);
            board.PutStone(4, 0, Board.CellValue.Black);

            var winStatus = board.CheckWinStatus();
            Assert.IsTrue(winStatus == Board.WinStatus.BlackWon);


        }

        [TestMethod]
        public void WinStatusBlackNorthWestAndSouthEast()
        {
            Board board = new Board(15);
            board.PutStone(0, 0, Board.CellValue.Black);
            board.PutStone(1, 1, Board.CellValue.Black);
            board.PutStone(2, 2, Board.CellValue.Black);
            board.PutStone(3, 3, Board.CellValue.Black);
            board.PutStone(4, 4, Board.CellValue.Black);

            var winStatus = board.CheckWinStatus();
            Assert.IsTrue(winStatus == Board.WinStatus.BlackWon);


        }

        [TestMethod]
        public void WinStatusBlackNorthEastAndSouthWest()
        {
            Board board = new Board(15);
            board.PutStone(0, 4, Board.CellValue.Black);
            board.PutStone(1, 3, Board.CellValue.Black);
            board.PutStone(2, 2, Board.CellValue.Black);
            board.PutStone(3, 1, Board.CellValue.Black);
            board.PutStone(4, 0, Board.CellValue.Black);

            var winStatus = board.CheckWinStatus();
            Assert.IsTrue(winStatus == Board.WinStatus.BlackWon);


        }

        [TestMethod]
        public void Draw()
        {
            Board board = new Board(9);
            int i;
            int j;
            Board.CellValue cellValue = Board.CellValue.Black;
            for (i = 0; i <= 8; i++)
            {
                if (i % 2 == 0)
                {
                    cellValue = Board.CellValue.Black;
                }
                else
                {
                    cellValue = Board.CellValue.White;
                }
                int iCountCellValue = 0;
                for (j = 0; j <= 8; j++)
                {
                    iCountCellValue++;
                    board.PutStone(new Position(i, j), cellValue);
                    if (iCountCellValue == 2)
                    {
                        if (cellValue == Board.CellValue.Black)
                        {
                            cellValue = Board.CellValue.White;
                        }
                        else
                        {
                            cellValue = Board.CellValue.Black;
                        }
                        iCountCellValue = 0;
                    }
                }
            }
            Assert.IsTrue(board.IsFull);
            var winStatus = board.CheckWinStatus();
            Assert.IsTrue(winStatus == Board.WinStatus.Draw);

            List<Position> listNeighbor = board.generateNeighboreMoves();
            Assert.IsTrue(listNeighbor.Count == 0);

            //   board.generateNeighboreMoves();
        }
        [TestMethod]
        public void Undo()
        {
            Board board = new Board(15);
            board.PutStone(0, 4, Board.CellValue.Black);
            board.PutStone(1, 3, Board.CellValue.Black);
            board.PutStone(2, 2, Board.CellValue.Black);
            board.PutStone(3, 1, Board.CellValue.Black);
            board.PutStone(4, 0, Board.CellValue.Black);
            board.PutStone(6, 6, Board.CellValue.White);
            // 
            Board NewBoard = new Board(board);
            NewBoard.PutStone(8, 8, Board.CellValue.White);

            Assert.IsTrue(!IsBoardThesame(board, NewBoard));

            NewBoard.Undo();
            board.SwitchTurn();
            Assert.IsTrue(IsBoardThesame(board, NewBoard));


            int i;

        }
        [TestMethod]
        public void GetListNeighborPosition()
        {
            Position position = null;

            Board board = new Board(15);
            position = new Position(0, 0);
            board.PutStone(position, Board.CellValue.Black);
            Assert.IsTrue(board.Matrix[0, 0] == (int)Board.CellValue.Black);

            List<Position> list = board.GetListNeighborPosition(new Position(0, 0));
            Assert.IsTrue(list.Count == 3);
            List<Position> listNeighbor00 = new List<Position>()
            {
                new Position (0,1),
                new Position (1,0),
                new Position (1,1)
            };

            int i;
            for (i = 0; i < listNeighbor00.Count; i++)
            {
                bool isExist = false;
                for (int j = 0; j < list.Count; j++)
                {
                    if (listNeighbor00[i].Row == list[j].Row &&
                        listNeighbor00[i].Col == list[j].Col)
                    {
                        isExist = true;
                    }
                }
                if (!isExist)
                {
                    Assert.Fail($"Postion {listNeighbor00[i].Row},{listNeighbor00[i].Col} does not exist");
                }
            }

        }

        [TestMethod]
        public void AdjustEmptyNeighborOf()
        {
            Position position = null;

            Board board = new Board(15);
            position = new Position(0, 0);
            board.PutStone(position, Board.CellValue.Black);
            Assert.IsTrue(board.Matrix[0, 0] == (int)Board.CellValue.Black);

            List<Position> listNeighbor00 = new List<Position>()
            {
                new Position (0,1),
                new Position (1,0),
                new Position (1,1)
            };

            Assert.IsTrue(board.CheckWinStatus() == Board.WinStatus.NotDecidedYet);
            Assert.IsTrue(board.listHistory.Count == 1);
            Assert.IsTrue(board.dicBlackStone.Count == 1);
            Assert.IsTrue(board.dicWhiteStone.Count == 0);
            Assert.IsTrue(board.CurrentTurn == Board.Turn.Black);
            Assert.IsTrue(board.dicBlackStone.ContainsKey(position.PositionString()));
            Assert.IsTrue(board.dicNeighbor.Count == 3);
            Assert.IsTrue(IsDicContainKey(board.dicNeighbor, listNeighbor00));


            position = new Position(0, 1);
            board.PutStone(position, Board.CellValue.White);
            var listNeighbor = new List<Position>()
            {
                new Position (0,2),
                new Position (1,0),
                new Position (1,1),
                new Position (1,2)
            };


            Assert.IsTrue(board.Matrix[0, 1] == (int)Board.CellValue.White);
            Assert.IsTrue(board.CheckWinStatus() == Board.WinStatus.NotDecidedYet);
            Assert.IsTrue(board.listHistory.Count == 2);
            Assert.IsTrue(board.dicBlackStone.Count == 1);
            Assert.IsTrue(board.dicWhiteStone.Count == 1);
            Assert.IsTrue(board.CurrentTurn == Board.Turn.Black);
            Assert.IsTrue(board.dicWhiteStone.ContainsKey(position.PositionString()));
            Assert.IsTrue(board.dicNeighbor.Count == 4);
            Assert.IsTrue(IsDicContainKey(board.dicNeighbor, listNeighbor));

            board.RemoveStone(new Position(0, 1));
            board.AdjustEmptyNeighborOf(new Position(0, 1));
            Assert.IsTrue(board.dicNeighbor.Count == listNeighbor00.Count);
            Assert.IsTrue(IsDicContainKey(board.dicNeighbor, listNeighbor00));

        }

        [TestMethod]
        public void Undo2Times()
        {
            Board board = new Board(15);
            board.PutStone(0, 4, Board.CellValue.Black);
            board.PutStone(1, 3, Board.CellValue.Black);
            board.PutStone(2, 2, Board.CellValue.Black);
            board.PutStone(3, 1, Board.CellValue.Black);
            board.PutStone(4, 0, Board.CellValue.Black);
            board.PutStone(6, 6, Board.CellValue.White);
            // 
            Board NewBoard = new Board(board);
            NewBoard.PutStone(8, 8, Board.CellValue.White);
            NewBoard.PutStone(7, 7, Board.CellValue.Black);

            //Assert.IsTrue(!IsBoardThesame(board, NewBoard));

            NewBoard.Undo();
            board.SwitchTurn();
            NewBoard.Undo();
            board.SwitchTurn();
            Assert.IsTrue(IsBoardThesame(board, NewBoard));


        }


        [TestMethod]
        public void CloneBoard()
        {
            Board board = new Board(9);
            board.PutStone(0, 4, Board.CellValue.Black);
            board.PutStone(1, 3, Board.CellValue.Black);
            board.PutStone(2, 2, Board.CellValue.Black);
            board.PutStone(3, 1, Board.CellValue.Black);
            board.PutStone(4, 0, Board.CellValue.Black);

            Board NewBoard = new Board(board);
            Assert.IsTrue(NewBoard.BoardSize == 9);
            Assert.IsTrue(NewBoard.Matrix[0, 4] == (int)Board.CellValue.Black);
            Assert.IsTrue(NewBoard.Matrix[1, 3] == (int)Board.CellValue.Black);
            Assert.IsTrue(NewBoard.Matrix[2, 2] == (int)Board.CellValue.Black);
            Assert.IsTrue(NewBoard.Matrix[3, 1] == (int)Board.CellValue.Black);
            Assert.IsTrue(NewBoard.Matrix[4, 0] == (int)Board.CellValue.Black);
            //Assert.IsTrue(NewBoard.Matrix[0, 4] == (int)Board.CellValue.Black);
            Assert.IsTrue(NewBoard.dicBlackStone.Count == 5);
            Assert.IsTrue(NewBoard.dicWhiteStone.Count == 0);

            Assert.IsTrue(IsBoardThesame(board, NewBoard));
        }
        public Boolean IsBoardThesame(Board board1, Board board2)
        {
            if (board1.CurrentTurn != board2.CurrentTurn)
            {
                return false;
            }
            if (board1.CurrentTurnCellValue != board2.CurrentTurnCellValue)
            {
                return false;
            }
            if (board1.CanUndo != board2.CanUndo)
            {
                return false;
            }
            if (board1.listHistory != null && board2.listHistory == null)
            {
                return false;
            }
            if (board2.listHistory != null && board1.listHistory == null)
            {
                return false;
            }

            if (board1.listHistory.Count != board2.listHistory.Count)
            {
                return false;
            }

            if (board1.CheckWinStatus() != board2.CheckWinStatus())
            {
                return false;
            }

            if (board1.dicNeighbor.Count != board2.dicNeighbor.Count)
            {
                return false;
            }
            if (board1.BoardSize != board2.BoardSize)
            {
                return false;
            }
            if (board1.dicBlackStone.Count != board2.dicBlackStone.Count)
            {
                return false;
            }
            if (board1.dicWhiteStone.Count != board2.dicWhiteStone.Count)
            {
                return false;
            }
            int i;
            int j;
            for (i = 0; i < board1.BoardSize; i++)
            {
                for (j = 0; j < board1.BoardSize; j++)
                {
                    if (board1.Matrix[i, j] != board2.Matrix[i, j])
                    {
                        return false;
                    }
                }
            }
            foreach (String posString in board1.dicWhiteStone.Keys)
            {
                if (!board1.dicWhiteStone[posString].IsEqual(
                     board2.dicWhiteStone[posString]))
                {
                    return false;
                }

            }
            foreach (String posString in board1.dicNeighbor.Keys)
            {
                if (!board1.dicNeighbor[posString].IsEqual(
                     board2.dicNeighbor[posString]))
                {
                    return false;
                }

            }
            foreach (String posString in board1.dicBlackStone.Keys)
            {
                if (!board1.dicBlackStone[posString].IsEqual(
                     board2.dicBlackStone[posString]))
                {
                    return false;
                }

            }

            for (i = 0; i < board1.listHistory.Count; i++)
            {
                if (!board1.listHistory[i].IsEqual(board2.listHistory[i]))
                {
                    return false;
                }
            }

            return true;
        }
        [TestMethod]
        public void WinStatusProblem()
        {
            String fileName = @"D:\CODE\visual studio 2010\Projects\SharpMoku\SharpMoku\SharpMoku\bin\Debug\AppInfo\Board.bin";
            Board board = SharpMoku.Utility.SerializeUtility.DeserializeBoard(fileName);
            var winStatus = board.CheckWinStatus();
            Assert.IsTrue(winStatus == Board.WinStatus.NotDecidedYet);
        }
    }
}
