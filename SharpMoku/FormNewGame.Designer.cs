namespace SharpMoku
{
    partial class FormNewGame
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cboBoardSize = new System.Windows.Forms.ComboBox();
            this.lblBoardSize = new System.Windows.Forms.Label();
            this.cboBotLevel = new System.Windows.Forms.ComboBox();
            this.lblBotLevel = new System.Windows.Forms.Label();
            this.cboMode = new System.Windows.Forms.ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(281, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 44);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(188, 154);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 44);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cboBoardSize
            // 
            this.cboBoardSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBoardSize.FormattingEnabled = true;
            this.cboBoardSize.Items.AddRange(new object[] {
            "9 x 9",
            "15 x 15"});
            this.cboBoardSize.Location = new System.Drawing.Point(120, 41);
            this.cboBoardSize.Name = "cboBoardSize";
            this.cboBoardSize.Size = new System.Drawing.Size(248, 29);
            this.cboBoardSize.TabIndex = 5;
            // 
            // lblBoardSize
            // 
            this.lblBoardSize.AutoSize = true;
            this.lblBoardSize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoardSize.ForeColor = System.Drawing.Color.Black;
            this.lblBoardSize.Location = new System.Drawing.Point(28, 44);
            this.lblBoardSize.Name = "lblBoardSize";
            this.lblBoardSize.Size = new System.Drawing.Size(86, 21);
            this.lblBoardSize.TabIndex = 4;
            this.lblBoardSize.Text = "Board Size:";
            // 
            // cboBotLevel
            // 
            this.cboBotLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBotLevel.FormattingEnabled = true;
            this.cboBotLevel.Items.AddRange(new object[] {
            "Normal",
            "Hard"});
            this.cboBotLevel.Location = new System.Drawing.Point(120, 110);
            this.cboBotLevel.Name = "cboBotLevel";
            this.cboBotLevel.Size = new System.Drawing.Size(248, 29);
            this.cboBotLevel.TabIndex = 9;
            // 
            // lblBotLevel
            // 
            this.lblBotLevel.AutoSize = true;
            this.lblBotLevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBotLevel.ForeColor = System.Drawing.Color.Black;
            this.lblBotLevel.Location = new System.Drawing.Point(38, 113);
            this.lblBotLevel.Name = "lblBotLevel";
            this.lblBotLevel.Size = new System.Drawing.Size(76, 21);
            this.lblBotLevel.TabIndex = 8;
            this.lblBotLevel.Text = "Bot Level:";
            // 
            // cboMode
            // 
            this.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMode.FormattingEnabled = true;
            this.cboMode.Items.AddRange(new object[] {
            "Human vs Human",
            "Human vs Bot",
            "Bot vs Human"});
            this.cboMode.Location = new System.Drawing.Point(120, 76);
            this.cboMode.Name = "cboMode";
            this.cboMode.Size = new System.Drawing.Size(248, 29);
            this.cboMode.TabIndex = 11;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Black;
            this.lblMode.Location = new System.Drawing.Point(61, 79);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(53, 21);
            this.lblMode.TabIndex = 10;
            this.lblMode.Text = "Mode:";
            // 
            // FormNewGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(188)))), ((int)(((byte)(68)))));
            this.ClientSize = new System.Drawing.Size(382, 208);
            this.Controls.Add(this.cboMode);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.cboBotLevel);
            this.Controls.Add(this.lblBotLevel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboBoardSize);
            this.Controls.Add(this.lblBoardSize);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "FormNewGame";
            this.Text = "New Game";
            this.Load += new System.EventHandler(this.FormNewGame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cboBoardSize;
        private System.Windows.Forms.Label lblBoardSize;
        private System.Windows.Forms.ComboBox cboBotLevel;
        private System.Windows.Forms.Label lblBotLevel;
        private System.Windows.Forms.ComboBox cboMode;
        private System.Windows.Forms.Label lblMode;
    }
}