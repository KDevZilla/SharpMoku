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
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            this.Icon = Resource1.SharpMokuIcon;
            this.UpdateUIColor(Global.BackColor, Global.ForeColor);
        }
        private void UpdateUIColor(Color backColor, Color foreColor)
        {
            this.lblProgramName.ForeColor = foreColor;
            this.linkLabel1.LinkColor = foreColor;
            this.BackColor = backColor;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.linkLabel1.Text);
        }
    }
}
