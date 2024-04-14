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
            labelPartyCode = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            buttonStartGame = new Button();
            SuspendLayout();
            // 
            // labelPartyCode
            // 
            labelPartyCode.Font = new Font("Segoe UI", 19F);
            labelPartyCode.Location = new Point(602, 9);
            labelPartyCode.Name = "labelPartyCode";
            labelPartyCode.Size = new Size(182, 36);
            labelPartyCode.TabIndex = 0;
            labelPartyCode.Text = "Getting code...";
            labelPartyCode.TextAlign = ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(12, 64);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(772, 316);
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
            // 
            // FormWaitingPlayers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 450);
            Controls.Add(buttonStartGame);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(labelPartyCode);
            Name = "FormWaitingPlayers";
            Text = "FormWaitingPlayers";
            ResumeLayout(false);
        }

        #endregion

        private Label labelPartyCode;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button buttonStartGame;
    }
}