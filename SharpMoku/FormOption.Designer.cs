namespace SharpMoku
{
    partial class FormOption
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTheme = new System.Windows.Forms.Label();
            this.cboTheme = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkBotMouseMove = new System.Windows.Forms.CheckBox();
            this.chkAllowUndo = new System.Windows.Forms.CheckBox();
            this.chkWriteLogFile = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTheme
            // 
            this.lblTheme.AutoSize = true;
            this.lblTheme.Location = new System.Drawing.Point(23, 18);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(60, 21);
            this.lblTheme.TabIndex = 0;
            this.lblTheme.Text = "Theme:";
            // 
            // cboTheme
            // 
            this.cboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTheme.FormattingEnabled = true;
            this.cboTheme.Items.AddRange(new object[] {
            "Gomoku1",
            "Gomoku2",
            "Gomoku3",
            "Gomoku4",
            "Gomoku5",
            "TicTacToe1",
            "TicTacToe2",
            "TicTacToe3",
            "Table Tennis"});
            this.cboTheme.Location = new System.Drawing.Point(81, 18);
            this.cboTheme.Name = "cboTheme";
            this.cboTheme.Size = new System.Drawing.Size(248, 29);
            this.cboTheme.TabIndex = 1;
            this.cboTheme.SelectedIndexChanged += new System.EventHandler(this.cboTheme_SelectedIndexChanged_1);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(149, 182);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 47);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(242, 182);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 47);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkBotMouseMove
            // 
            this.chkBotMouseMove.AutoSize = true;
            this.chkBotMouseMove.Location = new System.Drawing.Point(81, 71);
            this.chkBotMouseMove.Name = "chkBotMouseMove";
            this.chkBotMouseMove.Size = new System.Drawing.Size(146, 25);
            this.chkBotMouseMove.TabIndex = 4;
            this.chkBotMouseMove.Text = "Bot mouse move";
            this.chkBotMouseMove.UseVisualStyleBackColor = true;
            // 
            // chkAllowUndo
            // 
            this.chkAllowUndo.AutoSize = true;
            this.chkAllowUndo.Location = new System.Drawing.Point(81, 107);
            this.chkAllowUndo.Name = "chkAllowUndo";
            this.chkAllowUndo.Size = new System.Drawing.Size(153, 25);
            this.chkAllowUndo.TabIndex = 5;
            this.chkAllowUndo.Text = "Allow Undo move";
            this.chkAllowUndo.UseVisualStyleBackColor = true;
            // 
            // chkWriteLogFile
            // 
            this.chkWriteLogFile.AutoSize = true;
            this.chkWriteLogFile.Location = new System.Drawing.Point(81, 144);
            this.chkWriteLogFile.Name = "chkWriteLogFile";
            this.chkWriteLogFile.Size = new System.Drawing.Size(122, 25);
            this.chkWriteLogFile.TabIndex = 6;
            this.chkWriteLogFile.Text = "Write Log file";
            this.chkWriteLogFile.UseVisualStyleBackColor = true;
            // 
            // FormOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(188)))), ((int)(((byte)(68)))));
            this.ClientSize = new System.Drawing.Size(339, 232);
            this.Controls.Add(this.chkWriteLogFile);
            this.Controls.Add(this.chkAllowUndo);
            this.Controls.Add(this.chkBotMouseMove);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboTheme);
            this.Controls.Add(this.lblTheme);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FormOption";
            this.Text = "Option";
            this.Load += new System.EventHandler(this.FormOption_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTheme;
        private System.Windows.Forms.ComboBox cboTheme;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkBotMouseMove;
        private System.Windows.Forms.CheckBox chkAllowUndo;
        private System.Windows.Forms.CheckBox chkWriteLogFile;
    }
}