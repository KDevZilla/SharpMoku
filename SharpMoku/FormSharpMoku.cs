using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpMoku.UI;

namespace SharpMoku
{
    public partial class FormSharpMoku : Form, IUI
    {
        /* This class imeplement IUI
         * It has UI.PictureBoxGoMoKu picGoMoku that responsible for rendering a board
         * It has game object that use to bind between this class, game object and the board information
         */
        public FormSharpMoku()
        {
            InitializeComponent();
        }

        UI.PictureBoxGoMoKu picGoMoku = null;
        Game game = null;
        //AI.IEvaluate bot = new AI.EvaluateV2();
        AI.IEvaluate bot = new AI.EvaluateV3();

        public event Board.CellClickHandler CellClicked;
        public event EventHandler HasFinishedMoveCursor;

        private void UpdateTheme(UI.ThemeSpace.Theme theme)
        {

            picGoMoku.Initial(theme);
            picGoMoku.ReRender();
            ScaleBoard();
        }
        private void ScaleBoard()
        {
            //Scale the board to be bigger in case the board size is 9
            if (game.board.BoardSize == 9)
            {
                picGoMoku.Visible = false;
                picGoMoku.Scale(new SizeF(1.5f, 1.5f));
                picGoMoku.Top = this.menuStrip1.Height + 5;
                picGoMoku.Visible = true;
            }
        }
        // int iCountGameID = 0;

        private void NewGame(Game.GameModeEnum gameMode,
           int boardSize,
           int botSearchDepth,
           UI.ThemeSpace.ThemeFactory.ThemeEnum themeEnum) =>
                NewGame(gameMode, boardSize, botSearchDepth, themeEnum, null);

        private void CreateGame(Game.GameModeEnum gameMode, int boardSize, int botDepth, Board board)
        {
            if (game != null)
            {
                game.ReleaseResource();
            }

            if (board != null)
            {
                game = new Game(this, board, bot, botDepth, gameMode);
            }
            else
            {
                game = new Game(this, boardSize, bot, botDepth, gameMode);
            }

            game.log = new SimpleLog(Utility.FileUtility.LogFilePath);

        }
        private void NewGame(Game.GameModeEnum gameMode,
            int boardSize,
            int botSearchDepth,
            UI.ThemeSpace.ThemeFactory.ThemeEnum themeEnum,
            Board board)
        {

            var CurrentTheme = UI.ThemeSpace.ThemeFactory.Create(themeEnum);
            CreateGame(gameMode, boardSize, botSearchDepth, board);

            const int cellWidth = 38;
            const int cellHeight = 38;
            if (picGoMoku == null ||
                    picGoMoku.NoofRow != boardSize)
            {
                if (picGoMoku != null)
                {
                    picGoMoku.CellClicked -= PicGoMoku_CellClicked;
                    picGoMoku.ReleaseResource();
                    this.Controls.Remove(picGoMoku);
                }
                picGoMoku = new PictureBoxGoMoKu(game.board, cellWidth, cellHeight);
                picGoMoku.NoofRow = boardSize;
                picGoMoku.NoofColumn = boardSize;
                picGoMoku.BorderStyle = BorderStyle.FixedSingle;

                picGoMoku.Left = 0;
                picGoMoku.Initial(CurrentTheme);
                picGoMoku.CellClicked -= PicGoMoku_CellClicked;
                picGoMoku.CellClicked += PicGoMoku_CellClicked;

            }
            else
            {
                picGoMoku.Top = this.menuStrip1.Height + 5;
                picGoMoku.SetBoad(game.board);
                picGoMoku.Initial(CurrentTheme);
            }
            if (!this.Controls.Contains(picGoMoku))
            {
                this.Controls.Add(picGoMoku);
            }

            game.NewGame();


            picGoMoku.Visible = true;
            ScaleBoard();

            this.Height = picGoMoku.Height + picGoMoku.Top + this.menuStrip1.Height - 5;
            this.Width = picGoMoku.Width + picGoMoku.Left;

            this.RenderUI();
        }


        public void Game_BotFinishedThinking(object sender, EventArgs e)
        {


            IsBotThinking = false;
            this.Cursor = Cursors.Default;

        }
        bool IsBotThinking = false;// If this value is true it will prevent user from click on the cell

        Cursor tempCursor = Cursors.Default;
        public void Game_BotThinking(object sender, EventArgs e)
        {
            IsBotThinking = true;
            this.Cursor = Cursors.WaitCursor;
        }

        private void TShowPicGoMokue_Tick(object sender, EventArgs e)
        {

            picGoMoku.Visible = true;
            // throw new NotImplementedException();
        }

