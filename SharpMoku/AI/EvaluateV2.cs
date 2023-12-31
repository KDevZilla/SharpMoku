using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.AI
{
    /* 
     * https://blog.theofekfoundation.org/artificial-intelligence/2015/12/11/minimax-for-gomoku-connect-five/
     * 
     */
    /* This class is not used anymore
     * We just keep it for studying a brade and butter of a 5 in a row evaluate function.
     */
    public class EvaluateV2 : IEvaluate
    {

        public double evaluateBoard(Board board, bool IsMyturn)
        {

            return calculateBoardScoreForOpponenet(board, IsMyturn);
        }



        public enum BlockType
        {
            ZeroBlock = 0,
            OneSideBlock = 1,
            BothSideBlock = 2
        }
        public struct StonePattern
        {
            public int NumberOfConsecutiveStone;
            public BlockType blockType;
            public StonePattern(int numberOfConsecutiveStone, BlockType blockType)
            {
                this.NumberOfConsecutiveStone = numberOfConsecutiveStone;
                this.blockType = blockType;
            }
        }
        public double calculateBoardScoreForOpponenet(Board board, Boolean isMyturn)
        {

            Boolean IsItforMe = true;
            double MyScore = getScore(board, IsItforMe, isMyturn);
            double OponenetScore = getScore(board, !IsItforMe, isMyturn);

            if (MyScore == 0)
            {
                MyScore = 1.0;
            }
            return OponenetScore / MyScore;

        }


        public int getScore(Board board, Boolean isForBlackStone, Boolean isBlackTurn)
        {


            int[,] boardMatrix = board.Matrix;
            // Calculate score for each of the 3 directions

            int horizontalScore = CalculateScoreForHorizontal(boardMatrix, isForBlackStone, isBlackTurn);
            int verticalScore = CalculateScoreForVertical(boardMatrix, isForBlackStone, isBlackTurn);
            int DiagonalScore = CalculateScoreForDiagonal(boardMatrix, isForBlackStone, isBlackTurn);


            return horizontalScore + verticalScore + DiagonalScore;
        }

        public int getScore(Board board)
        {
            Position LatestPosition = board.LastPositionPut;

            
            SharpMoku.Board.CellValue cellValue = board.CurrentTurnCellValue;
            SharpMoku.Board.CellValue opponentCellValue = (SharpMoku.Board.CellValue)
                                   (-(int)cellValue);

            bool isBlackTurn = (cellValue == SharpMoku.Board.CellValue.Black);

            
            var blackStoneValue = getScore(board, isForBlackStone: true, isBlackTurn: isBlackTurn);
            var whiteStonevalue = getScore(board, isForBlackStone: false, isBlackTurn: isBlackTurn);

            if (cellValue == Board.CellValue.Black)
            {
                return blackStoneValue - whiteStonevalue;
            }
            else
            {
                return whiteStonevalue - blackStoneValue;
            }
          
        }
        private int leastStoneForConsecutive = 2;
        public List<StonePattern> GetStonePatternForHorizontal(int[,] boardMatrix, Boolean isForBlackStone)
        {
            List<StonePattern> listPattern = new List<StonePattern>();
            int numberOfConsecutiveStone = 0;

            BlockType blockType = BlockType.BothSideBlock;

            int targetcellValue = isForBlackStone
                ? blackStoneCellvalue
                : whiteStoneCellvalue;

            // Row loop
            for (int indexRow = 0; indexRow < boardMatrix.GetLength(0); indexRow++)
            {
                // Column loop
                for (int indexColumn = 0; indexColumn < boardMatrix.GetLength(1); indexColumn++)
                {
                    
                    var currentCellValue = boardMatrix[indexRow, indexColumn];
                    if (currentCellValue == targetcellValue)
                    {
                        
                        numberOfConsecutiveStone++;
                        continue;
                    }
                    
                    if (currentCellValue == emptyCellvalue)
                    {
                        
                        if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                        {

                            blockType--;
                            listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));
                            numberOfConsecutiveStone = 0;
                            blockType =  BlockType.OneSideBlock;
                        }
                        else
                        {
                            blockType = BlockType.OneSideBlock;
                        }
                        continue;
                    }
                    // Cell is occupied by opponent
                    if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                    {
                        listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));
                        numberOfConsecutiveStone = 0;
                        blockType = BlockType.BothSideBlock;
                        continue;
                    }

                    // Current cell is occupied by opponent, next consecutive set may have 2 blocked sides
                    blockType = BlockType.BothSideBlock;

                }
                // End of row, check if there were any consecutive stones before we reached right border
                if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                {
                    listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));
                    //score += getConsecutiveSetScore(numberOfConsecutiveStone, numberOfBlocks, isForBlackStone == isBlackTurn);
                }
                // Reset consecutive stone and blocks count
                numberOfConsecutiveStone = 0;
                blockType = BlockType.BothSideBlock;
            }

            return listPattern;
        }

        public int CalculateScoreForHorizontal(int[,] boardMatrix, Boolean isForBlackstone, Boolean isBlackTurn)
             => GetStonePatternForHorizontal(boardMatrix, isForBlackstone)
                .Sum(x => calculateConsecutiveStoneSequenceScore(x, isBlackTurn));

        public int CalculateScoreForVertical(int[,] boardMatrix, Boolean isForBlackstone, Boolean isBlackTurn)
            => GetStonePatternForVertical(boardMatrix, isForBlackstone)
                .Sum(x => calculateConsecutiveStoneSequenceScore(x, isBlackTurn));

        public int CalculateScoreForDiagonal(int[,] boardMatrix, Boolean isForBlackStone, Boolean isBlackTurn)
            => GetStonePatternForDiagonal(boardMatrix, isForBlackStone)
                .Sum(x => calculateConsecutiveStoneSequenceScore(x, isBlackTurn));

        public List<StonePattern> GetStonePatternForVertical(int[,] boardMatrix, Boolean isForBlackstone)
        {

            int numberOfConsecutiveStone = 0;
            BlockType blockType = BlockType.BothSideBlock;
            int targetcellValue = isForBlackstone
    ? blackStoneCellvalue
    : whiteStoneCellvalue;
            List<StonePattern> listPattern = new List<StonePattern>();
            for (int indexColumn = 0; indexColumn < boardMatrix.GetLength(1); indexColumn++)
            {
                for (int indexRow = 0; indexRow < boardMatrix.GetLength(0); indexRow++)
                {
                    var currentCellValue = boardMatrix[indexRow, indexColumn];
                    if (currentCellValue == targetcellValue)
                    {
                        numberOfConsecutiveStone++;
                        continue;
                    }
                    if (currentCellValue == emptyCellvalue)
                    {
                        if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                        {
                            blockType--;                            
                            listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));
                            numberOfConsecutiveStone = 0;
                            blockType = BlockType.OneSideBlock;
                        }
                        else
                        {
                            blockType = BlockType.OneSideBlock;
                        }
                        continue;
                    }
                    if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                    {
                        listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));                        
                        numberOfConsecutiveStone = 0;
                        blockType = BlockType.BothSideBlock;
                        continue;
                    }

                    blockType = BlockType.BothSideBlock;

                }
                if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                {
                    listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));

                }
                numberOfConsecutiveStone = 0;
                blockType = BlockType.BothSideBlock;

            }
            return listPattern;
        }
        private const int emptyCellvalue = 0;
        private const int blackStoneCellvalue = -1;
        private const int whiteStoneCellvalue = 1;



        public List<StonePattern> GetStonePatternForDiagonal(int[,] boardMatrix, Boolean isForBlackStone)
        {
            List<StonePattern> listPattern = new List<StonePattern>();
            int numberOfConsecutiveStone = 0;
            BlockType blockType = BlockType.BothSideBlock;

            int targetcellValue = isForBlackStone
? blackStoneCellvalue
: whiteStoneCellvalue;
            int opponentcellValue = isForBlackStone
                ? whiteStoneCellvalue
                : blackStoneCellvalue;

            // From bottom-left to top-right diagonally
            // https://stackoverflow.com/questions/20420065/loop-diagonally-through-two-dimensional-array
            int lastIndex = 2 * (boardMatrix.GetLength(0) - 1);
            for (int indexSWtoNE = 0; indexSWtoNE <= lastIndex; indexSWtoNE++)
            {
                int iStart = Math.Max(0, indexSWtoNE - boardMatrix.GetLength(0) + 1);
                int iEnd = Math.Min(boardMatrix.GetLength(0) - 1, indexSWtoNE);
                for (int i = iStart; i <= iEnd; ++i)
                {
                    int j = indexSWtoNE - i;

                    if (boardMatrix[i, j] == targetcellValue)
                    {
                        numberOfConsecutiveStone++;
                        continue;
                    }

                    if (boardMatrix[i, j] == emptyCellvalue)
                    {
                        if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                        {
                            blockType--;
                            listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));                            
                            numberOfConsecutiveStone = 0;
                            blockType =  BlockType.OneSideBlock;
                        }
                        else
                        {
                            blockType = BlockType.OneSideBlock;
                        }
                        continue;
                    }
                    if (boardMatrix[i, j] != opponentcellValue)
                    {
                        throw new Exception($@"The cell value in position {i},{j} is {boardMatrix[i, j]} which is invalid, the valid value must be {blackStoneCellvalue} For blackStone, {whiteStoneCellvalue} for whiteStone, {blackStoneCellvalue} for blankcell");
                    }

                    if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                    {
                        listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));                        
                        numberOfConsecutiveStone = 0;
                        blockType =  BlockType.BothSideBlock;
                        continue;
                    }

                    blockType = BlockType.BothSideBlock;

                }

                if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                {
                    listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));
                    
                }
                numberOfConsecutiveStone = 0;
                blockType = BlockType.BothSideBlock; 
            }

            // From top-left to bottom-right diagonally
            lastIndex = boardMatrix.GetLength(0) - 1;
            blockType = BlockType.BothSideBlock;
            for (int indexNWtoSE = 1 - boardMatrix.GetLength(0); indexNWtoSE <= lastIndex; indexNWtoSE++)
            {
                int iStart = Math.Max(0, indexNWtoSE);
                int iEnd = Math.Min(boardMatrix.GetLength(0) + indexNWtoSE - 1, boardMatrix.GetLength(0) - 1);
                for (int i = iStart; i <= iEnd; ++i)
                {
                    int j = i - indexNWtoSE;

                    if (boardMatrix[i, j] == targetcellValue)
                    {
                        numberOfConsecutiveStone++;
                        continue;
                    }
                    if (boardMatrix[i, j] == emptyCellvalue)
                    {
                        if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                        {
                            blockType--;
                            listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));                            
                            numberOfConsecutiveStone = 0;
                            blockType = BlockType.OneSideBlock;
                        }
                        else
                        {
                            blockType = BlockType.OneSideBlock;
                        }
                        continue;
                    }
                    if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                    {
                        listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));                        
                        numberOfConsecutiveStone = 0;
                        blockType = BlockType.BothSideBlock ;
                        continue;
                    }

                    blockType = BlockType.BothSideBlock ;


                }
                if (numberOfConsecutiveStone >= leastStoneForConsecutive)
                {
                    listPattern.Add(new StonePattern(numberOfConsecutiveStone, blockType));

                }
                numberOfConsecutiveStone = 0;
                blockType = BlockType.BothSideBlock ;
            }
            return listPattern;
        }
        

        

        //These readonly field are public because test cases need to access them

        public static readonly int wonScore = 50000000;
        public static readonly int confirmWinScore = 1000000;
        public static readonly int conSecutiveStone4_Not_MyTurn_0Block = 200000;
        public static readonly int conSecutiveStone4_Not_MyTurn_MoreThan0Block = 300;

        public static readonly int conSecutiveStone3_MyTurn_0Block = 50000;
        public static readonly int conSecutiveStone3_MyTurn_MoreThan0Block = 20;

        public static readonly int conSecutiveStone3_Not_MyTurn_0Block = 100;
        public static readonly int conSecutiveStone3_Not_MyTurn_MoreThan0Block = 5;

        public static readonly int conSecutiveStone2_MyTurn_0Block = 7;
        public static readonly int conSecutiveStone2_MoreThan0Block = 3;
        public static readonly int conSecutiveStone2_Not_MyTurn_0Block = 5;
        public static readonly int conSecutiveStone1 = 1;
        public int calculateConsecutiveStoneSequenceScore(StonePattern stonePattern, Boolean isMyTurn)
        {
            return calculateConsecutiveStoneSequenceScore(stonePattern.NumberOfConsecutiveStone,
                stonePattern.blockType,
                isMyTurn);
        }
     
        public int calculateConsecutiveStoneSequenceScore(int numberOfConsecutiveStone, BlockType blockType, Boolean isMyTurn)
        {
            
            if (numberOfConsecutiveStone >= 5)
            {
                return wonScore;
            }

            if (blockType== BlockType.BothSideBlock  
                && numberOfConsecutiveStone < 5)
            {
                return 0;
            }



            switch (numberOfConsecutiveStone)
            {
                case 4:
                    {
                        
                        if (isMyTurn)
                        {
                            return confirmWinScore;
                        }
                        else
                        {
                            
                            if (blockType == BlockType.ZeroBlock)
                            {
                                return conSecutiveStone4_Not_MyTurn_0Block;
                            }
                            else
                            {
                                return conSecutiveStone4_Not_MyTurn_MoreThan0Block;
                            }
                        }
                    }
                case 3:
                    {
                        
                        if (blockType ==  BlockType.ZeroBlock)
                        {

                            if (isMyTurn)
                            {
                                return conSecutiveStone3_MyTurn_0Block;
                            }
                            else
                            {
                                return conSecutiveStone3_Not_MyTurn_0Block;
                            }
                        }
                        else
                        {
                            if (isMyTurn)
                            {
                                return conSecutiveStone3_MyTurn_MoreThan0Block;
                            }
                            else
                            {
                                return conSecutiveStone3_Not_MyTurn_MoreThan0Block;
                            }
                        }
                    }
                case 2:
                    {

                        if (blockType  >  BlockType.ZeroBlock)
                        {
                            return conSecutiveStone2_MoreThan0Block;
                        }
                        if (isMyTurn)
                        {
                            return conSecutiveStone2_MyTurn_0Block;
                        }
                        else
                        {
                            return conSecutiveStone2_Not_MyTurn_0Block;
                        }

                    }
                case 1:
                    {
                        return conSecutiveStone1;
                    }
            }

           
            return 0;

        }

    }
}
