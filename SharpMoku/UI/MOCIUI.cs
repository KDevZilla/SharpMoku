using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.UI
{
    public class MOCKUI : IUI
    {
        public event Board.CellClickHandler CellClicked;
        public event EventHandler HasFinishedMoveCursor;

        public void Game_BotFinishedThinking(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        public void Game_BotThinking(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        public void Game_GameFinished(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        public void MoveCursorTo(Position position)
        {
            // throw new NotImplementedException();
            this.HasFinishedMoveCursor?.Invoke(this, new EventArgs());
        }

        public void RenderUI()
        {
           // throw new NotImplementedException();
        }

        public void PutStoneByUI(int row, int column)
        {
            CellClicked?.Invoke(this, new Board.PositionEventArgs(new Position(row, column)));
        }
    }
}