        public void Game_GameFinished(object sender, EventArgs e)
        {
            IsBotThinking = false;
            //    this.Cursor = Cursors.Default;
            if (game.GameState == Game.GameStateEnum.End)
            {
                /*
                bool isPlaywon =
                       (game.WinResult == Board.WinStatus.BlackWon && game.GameMode == Game.GameModeEnum.PlayerVsBot)
                    || (game.WinResult == Board.WinStatus.WhiteWon && game.GameMode == Game.GameModeEnum.BotVsPlayer);
                    */
                Game_GameFinishedV2(game.WinResult);
                /*
                if (isPlaywon)
                {
                    Game_GameFinishedV2(game.WinResult);
                }
                */
            }
            return;



        }
        private void Game_GameFinishedV2(Board.WinStatus winStatus)
        {
            // Deley a little bit before show the result.
            System.Threading.Thread.Sleep(300);


            String whiteTurn = "White";
            String blackTurn = "Black";

            switch (Global.CurrentSettings.ThemeEnum)
            {
                case UI.ThemeSpace.ThemeFactory.ThemeEnum.TicTacToe1:
                case UI.ThemeSpace.ThemeFactory.ThemeEnum.TicTacToe2:
                case UI.ThemeSpace.ThemeFactory.ThemeEnum.TicTacToe3:
                    whiteTurn = "X";
                    blackTurn = "O";
                    break;
                case UI.ThemeSpace.ThemeFactory.ThemeEnum.TableTennis:
                    blackTurn = "Orange";
                    break;
            }
            String message = whiteTurn + " Won.";

            if (winStatus == Board.WinStatus.NotDecidedYet)
            {
                return;
            }

            switch (winStatus)
            {

                case Board.WinStatus.BlackWon:
                    message = $"{blackTurn} Won.";
                    break;
                case Board.WinStatus.WhiteWon:
                    message = $"{whiteTurn} Won.";
                    break;
                case Board.WinStatus.Draw:
                    message = " Draw.";
                    break;
                default:
                    throw new Exception($"winStatus is not correct {winStatus}");
            }
            message += "\n Click ok if you want a rematch.";
            FormCustomMessageBox formCustomMessageBox = new FormCustomMessageBox();
            formCustomMessageBox.Message = message;
            formCustomMessageBox.ShowCancel = true;
            formCustomMessageBox.StartPosition = FormStartPosition.Manual;
            formCustomMessageBox.parentForm = this;
            formCustomMessageBox.ShowDialogAtCenter();

            if (formCustomMessageBox.DialogResult == DialogResult.Cancel)
            {
                return;
            }
            // Rematch();
            NewGame(Global.CurrentSettings.GameMode,
    Global.CurrentSettings.BoardSize,
    Global.CurrentSettings.BotDepth,
    Global.CurrentSettings.ThemeEnum);

        }

        private void FormSharpMoku_Load(object sender, EventArgs e)
        {
            this.Icon = Resource1.SharpMokuIcon;


            NewGame(Global.CurrentSettings.GameMode,
    Global.CurrentSettings.BoardSize,
    Global.CurrentSettings.BotDepth,
    Global.CurrentSettings.ThemeEnum);
            undoToolStripMenuItem.Enabled = Global.CurrentSettings.IsAllowUndo;


        }


        private void PicGoMoku_CellClicked(PictureBoxGoMoKu pic, PictureBoxGoMoKu.PositionEventArgs positionClick)
        {
            if (IsBotThinking)
            {
                return;
            }

            if (game.GameState != Game.GameStateEnum.Playing)
            {
                return;
            }

            this.CellClicked?.Invoke(this, new Board.PositionEventArgs(positionClick.Value));

        }



        public void RenderUI()
        {

            this.picGoMoku.ReRender();
            Application.DoEvents();
            /*
             * Without Application.DoEvents
             * When player click on the cell, it will not be render immidately it will wait 
             * until the bot has put the cell also. So instead of when player put black, the game render black first
             * the game just make the bot caluculate its position immidately then render both white and black
             * 
             * 1.First solution I tried to use Timer to delay the bot action, it works but it has 
             * the problem with UI thread becasue the thread created by the Timer cannot access UI thread
             * 
             * 2. Second solution, do not delay anything just put Application.DoEvents();
             */
        }

