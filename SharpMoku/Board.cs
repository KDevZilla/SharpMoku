using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku
{
    [Serializable]
    public class Board
    {
        public class PositionEventArgs : EventArgs
        {
            public Position Value { get; set; }
            public PositionEventArgs(Position position)
            {
                Value = position;
            }

        }
        public delegate void CellClickHandler(object sender, PositionEventArgs positionClick);

        // 2d array to store cell value
        public int[,] Matrix;

        /*
         * dicWhiteStone stores WhiteStone position
         * dicBlackStone stores BlackStone position
         * dicNieghbor stores position of the stone next to both white and black stone
         * 
         * These 3 dictionaries are used by Minimax evaluate function.
         */
        public Dictionary<String, SharpMoku.Position> dicWhiteStone { get; private set; } = new Dictionary<String, SharpMoku.Position>();
        public Dictionary<String, SharpMoku.Position> dicBlackStone { get; private set; } = new Dictionary<String, SharpMoku.Position>();
        public Dictionary<String, SharpMoku.Position> dicNeighbor { get; private set; } = new Dictionary<string, Position>();

        public int BoardSize { get; private set; }

        public enum WinStatus
        {
            BlackWon = -1,
            NotDecidedYet = 0,
            WhiteWon = 1,
            Draw = 2
        }

        // Represent the value of the cell in the board
        public enum CellValue
        {
            Black = -1,
            Empty = 0,
            White = 1,
        }

        public enum Turn
        {
            Black = -1,
            White = 1
        }
        public Turn CurrentTurn { get; private set; } = Turn.Black;
        public CellValue CurrentTurnCellValue
        {
            get
            {
                if (CurrentTurn == Turn.Black)
                {
                    return CellValue.Black;
                }

                return CellValue.White;
            }
        }
        public Board(int boardSize)
        {
            if (boardSize != 9 &&
                boardSize != 15)
            {
                throw new ArgumentException($"Board size is invalid {boardSize}, program only accept 9 and 15 as valid value");
            }
            this.BoardSize = boardSize;
            Matrix = new int[this.BoardSize, this.BoardSize];

        }

        public Board(Board board)
        {

            Matrix = new int[board.Matrix.GetLength(0), board.Matrix.GetLength(1)];
            dicWhiteStone = new Dictionary<string, Position>();
            dicBlackStone = new Dictionary<string, Position>();
            dicNeighbor = new Dictionary<string, Position>();

            this.Matrix = board.Matrix.Clone() as int[,];
            this.dicWhiteStone = new Dictionary<string, Position>(board.dicWhiteStone);
            this.dicBlackStone = new Dictionary<string, Position>(board.dicBlackStone);
            this.dicNeighbor = new Dictionary<string, Position>(board.dicNeighbor);
            this.listHistory = new List<Position>(board.listHistory);
            this.BoardSize = board.BoardSize;
            this.CurrentTurn = board.CurrentTurn;

        }
        public Board Clone() => new Board(this);

        public Boolean IsValidValue(int value)
        {
            if (value != (int)CellValue.Black &&
                value != (int)CellValue.White &&
                value != (int)CellValue.Empty)
            {
                return false;
            }

            return true;
        }
        private Dictionary<String, SharpMoku.Position> GetHshByCellValue(CellValue cellValue)
        {
            if (cellValue == CellValue.White)
            {
                return dicWhiteStone;
            }
            return dicBlackStone;
        }
        public bool IsThereAnyOneWon()
        {

            throw new Exception("Not implement");
        }

        public void PutStone(int pRow, int pCol)
        {

            CellValue cellValue = this.CurrentTurnCellValue;
            Matrix[pRow, pCol] = (int)cellValue;
            SharpMoku.Position newPosition = new Position(pRow, pCol);

            GetHshByCellValue(cellValue).Add(newPosition.PositionString(), newPosition);
            listHistory.Add(newPosition);
        }
        public List<Position> GetListNeighborPosition(Position position) => GetListNeighborPosition(position, 1);
        public List<Position> GetListNeighborPosition(Position position, int radius)
        {

            List<Position> listResult = new List<Position>();
            int i;
            int j;
            for (i = -radius; i <= radius; i++)
            {
                for (j = -radius; j <= radius; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    Position neighborPosition = new Position(i + position.Row, j + position.Col);
                    if (!IsValidPosition(neighborPosition))
                    {
                        continue;
                    }


                    listResult.Add(neighborPosition);
                }
            }
            return listResult;
        }
        private void AddEmptyNeighborOf(Position position)
        {

            if (dicNeighbor.ContainsKey(position.PositionString()))
            {
                dicNeighbor.Remove(position.PositionString());
            }

            List<Position> listNieghbor = GetListNeighborPosition(position);
            foreach (Position neighborPosition in listNieghbor)
            {


                if (dicNeighbor.ContainsKey(neighborPosition.PositionString()))
                {
                    continue;
                }

                if (Matrix[neighborPosition.Row, neighborPosition.Col] != (int)CellValue.Empty)
                {
                    continue;
                }
                dicNeighbor.Add(neighborPosition.PositionString(), neighborPosition);

            }
        }
        public void AdjustEmptyNeighborOf(Position position)
        {
            List<Position> listNieghbor = GetListNeighborPosition(position);
            HashSet<String> hshCannotRemove = new HashSet<string>();
            Boolean isNeedtoAddNeighborForPostion = false;
            foreach (Position neighborPosition in listNieghbor)
            {
                bool IsThisNeighborNotEmpty = false;
                if (Matrix[neighborPosition.Row, neighborPosition.Col] != (int)CellValue.Empty)
                {
                    IsThisNeighborNotEmpty = true;
                    isNeedtoAddNeighborForPostion = true;

                    if (!hshCannotRemove.Contains(neighborPosition.PositionString()))
                    {
                        hshCannotRemove.Add(neighborPosition.PositionString());
                    }

                }

                List<Position> listNighborOfNeighbor = GetListNeighborPosition(neighborPosition);

                foreach (Position neighborOfneighborPosition in listNighborOfNeighbor)
                {
                    if (IsThisNeighborNotEmpty)
                    {
                        if (!hshCannotRemove.Contains(neighborPosition.PositionString()))
                        {
                            hshCannotRemove.Add(neighborPosition.PositionString());
                        }
                        break;
                    }

                    if (Matrix[neighborOfneighborPosition.Row, neighborOfneighborPosition.Col] != (int)CellValue.Empty)
                    {
                        if (!hshCannotRemove.Contains(neighborPosition.PositionString()))
                        {
                            hshCannotRemove.Add(neighborPosition.PositionString());
                        }
                        break;
                    }
                }

            }
            foreach (Position neighborPosition in listNieghbor)
            {
                if (hshCannotRemove.Contains(neighborPosition.PositionString()))
                {
                    continue;
                }
                dicNeighbor.Remove(neighborPosition.PositionString());
            }
            if (isNeedtoAddNeighborForPostion)
            {
                dicNeighbor.Add(position.PositionString(), position);


            }


        }


        public void PutStone(int pRow, int pCol, CellValue cellValue)
        {
            /* 1.Assign value into the matrix
             * 2.Add the postion value into Hash
             * 3.Add postion into history
             * 4.Add Empty nex
             */
            Matrix[pRow, pCol] = (int)cellValue;
            SharpMoku.Position newPosition = new Position(pRow, pCol);
            GetHshByCellValue(cellValue).Add(newPosition.PositionString(), newPosition);
            listHistory.Add(newPosition);
            AddEmptyNeighborOf(newPosition);
        }

        public void PutStone(Position position) => PutStone(position.Row, position.Col, CurrentTurnCellValue);
        public void PutStone(Position position, CellValue cellValue) => PutStone(position.Row, position.Col, cellValue);
        public Boolean IsValidPosition(Position pos) => IsValidPosition(pos.Row, pos.Col);


        public Boolean IsValidPosition(int pRow, int pCol)
        {
            if (pRow < 0 || pRow >= BoardSize ||
                pCol < 0 || pCol >= BoardSize)
            {
                return false;
            }
            return true;
        }

        public void PutStone(int pRow, int pCol, int Value)
        {

            if (!IsValidPosition(pRow, pCol))
            {
                throw new ArgumentException($"Postion is not valid {pRow},{pCol} ");
            }
            if (!IsValidValue(Value))
            {
                throw new ArgumentException($"Value is not valid {Value}");
            }
            PutStone(pRow, pCol, (CellValue)Value);
        }

        public void PutStoneAndSwitchTurn(Position pos)
        {
            PutStone(pos.Row, pos.Col, CurrentTurnCellValue);
            SwitchTurn();
        }
        public void PutStoneAndSwitchTurn(Position pos, CellValue cellValue)
        {
            PutStone(pos.Row, pos.Col, cellValue);
            SwitchTurn();
        }
        public void PutStoneAndSwitchTurn(int pRow, int pCol, CellValue cellValue)
        {
            PutStone(pRow, pCol, cellValue);
            SwitchTurn();
        }
        public void SwitchTurn()
        {
            if (CurrentTurn == Turn.Black)
            {
                CurrentTurn = Turn.White;
                return;
            }

            CurrentTurn = Turn.Black;
        }
        public Boolean IsThere5inRow(Position pos)
        {
            int i;
            int j;
            int CellValue = this.Matrix[pos.Row, pos.Col];
            if (CellValue == 0)
            {
                return false;
            }

            for (i = -1; i <= 1; i++)
            {

                for (j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    int NoofRepeating = 0;

                    int SameCellValueInDirectionCount = 0;
                    for (NoofRepeating = 1; NoofRepeating <= 4; NoofRepeating++)
                    {
                        Position CheckPosition = new Position(pos.Row + (i * NoofRepeating),
                            pos.Col + (j * NoofRepeating));
                        if (CheckPosition.Row < 0
                            || CheckPosition.Row > BoardSize - 1
                            || CheckPosition.Col < 0
                            || CheckPosition.Col > BoardSize - 1)
                        {
                            break;
                        }

                        if (Matrix[CheckPosition.Row, CheckPosition.Col] != CellValue)
                        {
                            break;
                        }
                        SameCellValueInDirectionCount++;
                        if (SameCellValueInDirectionCount >= 4)
                        {
                            return true;
                        }
                    }

                    // Position posCheck


                }
            }
            return false;
        }
        public Boolean IsEmpty => dicBlackStone.Count + dicWhiteStone.Count == 0;
        public Boolean IsFull => dicBlackStone.Count + dicWhiteStone.Count == this.BoardSize * this.BoardSize;
        public WinStatus CheckWinStatus()
        {
            foreach (Position pos in dicWhiteStone.Values)
            {

                if (IsThere5inRow(pos))
                {
                    return WinStatus.WhiteWon;
                }
            }
            foreach (Position pos in dicBlackStone.Values)
            {
                if (IsThere5inRow(pos))
                {
                    return WinStatus.BlackWon;
                }
            }
            if (IsFull)
            {
                return WinStatus.Draw;
            }

            return WinStatus.NotDecidedYet;
        }

        public List<Position> listHistory = new List<Position>();
        public bool CanUndo => listHistory != null && listHistory.Count > 0;
        public Position LastPositionPut
        {
            get
            {
                if (listHistory == null || listHistory.Count == 0)
                {
                    return new Position(-1, -1);
                }
                return listHistory[listHistory.Count - 1];
            }
        }

        public List<Position> generateNeighboreMoves()
        {
            return generateNeighboreMoves(1);
        }

        public List<Position> generateNeighboreMoves(int radius)
        {
            List<Position> moveList = new List<Position>();
            bool IsUsedicNeighbor = (radius == 1);

            if (IsUsedicNeighbor)
            {
                return dicNeighbor.Values.ToList();
            }


            HashSet<String> hsh = new HashSet<string>();
            List<Position> listResult = new List<Position>();
            foreach (string key in dicBlackStone.Keys)
            {
                List<Position> list = GetListNeighborPosition(dicBlackStone[key], radius);
                foreach (Position pos in list)
                {
                    if (hsh.Contains(pos.PositionString()))
                    {
                        continue;
                    }
                    if (dicBlackStone.ContainsKey(pos.PositionString()))
                    {
                        continue;
                    }
                    if (dicWhiteStone.ContainsKey(pos.PositionString()))
                    {
                        continue;
                    }
                    hsh.Add(pos.PositionString());
                    listResult.Add(pos);
                }
            }

            foreach (string key in dicWhiteStone.Keys)
            {
                List<Position> list = GetListNeighborPosition(dicWhiteStone[key], radius);
                foreach (Position pos in list)
                {
                    if (hsh.Contains(pos.PositionString()))
                    {
                        continue;
                    }
                    if (dicBlackStone.ContainsKey(pos.PositionString()))
                    {
                        continue;
                    }
                    if (dicWhiteStone.ContainsKey(pos.PositionString()))
                    {
                        continue;
                    }
                    hsh.Add(pos.PositionString());
                    listResult.Add(pos);
                }
            }

            return listResult;



        }
        /*
        public List<Position> generateNeighboreMovesBK()
        {



            List<Position> moveList = new List<Position>();
            int boardSize =Matrix.GetLength(0);

            // Look for cells that has at least one stone in an adjacent cell.
            int iCount = 0;


            var listPosition = dicBlackStone.Values.ToList();
            listPosition.AddRange(dicWhiteStone.Values.ToList());


            NewGetMove(moveList, listPosition, this.Matrix, boardSize);

            iCount++;
            return moveList;

        }
        */
        /*
        private void NewGetMove(List<Position> moveList, List<SharpMoku.Position> listCheckPosition, int[,] pboarMatrix, int boardSize)
        {
            HashSet<String> hshPosition = new HashSet<String>();
            foreach (SharpMoku.Position checkPosition in listCheckPosition)
            {
                for (int Row = checkPosition.Row - 1; Row <= checkPosition.Row + 1; Row++)
                {
                    for (int Col = checkPosition.Col - 1; Col <= checkPosition.Col + 1; Col++)
                    {
                        Boolean IsOwnCell = (Row == checkPosition.Row && Col == checkPosition.Col);
                        Boolean IsInvalidRange = Row < 0 ||
                            Row >= boardSize ||
                            Col < 0 ||
                            Col >= boardSize;


                        if (IsOwnCell || IsInvalidRange)
                        {
                            continue;
                        }

                        Boolean IsNotEmpty = this.Matrix[Row, Col] != 0;

                        if (IsNotEmpty)
                        {
                            continue;
                        }
                        SharpMoku.Position newPosition = new SharpMoku.Position(Row, Col);
                        Boolean IsAlreadyExist = hshPosition.Contains(newPosition.PositionString());
                        if (IsAlreadyExist)
                        {
                            continue;
                        }

                        moveList.Add(newPosition);
                        hshPosition.Add(newPosition.PositionString());
                    }
                }
            }

        }
        */

        public void Undo()
        {
            int LastIndex = listHistory.Count - 1;
            Position pos = listHistory[LastIndex];
            listHistory.RemoveAt(LastIndex);
            RemoveStone(pos);
            AdjustEmptyNeighborOf(pos);
            this.SwitchTurn();

        }


        public void RemoveStone(Position position)
        {


            if (dicBlackStone.ContainsKey(position.PositionString()))
            {
                dicBlackStone.Remove(position.PositionString());
            }
            else
            {
                if (dicWhiteStone.ContainsKey(position.PositionString()))
                {
                    dicWhiteStone.Remove(position.PositionString());
                }
                else
                {
                    throw new Exception(String.Format("Postion is not correct {0},{1}", position.Row, position.Col));
                }

            }

            Matrix[position.Row, position.Col] = 0;

        }
        public void RemoveStone(int posX, int posY)
        {
            RemoveStone(new Position(posX, posY));
        }
    }
}
