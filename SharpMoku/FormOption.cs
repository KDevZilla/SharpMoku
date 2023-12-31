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
    public partial class FormOption : Form
    {
        public FormOption()
        {
            InitializeComponent();
        }
        public event EventHandler Themed_Changed;
        public class ThemChangedEventArgs : EventArgs
        {
            public UI.ThemeSpace.Theme Theme { get; private set; }
            public ThemChangedEventArgs(UI.ThemeSpace.Theme theme)
            {
                this.Theme = theme;
            }


        }
        private void InitialValue()
        {


            this.chkAllowUndo.Checked = Global.CurrentSettings.IsAllowUndo;
            this.chkWriteLogFile.Checked = Global.CurrentSettings.IsWriteLog;

            this.chkBotMouseMove.Checked = Global.CurrentSettings.IsUseBotMouseMove;
            this.cboTheme.SelectedIndex = (int)Global.CurrentSettings.ThemeEnum;

            this.cboTheme.SelectedIndexChanged += CboTheme_SelectedIndexChanged;
            UpdateUIColor(Global.BackColor, Global.ForeColor);

        }
        private void UpdateUIColor(Color backColor, Color foreColor)
        {
            this.lblTheme.ForeColor = foreColor;
            this.chkBotMouseMove.ForeColor = foreColor;
            this.chkWriteLogFile.ForeColor = foreColor;
            this.chkAllowUndo.ForeColor = foreColor;

            this.BackColor = backColor;
        }
        private void CboTheme_SelectedIndexChanged(object sender, EventArgs e)
        {

            var themeEnum = (UI.ThemeSpace.ThemeFactory.ThemeEnum)this.cboTheme.SelectedIndex;

            ThemChangedEventArgs eventArgs = new ThemChangedEventArgs(UI.ThemeSpace.ThemeFactory.Create(themeEnum));
            Themed_Changed?.Invoke(this, eventArgs);

            UI.ThemeSpace.ThemeFactory.BackColor(themeEnum);
            UpdateUIColor(UI.ThemeSpace.ThemeFactory.BackColor(themeEnum),
            UI.ThemeSpace.ThemeFactory.ForeColor(themeEnum));

        }

        private void FormOption_Load(object sender, EventArgs e)
        {
            this.Icon = Resource1.SharpMokuIcon;

            this.InitialValue();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            Global.CurrentSettings.IsUseBotMouseMove = this.chkBotMouseMove.Checked;

            Global.CurrentSettings.IsAllowUndo = this.chkAllowUndo.Checked;
            Global.CurrentSettings.IsWriteLog = this.chkWriteLogFile.Checked;

            Global.CurrentSettings.ThemeEnum = (UI.ThemeSpace.ThemeFactory.ThemeEnum)this.cboTheme.SelectedIndex;

            Global.SaveSettings();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cboTheme_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
