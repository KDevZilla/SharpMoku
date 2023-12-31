using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpMoku
{
    public partial class FormNewGame : Form
    {
        public FormNewGame()
        {
            InitializeComponent();
        }
        private void InitialValue()
        {
            this.cboBotLevel.SelectedIndex = 0;
            this.cboBoardSize.SelectedIndex = 0;
            this.cboMode.SelectedIndex = 0;

            switch (Global.CurrentSettings.GameMode)
            {
                case Game.GameModeEnum.PlayerVsPlayer:
                    cboMode.SelectedIndex = 0;
                    break;
                case Game.GameModeEnum.PlayerVsBot:
                    cboMode.SelectedIndex = 1;
                    break;
                case Game.GameModeEnum.BotVsPlayer:
                    cboMode.SelectedIndex = 2;
                    break;


            }

            cboBoardSize.SelectedIndex = 0;
            if (Global.CurrentSettings.BoardSize == 15)
            {
                this.cboBoardSize.SelectedIndex = 1;
            }
            if (Global.CurrentSettings.BotDepth == 4)
            {
                this.cboBotLevel.SelectedIndex = 1;
            }
            this.BackColor = Global.BackColor;
            this.lblBoardSize.ForeColor = Global.ForeColor;
            this.lblBotLevel.ForeColor = Global.ForeColor;
            this.lblMode.ForeColor = Global.ForeColor;


        }
        private void FormNewGame_Load(object sender, EventArgs e)
        {
            this.Icon = Resource1.SharpMokuIcon;
            this.InitialValue();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            Global.CurrentSettings.BoardSize = 9;
            if (this.cboBoardSize.SelectedIndex == 1)
            {
                Global.CurrentSettings.BoardSize = 15;
            }

            Global.CurrentSettings.GameMode = Game.GameModeEnum.PlayerVsPlayer;
            if (this.cboMode.SelectedIndex == 1)
            {
                Global.CurrentSettings.GameMode = Game.GameModeEnum.PlayerVsBot;
            }
            else if (this.cboMode.SelectedIndex == 2)
            {
                Global.CurrentSettings.GameMode = Game.GameModeEnum.BotVsPlayer;
            }

            Global.CurrentSettings.BotDepth = 2;
            if (this.cboBotLevel.SelectedIndex == 1)
            {

                Global.CurrentSettings.BotDepth = 4;
            }


            Global.SaveSettings();

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
