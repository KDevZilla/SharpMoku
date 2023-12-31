using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.AI
{
    //Credit:https://codepen.io/mudrenok/pen/gpMXgg
    /* This EvaluateV3 algorhim was ported from the javascript by Anton Mudrenok 
     * https://codepen.io/mudrenok
     */
    public class EvaluateV3:IEvaluate
    {
        public class NumberofScorePattern
        {
            public int Winning = 0;
            public int Stone4 = 0;
            public int Stone3 = 0;
            public int Stone2 = 0;
            public int BlockStone4 = 0;
            public int BlockStone3 = 0;
            public NumberofScorePattern()
            {

            }
            public NumberofScorePattern(int winning,
                int stone4,
                int stone3,
                int stone2,
                int blockStone4,
                int blockStone3)
            {
                this.Winning = winning;
                this.Stone4 = stone4;
                this.Stone3 = stone3;
                this.Stone2 = stone2;
                this.BlockStone4 = blockStone4;
                this.BlockStone3 = blockStone3;

            }
            public bool HasZeroValue
            {
                get
                {
                    return this.Winning == 0 &&
                        this.Stone4 == 0 &&
                        this.Stone3 == 0 &&
                        this.Stone2 == 0 &&
                        this.BlockStone4 == 0 &&
                        this.BlockStone3 == 0;
                }
            }
        }
        // This variable is used to track the number of evaluations for benchmarking purposes.
        public static int evaluationCount = 0;
        // Board instance is responsible for board mechanics
      
        // Win score should be greater than all possible board scores
        public const int CONST_winScore = 1000000000;
        public const int CONST_winGuarantee = CONST_winScore / 10;


        public class ListStonePattern
        {
            public static ListStonePattern Create(bool isForBlack)
            {
                ListStonePattern list = new ListStonePattern();
                if (!isForBlack)
                {
                    return list;
                }
                list.Stone2WithNoBLock = convertListValue(list.Stone2WithNoBLock);
                list.Stone3WithBlock = convertListValue(list.Stone3WithBlock);
                list.Stone3WithNoBlock = convertListValue(list.Stone3WithNoBlock);
                list.Stone4WithBlock = convertListValue(list.Stone4WithBlock);
                list.Stone4WithNoBlock = convertListValue(list.Stone4WithNoBlock);
                list.winPattern = convertListValue(list.winPattern);

                return list;
            }
            private static List<List<int>> convertListValue(List<List<int>> listInput)
            {
                int i;
                int j;
                for (i = 0; i < listInput.Count; i++)
                {
                    for(j=0;j<listInput[i].Count; j++)
                    {
                        listInput[i][j] = -listInput[i][j];
                    }
                }
                return listInput;
            }
           public List<List<int>> winPattern = new List<List<int>> { new List<int>() { 1, 1, 1, 1, 1 } };
           public List<List<int>> Stone4WithNoBlock = new List<List<int>> { new List<int>() { 0, 1, 1, 1, 1, 0 } };
           public List<List<int>> Stone3WithNoBlock = new List<List<int>>{
                        new List<int>(){ 0, 1, 1, 1, 0, 0 },
                        new List<int>(){ 0, 0, 1, 1, 1, 0 },
                        new List<int>(){ 0, 1, 0, 1, 1, 0 },
                        new List<int>(){ 0, 1, 1, 0, 1, 0 },

                    };
            public List<List<int>> Stone2WithNoBLock = new List<List<int>> {
                        new List<int>(){ 0, 0, 1, 1, 0, 0 },
                        new List<int>(){ 0, 1, 0, 1, 0, 0 },
                        new List<int>(){ 0, 0, 1, 0, 1, 0 },
                        new List<int>(){ 0, 1, 1, 0, 0, 0 },
                        new List<int>(){ 0, 0, 0, 1, 1, 0 },
                        new List<int>(){ 0, 1, 0, 0, 1, 0 },
                    };

            public List<List<int>> Stone4WithBlock = new List<List<int>> {
                        new List<int>(){-1, 1, 0, 1, 1, 1},
                        new List<int>(){-1, 1, 1, 0, 1, 1},
                        new List<int>(){-1, 1, 1, 1, 0, 1},
                        new List<int>(){-1, 1, 1, 1, 1, 0},
                        new List<int>(){0, 1, 1, 1, 1, -1},
                        new List<int>(){1, 0, 1, 1, 1, -1},
                        new List<int>(){1, 1, 0, 1, 1, -1},
                        new List<int>(){1, 1, 1, 0, 1, -1},

                    };

            public List<List<int>> Stone3WithBlock = new List<List<int>> {
                         new List<int>(){-1, 1, 1, 1, 0, 0},
                         new List<int>(){-1, 1, 1, 0, 1, 0},
                         new List<int>(){-1, 1, 0, 1, 1, 0},
                         new List<int>(){0, 0, 1, 1, 1, -1},
                         new List<int>(){0, 1, 0, 1, 1, -1},
                         new List<int>(){0, 1, 1, 0, 1, -1},
                         new List<int>(){-1, 1, 0, 1, 0, 1, -1},
                         new List<int>(){-1, 0, 1, 1, 1, 0, -1},
                         new List<int>(){-1, 1, 1, 0, 0, 1, -1},
                         new List<int>(){-1, 1, 0, 0, 1, 1, -1}
                    };
        }
        private ListStonePattern listStonePatternforWhite { get; } = ListStonePattern.Create(false);
        private ListStonePattern listStonePatternforBlack { get; } = ListStonePattern.Create(true);

        public NumberofScorePattern valuePosition(List<List<int>> listOfDirection, bool isForBlack)
        {

            NumberofScorePattern scorePattern = new NumberofScorePattern();
            int i;
            ListStonePattern listPattern = isForBlack
                ? listStonePatternforBlack
                : listStonePatternforWhite;

            for (i = 0; i < listOfDirection.Count; i++)
            {
                var list = listOfDirection[i];
                if (IsAnyInArrays(listPattern.winPattern, list))
                {
                    scorePattern.Winning++;
                    continue;
                }
                if (IsAnyInArrays(listPattern.Stone4WithNoBlock, list))
                {
                    scorePattern.Stone4++;
                    continue;
                }
                if (IsAnyInArrays(listPattern.Stone3WithNoBlock, list))
                {
                    scorePattern.Stone3++;
                    continue;
                }

                if (IsAnyInArrays(listPattern.Stone4WithBlock, list))
                {
                    scorePattern.BlockStone4++;
                    continue;
                }
                if (IsAnyInArrays(listPattern.Stone3WithBlock, list))
                {
                    scorePattern.BlockStone3++;
                    continue;
                }
                if (IsAnyInArrays(listPattern.Stone2WithNoBLock, list))
                {
                    scorePattern.Stone2++;
                    continue;
                }



            }
            return scorePattern;
        }
        public bool IsAnyInArrays(List<List<int>> listPattern, List<int> listCellValue)
        {

            int z;
            int j;
            int i;
            for (z = 0; z < listPattern.Count; z++)
            {
                //
                int fCount = listCellValue.Count;
                int sCount = listPattern[z].Count;
                int k;
                for (i = 0; i <= fCount - sCount; i++)
                {
                    k = 0;
                    for (j = 0; j < sCount; j++)
                    {
                        if (listCellValue[i + j] == listPattern[z][j])
                        {
                            k++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (k == sCount)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public double evaluateBoardForWhite(SharpMoku.Board board, Boolean blacksTurn)
        {
            evaluationCount++;

            // Get board score of both players.

           // double blackScore = getScore(board, true, blacksTurn);
            double whiteScore = getScore(board);
            return whiteScore;
            /*
            if (blackScore == 0) blackScore = 1.0;

            // Calculate relative score of black against white
            if (blacksTurn)
            {
                return blackScore / whiteScore;
            }
            // Calculate relative score of white against black
            return whiteScore / blackScore;
            */
        }
        public List<List<int>> GetListAllDirection(SharpMoku.Board board, Position checkPosition, SharpMoku.Board.CellValue cellValue)
        {
            Position positionDeltaNorthSouth = new Position(1, 0);
            Position positionDeltaWestEast = new Position(0, 1);
            Position positionDeltaNorthEast = new Position(1, 1);
            Position positionDeltaSouthWest = new Position(1, -1);

            /*
                   *Prepare to go though all 8 directions
                   * 4 have 4 lists of News because each list go both way
                   * For example NorthSouth mean from the position to north and from the postion to south
                   */
            List<int> listNorthSouth = GetCellValueInDirection(board.Matrix, cellValue, checkPosition, positionDeltaNorthSouth);
            List<int> listWestEast = GetCellValueInDirection(board.Matrix, cellValue, checkPosition, positionDeltaWestEast);
            List<int> listNorthEast = GetCellValueInDirection(board.Matrix, cellValue, checkPosition, positionDeltaNorthEast);
            List<int> listSouthWest = GetCellValueInDirection(board.Matrix, cellValue, checkPosition, positionDeltaSouthWest);

            List<List<int>> listAllDirection = new List<List<int>>()
            {
                listNorthSouth ,
                listWestEast ,
                listNorthEast ,
                listSouthWest
            };

            return listAllDirection;
        }


        //GetDifferenctScorefromLastPosition
        public int heuristic(SharpMoku.Board newBoard)
        {
            // GetPlayerscore from latest position
            Position LatestPosition = newBoard.LastPositionPut;

            SharpMoku.Board.CellValue PlayercellValue = (SharpMoku.Board.CellValue)newBoard.Matrix[LatestPosition.Row , LatestPosition.Col];
            List<List<int>> listAreaCheckForPlayer = GetListAllDirection(newBoard, LatestPosition, PlayercellValue);

            bool isForBlack = (PlayercellValue == SharpMoku.Board.CellValue.Black);

            NumberofScorePattern scorePattern = valuePosition(listAreaCheckForPlayer,isForBlack);
            var Player1Value = getScoreByPattern(scorePattern);


            // Put the opponenetcell value into the latest postion.
            // Then get opponenet score
            // to see the differnent
            SharpMoku.Board.CellValue opponentCellValue = (SharpMoku.Board.CellValue)
                                              (-(int)PlayercellValue);
            newBoard.Matrix[LatestPosition.Row, LatestPosition.Col] =(int) opponentCellValue;

            List<List<int>> listAreaCheckForOpponent = GetListAllDirection(newBoard, LatestPosition, opponentCellValue);

            isForBlack = (opponentCellValue == SharpMoku.Board.CellValue.Black);
            NumberofScorePattern enemyScorePattern = valuePosition(listAreaCheckForOpponent,isForBlack);
            var EnemyValue = getScoreByPattern(enemyScorePattern);

            //After get Score for opponenet
            //Set it back to player cell value
            newBoard.Matrix[LatestPosition.Row, LatestPosition.Col] =(int)PlayercellValue;



            return 2 * Player1Value + EnemyValue;
            //return 0;
        }


        public List<int> GetCellValueInDirection(int[,] matrix, SharpMoku.Board.CellValue cellValue, Position positionCheck, Position positionDelta)
        {
            int i;
            List<int> listCell = new List<int>();
            bool IsCheckPostionIsNotmatchWithCellValue = matrix[positionCheck.Row, positionCheck.Col] != (int)cellValue;
            HashSet<String> hshCellInaRow = new HashSet<string>();
            if (IsCheckPostionIsNotmatchWithCellValue)
            {
                return listCell;
            }
            int opponentCellvalue = -(int)cellValue;
            //First loop Insert cell
            for (i = 1; i < 5; i++)
            {
                Position nextPosition = new Position(positionCheck.Row - positionDelta.Row * i,
                                                    positionCheck.Col - positionDelta.Col * i);
                if (nextPosition.Row < 0 ||
                    nextPosition.Row >= matrix.GetLength(0) ||
                    nextPosition.Col < 0 ||
                    nextPosition.Col >= matrix.GetLength(0))
                {
                    break;
                }

                var nextValue = matrix[nextPosition.Row, nextPosition.Col];
                if(!hshCellInaRow.Contains ( nextPosition.PositionString()))
                {
                    listCell.Insert(0, nextValue); //We insert at the 0 position
                }
                
                if ((int)nextValue == opponentCellvalue)
                {

                    break;
                }

            }
            listCell.Add((int)cellValue); //The cell itself

            //Add
            for (i = 1; i < 5; i++)
            {
                Position nextPosition = new Position(positionCheck.Row + positionDelta.Row * i,
                                                    positionCheck.Col + positionDelta.Col * i);
                if (nextPosition.Row < 0 ||
                    nextPosition.Row >= matrix.GetLength(0) ||
                    nextPosition.Col < 0 ||
                    nextPosition.Col >= matrix.GetLength(0))
                {
                    break;
                }
                var nextValue = matrix[nextPosition.Row, nextPosition.Col];

                if (!hshCellInaRow.Contains(nextPosition.PositionString()))
                {
                    listCell.Add(nextValue);//We add it to the last position
                }
                if ((int)nextValue == opponentCellvalue)
                {
                   // listCell.Insert(0, nextValue);
                    break;
                }
                //listCell.Insert(0, nextValue);
            }
            return listCell;
        }
        public int getScore(SharpMoku.Board board)
        {

            NumberofScorePattern numPattern = new NumberofScorePattern();

            int Score = heuristic(board);
            return Score;
           
        }
        public int getScoreByPattern(NumberofScorePattern numberofPattern)
        {
            if (numberofPattern.Winning > 0)
            {
                return CONST_winScore * numberofPattern.Winning;
            }
            if (numberofPattern.Stone4 > 0)
            {
                return CONST_winGuarantee;

            }

            if (numberofPattern.BlockStone4 > 1)
            {
                return CONST_winGuarantee / 10;
            }
            if (numberofPattern.Stone3 > 0
                && numberofPattern.BlockStone4 > 0)
            {
                return CONST_winGuarantee / 100;
            }
            if (numberofPattern.Stone3 > 1)
            {
                return CONST_winGuarantee / 1000;
            }

            if (numberofPattern.Stone3 == 1)
            {
                switch (numberofPattern.Stone2)
                {
                    case 3: return 40000;
                    case 2: return 38000;
                    case 1: return 35000;
                    default: return 3450;
                }
            }

            if (numberofPattern.BlockStone4 == 1)
            {
                switch (numberofPattern.Stone2)
                {
                    case 3: return 4500;
                    case 2: return 4200;
                    case 1: return 4100;
                    default: return 4050;
                }
            }


            switch (numberofPattern.BlockStone3)
            {
                case 3:
                    if (numberofPattern.Stone2 == 1) return 2800;
                    break;
                case 2:
                    switch (numberofPattern.Stone2)
                    {
                        case 2: return 3000;
                        case 1: return 2900;

                    }
                    break;
                case 1:
                    switch (numberofPattern.Stone2)
                    {
                        case 3: return 3400;
                        case 2: return 3300;
                        case 1: return 3100;

                    }
                    break;
            }

            switch (numberofPattern.Stone2)
            {
                case 4: return 2700;
                case 3: return 2500;
                case 2: return 2000;
                case 1: return 1000;

            }
            return 0;
        }


       

        public double evaluateBoard(SharpMoku.Board board, bool IsMyturn)
        {
            return evaluateBoardForWhite(board, IsMyturn);
        }
      

    }
}
