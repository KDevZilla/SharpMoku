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
    public partial class FormCustomMessageBox : Form
    {
        public FormCustomMessageBox()
        {
            InitializeComponent();
        }

        public String Caption
        {
            get => this.Text;
            set => this.Text = value;

        }
        public String Message
        {
            get => this.txtMessage.Text;
            set => this.txtMessage.Text = value;
        }

        private Boolean _ShowCancel = false;
        public Boolean ShowCancel
        {
            get => _ShowCancel;
            set
            {
                _ShowCancel = value;
                SetCancelbuttonVisible();
            }
        }
        private void SetCancelbuttonVisible()
        {
            this.btnCancel.Visible = true;
            this.btnOK.Left = 247;
            if (!_ShowCancel)
            {
                this.btnCancel.Visible = false;
                this.btnOK.Left = this.btnCancel.Left;
            }
        }
        private void UpdateUIColor(Color backColor, Color foreColor)
        {

            this.txtMessage.BackColor = backColor;
            this.txtMessage.ForeColor = foreColor;

            this.BackColor = backColor;
        }
        private void SetTheme()
        {
            /*
            this.txtMessage.BackColor = Global.CurrentTheme.FormBackColor;
            this.txtMessage.ForeColor = Global.CurrentTheme.LabelForeColor;
            Utility.UI.MakeFormCaptionToBeDarkMode(this, Global.CurrentTheme.IsFormCaptionDarkMode);
            Utility.ThemeUtility themeUtil = new Utility.ThemeUtility(Global.CurrentTheme);
            themeUtil
                .SetButtonColor(this.btnOK, this.btnCancel)
                .SetForm(this);

            */
        }
        private void FormCustomMessageBox_Load(object sender, EventArgs e)
        {
            this.Icon = Resource1.SharpMokuIcon;

            this.pictureBox1.Image = SystemIcons.Information.ToBitmap();
            //this.SetTheme();
            this.txtMessage.GotFocus += (txts, txte)
                => this.btnOK.Focus();



            UpdateUIColor(Global.BackColor, Global.ForeColor);
        }

        private delegate void DisplayDialogCallback();
        public Form parentForm = null;
        public void ShowDialogAtCenter()
        {
            this.Left = parentForm.Left + ((parentForm.Width - this.Width) / 2);
            this.Top = parentForm.Top + ((parentForm.Height - this.Height) / 2);

            this.ShowDialog();
        }
        public void DisplayDialog()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DisplayDialogCallback(DisplayDialog));
                return;
            }

            if (this.Handle != (IntPtr)0) // you can also use: this.IsHandleCreated
            {
                if (parentForm != null)
                {
                    this.Left = parentForm.Left + ((parentForm.Width - this.Width) / 2);
                    this.Top = parentForm.Top + ((parentForm.Height - this.Height) / 2);
                }
                this.ShowDialog();

                if (this.CanFocus)
                {
                    this.Focus();
                }
            }
            else
            {
                // Handle the error
            }

        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
