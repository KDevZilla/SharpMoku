using SharpMoku.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku
{
    public class Game
    {
        public Board board = null;
        private UI.IUI UI = null;
        public enum GameStateEnum
        {
            NotBegin,
            Playing,
            End
        }
        public enum GameModeEnum
        {
            PlayerVsBot = 0,
            BotVsPlayer = 1,
            PlayerVsPlayer = 2,

        }
        public Board.WinStatus WinResult { get; private set; } = Board.WinStatus.NotDecidedYet;
        public Board.Turn TheWinner { get; private set; }
        public ILog log = null;
        public GameModeEnum GameMode { get; private set; } = GameModeEnum.PlayerVsBot;
        public GameStateEnum GameState { get; private set; } = GameStateEnum.NotBegin;
        /*
         * GameFinished event to tell the UI to display the result
         * BotThinking to tell the UI to change the cursor to hourglass
         * BotFinishedThinking to tell the UI that now UI is allowed to accept user input
         */
        public event EventHandler GameFinished;
        public event EventHandler BotThinking;
        public event EventHandler BotFinishedThinking;

        public class WinStatusEventArgs : EventArgs
        {
            public Board.WinStatus Winstatus { get; set; }
            public WinStatusEventArgs(Board.WinStatus winStatus)
            {
                this.Winstatus = winStatus;
            }
        }
        // For ObjectID is for helping to debug purpose only
        // We don't need it for other purpose
        // so I commented it out
        //public int ObjectID { get; set; } = 0;
        private void ExplicitConstructor(UI.IUI ui,
            Board board,
            int boardSize,
            IEvaluate pbot,
            int botSearchDepth,
            GameModeEnum gameMode)
        {
            this.UI = ui;
            this.GameMode = gameMode;
            this.BotSearchDepth = botSearchDepth;
            if (pbot != null)
            {
                bot = pbot;
            }
            WinResult = Board.WinStatus.NotDecidedYet;

            this.UI.CellClicked -= UI_CellClicked;
            this.UI.HasFinishedMoveCursor -= UI_HasFinishedMoveCursor;
            this.GameFinished -= UI.Game_GameFinished;
            this.BotThinking -= UI.Game_BotThinking;
            this.BotFinishedThinking -= UI.Game_BotFinishedThinking;


            this.UI.CellClicked += UI_CellClicked;
            this.UI.HasFinishedMoveCursor += UI_HasFinishedMoveCursor;

            this.board = (board != null)
                ? board
                : new Board(boardSize);


            //Cannnot use this event (Game_GameFinished)
            //Has the problem with threading
            this.GameFinished += UI.Game_GameFinished;
            this.BotThinking += UI.Game_BotThinking;
            this.BotFinishedThinking += UI.Game_BotFinishedThinking;

        }
        public Game(UI.IUI ui, Board board,
            IEvaluate pbot,
            int botSearchDepth,
            GameModeEnum gameMode
            )
        {

            ExplicitConstructor(ui, board, 0, pbot, botSearchDepth, gameMode);
            /*
            this.UI = ui;
            this.GameMode = gameMode;
            this.BotSearchDepth = botSearchDepth;
            if(pbot != null)
            {
                bot = pbot;
            }
            this.UI.CellClicked += UI_CellClicked;
            this.UI.HasFinishedMoveCursor += UI_HasFinishedMoveCursor;
            WinResult = Board.WinStatus.NotDecidedYet;
            this.board = new Board(board);
            */
        }
        public Game(UI.IUI ui, int boardSize,
            IEvaluate pbot,
            int botSearchDepth,
            GameModeEnum gameMode
            )
        {
            ExplicitConstructor(ui, null, boardSize, pbot, botSearchDepth, gameMode);

        }
        public bool CanUndo => board == null
            ? false
            : board.CanUndo;

        public void Undo()
        {

            if (this.GameMode == GameModeEnum.PlayerVsPlayer)
            {
                board.Undo();
                if (this.GameState == GameStateEnum.End)
                {
                    board.SwitchTurn();
                }
            }
            else
            {
                //If the gameMode is BotVsPlayer or PlayerVsBOT
                //We need to Undo 2 times
                //1 for bot another for player
                if (this.GameState == GameStateEnum.End)
                {
                    bool isneedToDoubleUndo = false;
                    if (this.GameMode == GameModeEnum.BotVsPlayer)
                    {
                        if (this.WinResult == Board.WinStatus.BlackWon)
                        {
                            isneedToDoubleUndo = true;

                        }
                    }
                    else
                    {
                        if (this.WinResult == Board.WinStatus.WhiteWon)
                        {
                            isneedToDoubleUndo = true;
                        }
                    }

                    if (isneedToDoubleUndo)
                    {
                        board.Undo();
                        board.Undo();
                        board.SwitchTurn();
                    }
                    else
                    {
                        board.Undo();
                        board.SwitchTurn();
                    }
                }
                else
                {
                    board.Undo();
                    board.Undo();
                }
            }
            this.WinResult = Board.WinStatus.NotDecidedYet;
            this.GameState = GameStateEnum.Playing;
            UI.RenderUI();

        }
        private void UI_HasFinishedMoveCursor(object sender, EventArgs e)
        {

            PutStone(botMoveToPostion, (Board.CellValue)board.CurrentTurn);

        }
        public void PutStone(Position position)
        {
            PutStone(position, this.board.CurrentTurnCellValue);
        }
        //public Boolean 
        public void PutStone(Position position, Board.CellValue turn)
        {

            board.PutStone(position, turn);

            this.UI.RenderUI();

            WinResult = board.CheckWinStatus();

            if (WinResult == Board.WinStatus.NotDecidedYet)
            {
                //DANGEROUS
                this.board.SwitchTurn();
                bool IsBotTurn = (GameMode == GameModeEnum.PlayerVsBot && !IsPlayer1Turn) ||
                                (GameMode == GameModeEnum.BotVsPlayer && IsPlayer1Turn);
                if (IsBotTurn)
                {
                    BotThinking?.Invoke(this, null);
                    BotMove();
                }
                return;
            }

            this.GameState = GameStateEnum.End;
            WinStatusEventArgs statusEvent = new WinStatusEventArgs(WinResult);
            GameFinished?.Invoke(this, statusEvent);


        }
        System.Timers.Timer botTimer = new System.Timers.Timer();

        public void NewGame()
        {
            this.GameState = GameStateEnum.Playing;

            if (this.GameMode == GameModeEnum.BotVsPlayer)
            {
                // botTimer.Start();
                System.Threading.Thread.Sleep(20);
                BotMove();

            }

        }

        private bool IsPlayer1Turn => board.CurrentTurn == Board.Turn.Black;

        // This method being used by human only.
        private void UI_CellClicked(object o, Board.PositionEventArgs positionClick)
        {
            Boolean isPlayerClickDespiteItisBotTurn = (this.GameMode == GameModeEnum.PlayerVsBot && this.board.CurrentTurn != Board.Turn.Black) ||
                                                      (this.GameMode == GameModeEnum.BotVsPlayer && this.board.CurrentTurn != Board.Turn.White);

            Boolean isClickedOnNonEmptyCell = board.Matrix[positionClick.Value.Row, positionClick.Value.Col] != (int)Board.CellValue.Empty;
            Boolean isClickedOInValidPosition = !board.IsValidPosition(positionClick.Value);

            if (GameState != GameStateEnum.Playing
                || isClickedOInValidPosition
                || isPlayerClickDespiteItisBotTurn
                || isClickedOnNonEmptyCell)
            {
                return;
            }

            /*
            if (!board.IsValidPosition(positionClick.Value))
            {
                return;
            }
               if (isPlayerClickDespiteItisBotTurn )
            {
                return;
            }

            if(board.Matrix[positionClick.Value.Row ,positionClick.Value.Col ] != (int)Board.CellValue.Empty)
            {
                return;
            }
            */

            PutStone(positionClick.Value, board.CurrentTurnCellValue);

        }

        public void ReleaseResource()
        {
            if (this.UI != null)
            {
                this.UI.CellClicked -= UI_CellClicked;
                this.UI.HasFinishedMoveCursor -= UI_HasFinishedMoveCursor;
                this.GameFinished -= UI.Game_GameFinished;
                this.BotThinking -= UI.Game_BotThinking;
                this.BotFinishedThinking -= UI.Game_BotFinishedThinking;
            }
            this.UI = null;
        }
        public int BotSearchDepth { get; private set; } = 2;
        private Position botMoveToPostion;
        private IEvaluate bot = new EvaluateV3();
        private void BotMove()
        {

            SharpMoku.Board cloneBoard = new Board(this.board);

            Minimax miniMax = new Minimax(cloneBoard, bot, this.log);

            botMoveToPostion = miniMax.calculateNextMove(BotSearchDepth);
            BotFinishedThinking?.Invoke(this, null);
            UI.MoveCursorTo(botMoveToPostion);

        }

    }
}
