using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.UI
{
    public interface IUI
    {

        // Raise event CellClicked so the game object will handle the operation
        event Board.CellClickHandler CellClicked;

        void RenderUI();
        void MoveCursorTo(Position position);

        //To tell the game object that bot has finished moving a cursor to the position to put.
        event EventHandler HasFinishedMoveCursor;


        /*
         These 3 Method will be trigged from Game Object.
         Game_GameFinished : UI wil display the result.
         Game_BotThinking : UI will change the cursor to an hourglass.
         Game_BotFinishedThinking : Ui will change the cursor back to default cursor and allow user to input
        */
        void Game_GameFinished(object sender, EventArgs e);
        void Game_BotThinking(object sender, EventArgs e);
        void Game_BotFinishedThinking(object sender, EventArgs e);
    }
}
