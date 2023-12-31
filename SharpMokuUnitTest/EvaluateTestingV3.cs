using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMoku;


namespace KGoMokuUnitTest
{
    [TestClass]
    public class EvaluateTestingV3
    {
        public bool IsListValueEqual(List<int> listCellValue, List<int> listVerify)
        {
            if (listCellValue.Count != listVerify.Count)
            {
                return false;
            }
            int i;
            for (i = 0; i < listCellValue.Count; i++)
            {
                if (listCellValue[i] != listVerify[i])
                {
                    return false;
                }
            }
            return true;
        }
        [TestMethod]
        public void GetCellValueInDirection()
        {
            SharpMoku.AI.EvaluateV3 evo = new SharpMoku.AI.EvaluateV3();
            Board board = new Board(15);
            board.PutStone(0, 0, Board.CellValue.White);
            board.PutStone(0, 1, Board.CellValue.White);
            board.PutStone(0, 2, Board.CellValue.White);
            board.PutStone(0, 3, Board.CellValue.White);
            board.PutStone(0, 4, Board.CellValue.White);

            Position currentPosition = new Position(0, 0);
            Position positionDeltaNorthSouth = new Position(1, 0);
            Position positionDeltaWestEast = new Position(0, 1);
            Position positionDeltaNorthEast = new Position(1, 1);
            Position positionDeltaSouthWest = new Position(1, -1);

            Board.CellValue cellValue = Board.CellValue.White;

            List<int> listNorthSouth = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthSouth);
            List<int> listWestEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaWestEast);
            List<int> listNorthEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthEast);
            List<int> listSouthWest = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaSouthWest);
            Assert.AreEqual(5, listNorthSouth.Count);
            Assert.AreEqual(5, listWestEast.Count);
            Assert.AreEqual(5, listNorthEast.Count);
            Assert.AreEqual(1, listSouthWest.Count);

            List<int> listNorthSouthExpectedValue = new List<int> { 1, 0, 0, 0, 0 };
            List<int> listWestEastExpectedValue = new List<int> { 1, 1, 1, 1, 1 };
            List<int> listNorthEastExpectedValue = new List<int> { 1, 0, 0, 0, 0 };
            List<int> listSoutWestExpectedValue = new List<int> { 1 };
            int i;
            Assert.AreEqual(true, IsListValueEqual(listNorthSouth, listNorthSouthExpectedValue));
            Assert.AreEqual(true, IsListValueEqual(listWestEast, listWestEastExpectedValue));
            Assert.AreEqual(true, IsListValueEqual(listNorthEast, listNorthEastExpectedValue));
            Assert.AreEqual(true, IsListValueEqual(listSouthWest, listSoutWestExpectedValue));



            board.PutStone(0, 1);
            listNorthSouth = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthSouth);
            listWestEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaWestEast);
            listNorthEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthEast);
            listSouthWest = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaSouthWest);

            Assert.AreEqual(5, listNorthSouth.Count);
            Assert.AreEqual(2, listWestEast.Count);
            Assert.AreEqual(5, listNorthEast.Count);
            Assert.AreEqual(1, listSouthWest.Count);


            Position blankCellPosition = new Position(10, 10);
            listNorthSouth = evo.GetCellValueInDirection(board.Matrix, cellValue, blankCellPosition, positionDeltaNorthSouth);
            listWestEast = evo.GetCellValueInDirection(board.Matrix, cellValue, blankCellPosition, positionDeltaWestEast);
            listNorthEast = evo.GetCellValueInDirection(board.Matrix, cellValue, blankCellPosition, positionDeltaNorthEast);
            listSouthWest = evo.GetCellValueInDirection(board.Matrix, cellValue, blankCellPosition, positionDeltaSouthWest);
            Assert.AreEqual(0, listNorthSouth.Count);
            Assert.AreEqual(0, listWestEast.Count);
            Assert.AreEqual(0, listNorthEast.Count);
            Assert.AreEqual(0, listSouthWest.Count);



            currentPosition = new Position(10, 10);

            board.PutStone(currentPosition, Board.CellValue.White);
            listNorthSouth = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthSouth);
            listWestEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaWestEast);
            listNorthEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthEast);
            listSouthWest = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaSouthWest);
            Assert.AreEqual(9, listNorthSouth.Count);
            Assert.AreEqual(9, listWestEast.Count);
            Assert.AreEqual(9, listNorthEast.Count);
            Assert.AreEqual(9, listSouthWest.Count);


            listNorthSouthExpectedValue = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0 };
            listWestEastExpectedValue = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0 };
            listNorthEastExpectedValue = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0 };
            listSoutWestExpectedValue = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0 };

            Assert.AreEqual(true, IsListValueEqual(listNorthSouth, listNorthSouthExpectedValue));
            Assert.AreEqual(true, IsListValueEqual(listWestEast, listWestEastExpectedValue));
            Assert.AreEqual(true, IsListValueEqual(listNorthEast, listNorthEastExpectedValue));
            Assert.AreEqual(true, IsListValueEqual(listSouthWest, listSoutWestExpectedValue));



            board.PutStone(9, 10, Board.CellValue.Black);
            listNorthSouth = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthSouth);
            listWestEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaWestEast);
            listNorthEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthEast);
            listSouthWest = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaSouthWest);
            Assert.AreEqual(6, listNorthSouth.Count);
            Assert.AreEqual(9, listWestEast.Count);
            Assert.AreEqual(9, listNorthEast.Count);
            Assert.AreEqual(9, listSouthWest.Count);



            listNorthSouthExpectedValue = new List<int> { -1, 1, 0, 0, 0, 0 };
            listWestEastExpectedValue = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0 };
            listNorthEastExpectedValue = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0 };
            listSoutWestExpectedValue = new List<int> { 0, 0, 0, 0, 1, 0, 0, 0, 0 };

            Assert.AreEqual(true, IsListValueEqual(listNorthSouth, listNorthSouthExpectedValue));
            Assert.AreEqual(true, IsListValueEqual(listWestEast, listWestEastExpectedValue));
            Assert.AreEqual(true, IsListValueEqual(listNorthEast, listNorthEastExpectedValue));
            Assert.AreEqual(true, IsListValueEqual(listSouthWest, listSoutWestExpectedValue));



            board.PutStone(9, 11, Board.CellValue.Black);
            listNorthSouth = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthSouth);
            listWestEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaWestEast);
            listNorthEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthEast);
            listSouthWest = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaSouthWest);
            Assert.AreEqual(6, listNorthSouth.Count);
            Assert.AreEqual(9, listWestEast.Count);
            Assert.AreEqual(9, listNorthEast.Count);
            Assert.AreEqual(6, listSouthWest.Count);

            board.PutStone(11, 11, Board.CellValue.Black);
            listNorthSouth = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthSouth);
            listWestEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaWestEast);
            listNorthEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthEast);
            listSouthWest = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaSouthWest);
            Assert.AreEqual(6, listNorthSouth.Count);
            Assert.AreEqual(9, listWestEast.Count);
            Assert.AreEqual(6, listNorthEast.Count);
            Assert.AreEqual(6, listSouthWest.Count);


            board.PutStone(11, 9, Board.CellValue.Black);
            listNorthSouth = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthSouth);
            listWestEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaWestEast);
            listNorthEast = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaNorthEast);
            listSouthWest = evo.GetCellValueInDirection(board.Matrix, cellValue, currentPosition, positionDeltaSouthWest);
            Assert.AreEqual(6, listNorthSouth.Count);
            Assert.AreEqual(9, listWestEast.Count);
            Assert.AreEqual(6, listNorthEast.Count);
            Assert.AreEqual(3, listSouthWest.Count);
        }

        [TestMethod]
        public void GetListAllDirection()
        {
            SharpMoku.AI.EvaluateV3 evo = new SharpMoku.AI.EvaluateV3();
            Board board = new Board(15);
            Position currentPosition = new Position(10, 10);

            board.PutStone(currentPosition, Board.CellValue.White);
            List<List<int>> listCheckArea = evo.GetListAllDirection(board, currentPosition, Board.CellValue.White);

            Assert.AreEqual(4, listCheckArea.Count);
            Assert.AreEqual(9, listCheckArea[0].Count);
            Assert.AreEqual(9, listCheckArea[1].Count);
            Assert.AreEqual(9, listCheckArea[2].Count);
            Assert.AreEqual(9, listCheckArea[3].Count);



        }

        [TestMethod]
        public void IsInArray()
        {

            List<List<int>> Stone4WithNoBlock = new List<List<int>> { new List<int>() { 0, 1, 1, 1, 1, 0 } };
            List<List<int>> Stone3WithNoBlock = new List<List<int>>{
                        new List<int>(){ 0, 1, 1, 1, 0, 0 },
                        new List<int>(){ 0, 0, 1, 1, 1, 0 },
                        new List<int>(){ 0, 1, 0, 1, 1, 0 },
                        new List<int>(){ 0, 1, 1, 0, 1, 0 },

                    };

            SharpMoku.AI.EvaluateV3 evo = new SharpMoku.AI.EvaluateV3();
            List<int> listvalue = new List<int> { 0, 1, 1, 1, 1, 0 };
            bool isIn = evo.IsAnyInArrays(Stone4WithNoBlock, listvalue);

            Assert.AreEqual(true, isIn);
            listvalue = new List<int> { 0, 1, 0, 1, 1, 0 };
            isIn = evo.IsAnyInArrays(Stone3WithNoBlock, listvalue);
            Assert.AreEqual(true, isIn);

        }

        [TestMethod]
        public void ValuePosition()
        {
            SharpMoku.AI.EvaluateV3 evo = new SharpMoku.AI.EvaluateV3();
            Board board = new Board(15);



            Position emptyCellPosition = new Position(10, 10);
            List<List<int>> listCheckArea = evo.GetListAllDirection(board, emptyCellPosition, Board.CellValue.White);
            var numberPatternScore = evo.valuePosition(listCheckArea, false);

            Assert.IsTrue(numberPatternScore.HasZeroValue);

            board.PutStone(0, 0, Board.CellValue.White);
            board.PutStone(0, 1, Board.CellValue.White);
            board.PutStone(0, 2, Board.CellValue.White);
            board.PutStone(0, 3, Board.CellValue.White);
            board.PutStone(0, 4, Board.CellValue.White);
            Position currentPosition = new Position(0, 0);
            List<List<int>> listCheckAreaForBlack = evo.GetListAllDirection(board, currentPosition, Board.CellValue.Black);

            numberPatternScore = evo.valuePosition(listCheckAreaForBlack, true);
            Assert.IsTrue(numberPatternScore.HasZeroValue);


            listCheckArea = evo.GetListAllDirection(board, currentPosition, Board.CellValue.White);
            numberPatternScore = evo.valuePosition(listCheckArea, false);

            Assert.AreEqual(1, numberPatternScore.Winning);
            Assert.AreEqual(0, numberPatternScore.Stone4);
            Assert.AreEqual(0, numberPatternScore.Stone3);
            Assert.AreEqual(0, numberPatternScore.Stone2);
            Assert.AreEqual(0, numberPatternScore.BlockStone4);
            Assert.AreEqual(0, numberPatternScore.BlockStone3);


            board = new Board(15);

            board.PutStone(0, 0, Board.CellValue.White);
            board.PutStone(0, 1, Board.CellValue.White);
            board.PutStone(0, 2, Board.CellValue.White);
            board.PutStone(0, 3, Board.CellValue.White);

            listCheckArea = evo.GetListAllDirection(board, currentPosition, Board.CellValue.White);
            numberPatternScore = evo.valuePosition(listCheckArea, false);
            Assert.AreEqual(0, numberPatternScore.Winning);
            Assert.AreEqual(0, numberPatternScore.Stone4);
            Assert.AreEqual(0, numberPatternScore.Stone3);
            Assert.AreEqual(0, numberPatternScore.Stone2);
            Assert.AreEqual(0, numberPatternScore.BlockStone4);
            Assert.AreEqual(0, numberPatternScore.BlockStone3);


            board = new Board(15);
            board.PutStone(0, 1, Board.CellValue.White);
            board.PutStone(0, 2, Board.CellValue.White);
            board.PutStone(0, 3, Board.CellValue.White);
            board.PutStone(0, 4, Board.CellValue.White);
            currentPosition = new Position(0, 4);
            listCheckArea = evo.GetListAllDirection(board, currentPosition, Board.CellValue.White);
            numberPatternScore = evo.valuePosition(listCheckArea, false);
            Assert.AreEqual(0, numberPatternScore.Winning);
            Assert.AreEqual(1, numberPatternScore.Stone4);
            Assert.AreEqual(0, numberPatternScore.Stone3);
            Assert.AreEqual(0, numberPatternScore.Stone2);
            Assert.AreEqual(0, numberPatternScore.BlockStone4);
            Assert.AreEqual(0, numberPatternScore.BlockStone3);


            board = new Board(15);
            //0, 1, 0, 1, 1, 0
            board.PutStone(0, 1, Board.CellValue.White);

            board.PutStone(0, 3, Board.CellValue.White);
            board.PutStone(0, 4, Board.CellValue.White);
            currentPosition = new Position(0, 4);
            listCheckArea = evo.GetListAllDirection(board, currentPosition, Board.CellValue.White);

            numberPatternScore = evo.valuePosition(listCheckArea, false);
            Assert.AreEqual(0, numberPatternScore.Winning);
            Assert.AreEqual(0, numberPatternScore.Stone4);
            Assert.AreEqual(1, numberPatternScore.Stone3);
            Assert.AreEqual(0, numberPatternScore.Stone2);
            Assert.AreEqual(0, numberPatternScore.BlockStone4);
            Assert.AreEqual(0, numberPatternScore.BlockStone3);


            board = new Board(15);
            // 0, 1, 0, 1, 0, 0
            board.PutStone(0, 1, Board.CellValue.White);

            board.PutStone(0, 3, Board.CellValue.White);

            currentPosition = new Position(0, 3);
            listCheckArea = evo.GetListAllDirection(board, currentPosition, Board.CellValue.White);
            numberPatternScore = evo.valuePosition(listCheckArea, false);
            Assert.AreEqual(0, numberPatternScore.Winning);
            Assert.AreEqual(0, numberPatternScore.Stone4);
            Assert.AreEqual(0, numberPatternScore.Stone3);
            Assert.AreEqual(1, numberPatternScore.Stone2);
            Assert.AreEqual(0, numberPatternScore.BlockStone4);
            Assert.AreEqual(0, numberPatternScore.BlockStone3);



            board = new Board(15);

            board.PutStone(1, 3, Board.CellValue.Black);
            board.PutStone(2, 3, Board.CellValue.Black);
            board.PutStone(4, 3, Board.CellValue.Black);
            currentPosition = new Position(4, 3);
            listCheckArea = evo.GetListAllDirection(board, currentPosition, Board.CellValue.Black);
            numberPatternScore = evo.valuePosition(listCheckArea, true);
            Assert.AreEqual(0, numberPatternScore.Winning);
            Assert.AreEqual(0, numberPatternScore.Stone4);
            Assert.AreEqual(1, numberPatternScore.Stone3);
            Assert.AreEqual(0, numberPatternScore.Stone2);
            Assert.AreEqual(0, numberPatternScore.BlockStone4);
            Assert.AreEqual(0, numberPatternScore.BlockStone3);

        }


        [TestMethod]
        public void ScoreByPattern()
        {


            bool IsMyTurn = true;
            SharpMoku.AI.EvaluateV3.NumberofScorePattern numPattern = new SharpMoku.AI.EvaluateV3.NumberofScorePattern(1, 0, 0, 0, 0, 0);
            SharpMoku.AI.EvaluateV3 evo = new SharpMoku.AI.EvaluateV3();

            int scoreByPattern = 0;
            scoreByPattern = evo.getScoreByPattern(numPattern);

            const int wonPattern = 1000000000;
            const int stone4Pattern = 100000000;
            Assert.AreEqual(scoreByPattern, wonPattern);
            numPattern.Winning = 0;
            numPattern.Stone4 = 1;

            scoreByPattern = evo.getScoreByPattern(numPattern);
            Assert.AreEqual(scoreByPattern, stone4Pattern);

            numPattern = new SharpMoku.AI.EvaluateV3.NumberofScorePattern();
            numPattern.Stone3 = 1;
            numPattern.BlockStone4 = 1;
            scoreByPattern = evo.getScoreByPattern(numPattern);
            Assert.AreEqual(scoreByPattern, SharpMoku.AI.EvaluateV3.CONST_winGuarantee / 100);

            numPattern = new SharpMoku.AI.EvaluateV3.NumberofScorePattern();
            numPattern.Stone3 = 2;
            scoreByPattern = evo.getScoreByPattern(numPattern);
            Assert.AreEqual(scoreByPattern, SharpMoku.AI.EvaluateV3.CONST_winGuarantee / 1000);

            numPattern = new SharpMoku.AI.EvaluateV3.NumberofScorePattern();
            numPattern.Stone3 = 1;
            numPattern.Stone2 = 3;
            scoreByPattern = evo.getScoreByPattern(numPattern);
            Assert.AreEqual(scoreByPattern, 40000);

            numPattern = new SharpMoku.AI.EvaluateV3.NumberofScorePattern();
            numPattern.Stone3 = 1;
            numPattern.Stone2 = 2;
            scoreByPattern = evo.getScoreByPattern(numPattern);
            Assert.AreEqual(scoreByPattern, 38000);

            numPattern = new SharpMoku.AI.EvaluateV3.NumberofScorePattern();
            numPattern.Stone3 = 1;
            numPattern.Stone2 = 1;
            scoreByPattern = evo.getScoreByPattern(numPattern);
            Assert.AreEqual(scoreByPattern, 35000);

            numPattern = new SharpMoku.AI.EvaluateV3.NumberofScorePattern();
            numPattern.Stone3 = 1;
            numPattern.Stone2 = 0;
            scoreByPattern = evo.getScoreByPattern(numPattern);
            Assert.AreEqual(scoreByPattern, 3450);


            numPattern = new SharpMoku.AI.EvaluateV3.NumberofScorePattern();
            numPattern.BlockStone4 = 1;
            numPattern.Stone2 = 3;
            scoreByPattern = evo.getScoreByPattern(numPattern);
            Assert.AreEqual(scoreByPattern, 4500);


            numPattern = new SharpMoku.AI.EvaluateV3.NumberofScorePattern();
            numPattern.BlockStone4 = 1;
            numPattern.Stone2 = 2;
            scoreByPattern = evo.getScoreByPattern(numPattern);
            Assert.AreEqual(scoreByPattern, 4200);


            numPattern = new SharpMoku.AI.EvaluateV3.NumberofScorePattern();
            numPattern.BlockStone4 = 1;
            numPattern.Stone2 = 1;
            scoreByPattern = evo.getScoreByPattern(numPattern);
            Assert.AreEqual(scoreByPattern, 4100);



            //double dblScore=    evo.evaluateBoardForOponent(board, IsMyTurn);

        }

        [TestMethod]
        public void GetScore()
        {
            SharpMoku.AI.EvaluateV3 evo = new SharpMoku.AI.EvaluateV3();
            Board board = new Board(15);
            //  Position currentPosition = new Position(10, 10);

            board.PutStone(0, 0, Board.CellValue.White);
            board.PutStone(0, 1, Board.CellValue.White);
            board.PutStone(0, 2, Board.CellValue.White);
            board.PutStone(0, 3, Board.CellValue.White);
            board.PutStone(0, 4, Board.CellValue.White);


            Board cloneBoard = board.Clone();


            double score = evo.getScore(board);// evo.getScore(board, IsThisforblack, IsThisBlackTurn);

            //Make sure that evo.getScore will not update value in the board
            BoardTesting boardTesting = new BoardTesting();
            Assert.AreEqual(true, boardTesting.IsBoardThesame(board, cloneBoard));


            board = new Board(15);
            board.PutStone(0, 1, Board.CellValue.White);
            board.PutStone(0, 2, Board.CellValue.White);
            board.PutStone(0, 3, Board.CellValue.White);
            board.PutStone(0, 4, Board.CellValue.White);
            board.PutStone(0, 5, Board.CellValue.Black);
            score = evo.getScore(board); // evo.getScore(board, IsThisforblack, IsThisBlackTurn);


            board = new Board(9);
            board.PutStone(1, 3, Board.CellValue.Black);
            board.PutStone(1, 7, Board.CellValue.White);
            board.PutStone(2, 3, Board.CellValue.Black);
            board.PutStone(2, 7, Board.CellValue.White);
            board.PutStone(4, 3, Board.CellValue.Black);
            score = evo.getScore(board);

        }
    }
}
