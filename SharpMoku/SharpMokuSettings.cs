using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku
{
    [Serializable]
    public class SharpMokuSettings
    {
        public int BotDepth { get; set; } = 1;
        public int BoardSize { get; set; } = 9;
        public Game.GameModeEnum GameMode { get; set; } = Game.GameModeEnum.PlayerVsBot;
        public UI.ThemeSpace.ThemeFactory.ThemeEnum ThemeEnum { get; set; } = UI.ThemeSpace.ThemeFactory.ThemeEnum.Gomoku1;
        public Boolean IsUseBotMouseMove { get; set; } = true;
        public Boolean IsWriteLog { get; set; } = false;
        public Boolean IsAllowUndo { get; set; } = false;
        
    }
}
