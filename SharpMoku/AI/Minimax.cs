using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.AI
{
    public class Minimax
    {

        private SharpMoku.Board board;
        public ILog log = null;
        public Minimax(SharpMoku.Board board, IEvaluate pEvaluater, ILog log)
        {
            this.evaluater = (pEvaluater != null)
                ? pEvaluater
                : new EvaluateV3(); //Default evaluate function

            this.board = board;
            this.log = log;
        }
        private void Log(String message)
        {
            if (this.log == null)
            {
                return;
            }
            this.log.Log("MiniMaxNew::" + message);

        }





        public int NumberOfNodes = 0;

        int evaluationCount = 0;


        private const int CONST_winScore = 10000000;
        public Position calculateNextMove(int depth)
        {
            int minAcceptDepth = 1;
            int maxAcceptDepth = 6;
            if (depth < minAcceptDepth || depth > maxAcceptDepth)
            {
                String message = $"depth value is {depth} which is invalid, program support the depth value between {minAcceptDepth} and {maxAcceptDepth}";
                throw new ArgumentOutOfRangeException(message);
            }

            MoveScore botMoveScore = new MoveScore();

            if (board.IsEmpty)
            {
                botMoveScore.Row = Utility.Randomizer.GetRandomNumber(0, board.Matrix.GetUpperBound(0));
                botMoveScore.Col = Utility.Randomizer.GetRandomNumber(0, board.Matrix.GetUpperBound(1));
                //bestMove.Row =
                evaluationCount = 0;
                return botMoveScore.GetPosition();
            }

            MoveScore botWinningPosition = new MoveScore();

            botWinningPosition = searchBotWinningPosition(board, evaluater);

            if (botWinningPosition.Row != -1)
            {
                evaluationCount = 0;
                return botWinningPosition.GetPosition();
            }


            MoveScore OpponenetWinningPosition = new MoveScore();
            OpponenetWinningPosition = searchOpponentWinningPosition(board, evaluater);

            if (OpponenetWinningPosition.Row != -1)
            {
                evaluationCount = 0;
                return OpponenetWinningPosition.GetPosition();

            }


            // If there is no such move, search the minimax tree with specified depth.
            //  Boolean IsMax = true;
            //  double Alpha = -1.0;
            NumberOfNodes = 0;
            miniMaxParameter Para = new miniMaxParameter();
            Para.Depth = depth;
            Para.board = board.Clone();
            Para.IsMax = true;
            Para.Alpha = -1.0;
            Para.Beta = CONST_winScore;
            Log("Before search");
            NumberOfNodeInEachLevel = new List<int>();
            int i;
            for (i = 0; i <= depth; i++)
            {
                NumberOfNodeInEachLevel.Add(0);
            }

            FirstLevelDepth = Para.Depth;
            botMoveScore = minimaxSearchAlphaBeta(Para.Depth, Para.board, Para.IsMax, Para.Alpha, Para.Beta);

            Log("  Depth::" + Para.Depth);
            Log("  Total No of Nodes::" + NumberOfNodes);

            if (botMoveScore.Row == -1 ||
                botMoveScore.Col == -1)
            {
                Log(" Cannot find any good postion using minimaxSerchAB");
                Log(" So we decide to use any position");

            }
            for (i = 0; i <= depth; i++)
            {
                Log("    Nodes[" + i + "] :" + NumberOfNodeInEachLevel[i]);
            }

            Log($"Score::{botMoveScore.Score}");
            Log($"Postion:: {botMoveScore.GetPosition().PositionString()}");
            return botMoveScore.GetPosition();
        }
        private int FirstLevelDepth = -1;



        /*
         * alpha : Best AI Move (Max)
         * beta : Best Player Move (Min)
         * returns: {score, move[0], move[1]}
         * */
        public class miniMaxParameter
        {
            public int Depth { get; set; }
            public SharpMoku.Board board { get; set; }
            public Boolean IsMax { get; set; }
            public double Alpha = 0;
            public double Beta = 0;
            public miniMaxParameter Clone()
            {
                miniMaxParameter CloneObject = new miniMaxParameter();
                CloneObject.Depth = this.Depth;
                CloneObject.board = this.board.Clone();
                CloneObject.IsMax = this.IsMax;
                CloneObject.Alpha = this.Alpha;
                CloneObject.Beta = this.Beta;
                return CloneObject;
            }
        }

        IEvaluate evaluater = null;
        private List<int> NumberOfNodeInEachLevel = null;
        private string GetTab(int moveDepth)
        {
            int maxDepth = 5;
            int numberofTab = maxDepth - moveDepth;
            StringBuilder strB = new StringBuilder();
            int i;
            for (i = 0; i < numberofTab; i++)
            {
                strB.Append("\t");
            }
            return strB.ToString();
        }
        private MoveScore minimaxSearchAlphaBeta(int depth, SharpMoku.Board board, Boolean IsMax, double AlphaValue, double BetaValue)
        {
            NumberOfNodes++;
            NumberOfNodeInEachLevel[depth]++;
            // Last depth (terminal node), evaluate the current board score.
            String tabString = GetTab(depth);
            MoveScore movescore = new MoveScore();
            Log($"{tabString}depth{depth}");

            if (depth == 0)
            {

                movescore = new MoveScore(evaluater.evaluateBoard(board, !IsMax));
                Log($"{tabString}Evaluate happens here");
                Log($"{tabString}Score::{movescore.Score}");

                return movescore;
            }

            /*If it is first level, the radiusNeighbor can be 2
             * because it will not have too much node.
            */
            int radiusNeighbour = (depth == FirstLevelDepth)
                ? 2
                : 1;
            List<Position> allNeighborPossibleMoves = null;
            if (radiusNeighbour == 2)
            {
                allNeighborPossibleMoves = board.generateNeighboreMoves(radiusNeighbour);
                if (allNeighborPossibleMoves.Count > 30)
                {
                    allNeighborPossibleMoves = board.generateNeighboreMoves(1);
                }
            }
            else
            {
                allNeighborPossibleMoves = board.generateNeighboreMoves(1);
            }


            // If there is no possible move left, treat this node as a terminal node and return the score.
            bool IsNothingLeftToSearch = (allNeighborPossibleMoves.Count == 0);

            if (IsNothingLeftToSearch)
            {
                movescore = new MoveScore(evaluater.evaluateBoard(board, !IsMax));
                return movescore;
            }

            /*If we reach this stage it means
             * There are valid moves
             */

            MoveScore bestMove = new MoveScore();
            int depthChild = 0;
            Boolean isMaxChild = false;
            depthChild = depth - 1;
            isMaxChild = !IsMax;

            bestMove.Row = allNeighborPossibleMoves[0].Row;
            bestMove.Col = allNeighborPossibleMoves[0].Col;
            bestMove.Score = IsMax
                            ? int.MinValue
                            : int.MaxValue;
            int iCountMove = 0;
            Log($"{tabString}No of neighbor::{allNeighborPossibleMoves.Count }");
            foreach (Position move in allNeighborPossibleMoves)
            {

                iCountMove++;
                Log($"{tabString}{iCountMove}.   move::{move.PositionString()}");
                board.PutStoneAndSwitchTurn(move);
                movescore = minimaxSearchAlphaBeta(depthChild, board, isMaxChild, AlphaValue, BetaValue);
                movescore.Row = move.Row;
                movescore.Col = move.Col;

                Log($"{tabString}Score::{movescore.Score }");
                //  board.Undo();

                if (board.IsFull)
                {
                    Log("{tabString}board.IsFull");
                    return movescore;

                }
                board.Undo();

                if (IsMax)
                {
                    AlphaValue = Math.Max(movescore.Score, AlphaValue);
                    if (movescore.Score >= BetaValue)
                    {
                        Log($"{tabString}moveScoe >= Beta");
                        return movescore;
                    }
                    bestMove = MoveScore.Max(bestMove, movescore);

                }
                else
                {
                    BetaValue = Math.Min(movescore.Score, BetaValue);
                    if (movescore.Score > AlphaValue)
                    {
                        Log($"{tabString}moveScore > Alpha");
                        return movescore;
                    }
                    bestMove = MoveScore.Min(bestMove, movescore);

                }
            }
            return bestMove;

        }

        public MoveScore searchBotWinningPosition(SharpMoku.Board board, IEvaluate bot)
        {

            List<Position> allPossibleMoves = board.generateNeighboreMoves();
            MoveScore winningPosition = new MoveScore();
            winningPosition.Score = -1;
            winningPosition.Row = -1;
            winningPosition.Col = -1;

            MoveScore maxMoveScore = new MoveScore(int.MinValue, -1, -1);

            foreach (Position move in allPossibleMoves)
            {
                evaluationCount++;
                SharpMoku.Board tempBoard = new SharpMoku.Board(board);

                tempBoard.PutStone(move);
                int Score = bot.getScore(tempBoard);

                if (Score > maxMoveScore.Score)
                {

                    maxMoveScore = new MoveScore(Score, move.Row, move.Col);
                }

            }

            if (maxMoveScore.Score >= CONST_winScore)
            {
                return maxMoveScore;

            }
            return winningPosition;
        }

        public MoveScore searchOpponentWinningPosition(SharpMoku.Board board, IEvaluate bot)
        {


            List<Position> allPossibleMoves = board.generateNeighboreMoves();
            MoveScore winningPosition = new MoveScore();
            winningPosition.Score = -1;
            winningPosition.Row = -1;
            winningPosition.Col = -1;

            MoveScore maxMoveScore = new MoveScore(int.MinValue, -1, -1);

            foreach (Position move in allPossibleMoves)
            {
                evaluationCount++;

                SharpMoku.Board tempBoard = new SharpMoku.Board(board);

                tempBoard.SwitchTurn();
                tempBoard.PutStone(move);
                int Score = bot.getScore(tempBoard);

                if (Score > maxMoveScore.Score)
                {
                    maxMoveScore = new MoveScore(Score, move.Row, move.Col);
                }

            }

            if (maxMoveScore.Score >= 10000)
            {
                return maxMoveScore;

            }
            return winningPosition;
        }


    }
}
