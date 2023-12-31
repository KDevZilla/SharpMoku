using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpMoku;
using SharpMoku.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharpMoku.Board;

namespace KGoMokuUnitTest
{
    [TestClass]
    public class GameTesting
    {
        private const int blackStoneCellValue = -1;
        private const int whiteStoneCellValue = 1;
        private const int emptyStoneCellvalue = 0;
        [TestMethod]
        public void PlayerVsPlayerPutCell()
        {
            MOCKUI ui = new MOCKUI();
            Board board = new Board(9);
            var game = new Game(ui, board, null, 1, Game.GameModeEnum.PlayerVsPlayer);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.NotBegin);

            game.NewGame();
            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);

            ui.PutStoneByUI(0, 0);


            Assert.IsTrue(game.board.CurrentTurn == Turn.White);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);
            Assert.IsTrue(game.board.Matrix[0, 0] == blackStoneCellValue);

            ui.PutStoneByUI(1, 0);
            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);
            Assert.IsTrue(game.board.Matrix[1, 0] == whiteStoneCellValue);



        }


        [TestMethod]
        public void PlayerVsPlayerWonStatus()
        {
            MOCKUI ui = new MOCKUI();
            Board board = new Board(9);

            board.PutStone(0, 0, CellValue.Black);
            board.PutStone(0, 1, CellValue.Black);
            board.PutStone(0, 2, CellValue.Black);
            board.PutStone(0, 3, CellValue.Black);


            board.PutStone(1, 0, CellValue.White);
            board.PutStone(1, 1, CellValue.White);
            board.PutStone(1, 2, CellValue.White);
            board.PutStone(1, 3, CellValue.White);


            var game = new Game(ui, board, null, 1, Game.GameModeEnum.PlayerVsPlayer);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.NotBegin);


            game.NewGame();
            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);

            ui.PutStoneByUI(0, 4);


            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.End);
            Assert.IsTrue(game.WinResult == WinStatus.BlackWon);
            Assert.IsTrue(game.board.Matrix[0, 4] == blackStoneCellValue);





        }

        [TestMethod]
        public void PlayerVsPlayerUndo()
        {
            /*
             * This test is different than BoardTest.Undo
             * Because this test would like to determine the scenario that
             * the game has end then it was undoed, what is the board's current turn ?
             * The undo method behaviour is depende on the game mode
             */
            MOCKUI ui = new MOCKUI();
            Board board = new Board(9);

            board.PutStone(0, 0, CellValue.Black);
            board.PutStone(0, 1, CellValue.Black);
            board.PutStone(0, 2, CellValue.Black);



            board.PutStone(1, 0, CellValue.White);
            board.PutStone(1, 1, CellValue.White);
            board.PutStone(1, 2, CellValue.White);
            board.PutStone(1, 3, CellValue.White);


            var game = new Game(ui, board, null, 1, Game.GameModeEnum.PlayerVsPlayer);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.NotBegin);


            game.NewGame();
            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);


            ui.PutStoneByUI(0, 3);
            Assert.IsTrue(game.board.CurrentTurn == Turn.White);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);

            game.Undo();
            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);
            Assert.IsTrue(game.board.Matrix[0, 3] == emptyStoneCellvalue);


            ui.PutStoneByUI(0, 3); //Black
            Assert.IsTrue(game.board.CurrentTurn == Turn.White);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);

            ui.PutStoneByUI(8, 8); // White
            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);


            ui.PutStoneByUI(0, 4); // Black
            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.End);
            Assert.IsTrue(game.WinResult == WinStatus.BlackWon);
            Assert.IsTrue(game.board.Matrix[0, 4] == blackStoneCellValue);





            game.Undo();

            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);
            Assert.IsTrue(game.board.Matrix[0, 4] == emptyStoneCellvalue);




        }

        //Cannot test PlayerVsBotUndo this due to the way application design
        public void PlayerVsBotUndo()
        {
            /*
             * This test is different than BoardTest.Undo
             * Because this test would like to determine the scenario that
             * the game has end then it was undoed, what is the board's current turn ?
             * The undo method behaviour is depende on the game mode
             */
            MOCKUI ui = new MOCKUI();
            Board board = new Board(9);


            board.PutStone(0, 1, CellValue.Black);
            board.PutStone(0, 2, CellValue.Black);
            board.PutStone(0, 3, CellValue.Black);
            board.PutStone(0, 4, CellValue.Black);


            var bot = new SharpMoku.AI.EvaluateV2();

            var game = new Game(ui, board, bot, 1, Game.GameModeEnum.PlayerVsBot);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.NotBegin);


            game.NewGame();
            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);


            ui.PutStoneByUI(0, 8);
            //Just put Stone into any postion that not make game finish
            //After ui.PutStoneByUI, the botwill automatically put stone.


            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);
            Assert.IsTrue(game.board.Matrix[0, 8] == blackStoneCellValue);
            game.Undo();


            Assert.IsTrue(game.board.CurrentTurn == Turn.Black);
            Assert.IsTrue(game.GameState == Game.GameStateEnum.Playing);
            Assert.IsTrue(game.WinResult == WinStatus.NotDecidedYet);
            Assert.IsTrue(game.board.Matrix[0, 8] == emptyStoneCellvalue);





        }
        private void Ui_CellClicked(object sender, Board.PositionEventArgs positionClick)
        {
            //throw new NotImplementedException();
        }
    }
}