        public void MoveCursorTo(Position position)
        {
            if (!Global.CurrentSettings.IsUseBotMouseMove)
            {
                MouseAction_HasFinishedMoved(this, new EventArgs());
                return;
            }

            if (position.Row < 0 ||
                position.Col < 0 ||
                position.Row >= game.board.BoardSize ||
                position.Col >= game.board.BoardSize)
            {
                return;
            }

            Point ToPoint = this.picGoMoku.GetLowerRightPoint(position);

            MoveCursor(ToPoint);

        }
        private void MoveCursor(Point ToPoint)
        {
            MoveCursor(Cursor.Position, ToPoint);
        }
        Random RandomGenerator = new Random();
        private void MoveCursor(Point FromPoint, Point ToPoint)
        {
            MouseAction.HasFinishedMoved -= MouseAction_HasFinishedMoved;
            MouseAction.HasFinishedMoved += MouseAction_HasFinishedMoved;

            int xDifferent = Math.Abs(FromPoint.X - ToPoint.X);
            int yDifferent = Math.Abs(FromPoint.Y - ToPoint.Y);
            int xyDifferent = xDifferent + yDifferent;

            /*
             * Make a numberofSteps a little bit random
             * but it cannot be more than 35 and cannot be less than 20;
             * More number of steps, more number of time it takes
             */
            int numberOfSteps = RandomGenerator.Next(10, 40);// 100 - xyDifferent +  RandomGenerator.Next(10, 40);

            int maximumSteps = 35;
            int minimumSteps = 20;
            numberOfSteps = Math.Max(Math.Min(numberOfSteps, maximumSteps), minimumSteps);


            MouseAction.LinearSmoothMove(MouseAction.convertDrawingPointToStructPoint(ToPoint), numberOfSteps);

        }

        private void MouseAction_HasFinishedMoved(object sender, EventArgs e)
        {


            this.HasFinishedMoveCursor?.Invoke(this, new EventArgs());
        }

        private void toolStripMenuItemNewGame_Click(object sender, EventArgs e)
        {
            FormNewGame formNewGame = new FormNewGame();

            formNewGame.StartPosition = FormStartPosition.CenterParent;
            formNewGame.ShowDialog(this);
            if (formNewGame.DialogResult != DialogResult.OK)
            {
                return;
            }

            NewGame(Global.CurrentSettings.GameMode,
                Global.CurrentSettings.BoardSize,
                Global.CurrentSettings.BotDepth,
                Global.CurrentSettings.ThemeEnum);


        }

        private void toolStripMenuItemOption_Click(object sender, EventArgs e)
        {
            FormOption f = new FormOption();
            // f.Parent = this;
            f.Themed_Changed -= F_Themed_Changed;
            f.Themed_Changed += F_Themed_Changed;
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
            if (f.DialogResult != DialogResult.OK)
            {
                return;
            }

            UpdateTheme(UI.ThemeSpace.ThemeFactory.Create(Global.CurrentSettings.ThemeEnum));
            undoToolStripMenuItem.Enabled = Global.CurrentSettings.IsAllowUndo;



        }

        private void F_Themed_Changed(object sender, EventArgs e)
        {
            var eChange = (FormOption.ThemChangedEventArgs)e;
            UpdateTheme(eChange.Theme);


        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game == null ||
               !game.CanUndo ||
               IsBotThinking)
            {
                return;
            }

            game.Undo();
        }

        private void copyBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog spf = new SaveFileDialog();
            spf.InitialDirectory = Utility.FileUtility.BoardPath;
            spf.Filter = "Board |*.bin";



            var dialogResult = spf.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            string fileName = spf.FileName;

            Utility.SerializeUtility.SerializeBoard(this.game.board, fileName);
        }

        private void loadBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.InitialDirectory = Utility.FileUtility.BoardPath;
            opf.Filter = "Board |*.bin";



            var dialogResult = opf.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            string fileName = opf.FileName;

            Board board = Utility.SerializeUtility.DeserializeBoard(fileName);


            NewGame(Global.CurrentSettings.GameMode,
                Global.CurrentSettings.BoardSize,
                Global.CurrentSettings.BotDepth,
                Global.CurrentSettings.ThemeEnum,
                board);

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveFileDialog spf = new SaveFileDialog();
            spf.InitialDirectory = Utility.FileUtility.BoardPath;
            spf.Filter = "Board |*.brd";



            var dialogResult = spf.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            string fileName = spf.FileName;

            Utility.SerializeUtility.SerializeBoard(this.game.board, fileName);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.InitialDirectory = Utility.FileUtility.BoardPath;
            opf.Filter = "Board |*.brd";



            var dialogResult = opf.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            string fileName = opf.FileName;
            Board board = Utility.SerializeUtility.DeserializeBoard(fileName);

            Global.CurrentSettings.BoardSize = board.BoardSize;
            NewGame(Global.CurrentSettings.GameMode,
                Global.CurrentSettings.BoardSize,
                Global.CurrentSettings.BotDepth,
                Global.CurrentSettings.ThemeEnum,
                board);
        }

        private void toolStripMenuItemOptionExit_Click(object sender, EventArgs e)
        {
            FormCustomMessageBox f2 = new FormCustomMessageBox();
            f2.Message = "Do you want to exit ?";
            f2.ShowCancel = true;
            f2.StartPosition = FormStartPosition.Manual;
            f2.parentForm = this;
            f2.ShowDialogAtCenter();

            if (f2.DialogResult == DialogResult.Cancel)
            {
                return;
            }
            Application.Exit();

        }
    }
}
