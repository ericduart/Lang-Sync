namespace LangSyncServer.forms
{
    partial class FormWaitingPlayers
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            labelPartyCode = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            buttonStartGame = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            lblPlayersAnswered = new Label();
            lblCurrentGrammarSpanish = new Label();
            label3 = new Label();
            lblCurrentGrammarEnglish = new Label();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            NameOfUser = new DataGridViewTextBoxColumn();
            isCorrect = new DataGridViewTextBoxColumn();
            UserInput = new DataGridViewTextBoxColumn();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // labelPartyCode
            // 
            labelPartyCode.Font = new Font("Segoe UI", 19F);
            labelPartyCode.Location = new Point(576, 3);
            labelPartyCode.Name = "labelPartyCode";
            labelPartyCode.Size = new Size(182, 36);
            labelPartyCode.TabIndex = 0;
            labelPartyCode.Text = "Getting code...";
            labelPartyCode.TextAlign = ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(24, 42);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(717, 322);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // buttonStartGame
            // 
            buttonStartGame.Location = new Point(359, 400);
            buttonStartGame.Name = "buttonStartGame";
            buttonStartGame.Size = new Size(105, 38);
            buttonStartGame.TabIndex = 2;
            buttonStartGame.Text = "Start";
            buttonStartGame.UseVisualStyleBackColor = true;
            buttonStartGame.Click += buttonStartGame_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, -7);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(772, 401);
            tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(flowLayoutPanel1);
            tabPage1.Controls.Add(labelPartyCode);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(764, 373);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridView1);
            tabPage2.Controls.Add(lblPlayersAnswered);
            tabPage2.Controls.Add(lblCurrentGrammarSpanish);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(lblCurrentGrammarEnglish);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(764, 373);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblPlayersAnswered
            // 
            lblPlayersAnswered.AutoSize = true;
            lblPlayersAnswered.Location = new Point(221, 29);
            lblPlayersAnswered.Name = "lblPlayersAnswered";
            lblPlayersAnswered.Size = new Size(24, 15);
            lblPlayersAnswered.TabIndex = 5;
            lblPlayersAnswered.Text = "0/0";
            // 
            // lblCurrentGrammarSpanish
            // 
            lblCurrentGrammarSpanish.AutoSize = true;
            lblCurrentGrammarSpanish.Location = new Point(68, 162);
            lblCurrentGrammarSpanish.Name = "lblCurrentGrammarSpanish";
            lblCurrentGrammarSpanish.Size = new Size(119, 15);
            lblCurrentGrammarSpanish.TabIndex = 4;
            lblCurrentGrammarSpanish.Text = "Some spanish word...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(68, 147);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 3;
            label3.Text = "Spanish";
            // 
            // lblCurrentGrammarEnglish
            // 
            lblCurrentGrammarEnglish.AutoSize = true;
            lblCurrentGrammarEnglish.Location = new Point(68, 119);
            lblCurrentGrammarEnglish.Name = "lblCurrentGrammarEnglish";
            lblCurrentGrammarEnglish.Size = new Size(117, 15);
            lblCurrentGrammarEnglish.TabIndex = 2;
            lblCurrentGrammarEnglish.Text = "Some english word...";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(68, 104);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 1;
            label1.Text = "English";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.RaisedHorizontal;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.SelectionForeColor = Color.CornflowerBlue;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { NameOfUser, isCorrect, UserInput });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.White;
            dataGridView1.Location = new Point(221, 58);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.IndianRed;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Lime;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.White;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(525, 293);
            dataGridView1.TabIndex = 1;
            // 
            // Name
            // 
            NameOfUser.HeaderText = "Name";
            NameOfUser.Name = "Name";
            // 
            // isCorrect
            // 
            isCorrect.HeaderText = "Is correct";
            isCorrect.Name = "isCorrect";
            // 
            // UserInput
            // 
            UserInput.HeaderText = "User input";
            UserInput.Name = "UserInput";
            UserInput.Resizable = DataGridViewTriState.True;
            // 
            // FormWaitingPlayers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 450);
            Controls.Add(buttonStartGame);
            Controls.Add(tabControl1);
            Name = "FormWaitingPlayers";
            Text = "FormWaitingPlayers";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label labelPartyCode;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button buttonStartGame;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label lblCurrentGrammarSpanish;
        private Label label3;
        private Label lblCurrentGrammarEnglish;
        private Label label1;
        private Label lblPlayersAnswered;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn NameOfUser;
        private DataGridViewTextBoxColumn isCorrect;
        private DataGridViewTextBoxColumn UserInput;
    }
}