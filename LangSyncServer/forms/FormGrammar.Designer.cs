namespace LangSync.forms
{
    partial class FormGrammar
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
            label1 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            textBoxEnglish = new TextBox();
            label3 = new Label();
            label4 = new Label();
            textBoxSpanish = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 15);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 0;
            label1.Text = "GRAMMAR";
            label1.Click += label1_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AllowDrop = true;
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(19, 47);
            flowLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(906, 235);
            flowLayoutPanel1.TabIndex = 1;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.DragDrop += flowLayoutPanel1_DragDrop;
            flowLayoutPanel1.DragEnter += flowLayoutPanel1_DragEnter;
            // 
            // textBoxEnglish
            // 
            textBoxEnglish.Location = new Point(19, 321);
            textBoxEnglish.Margin = new Padding(4, 3, 4, 3);
            textBoxEnglish.Name = "textBoxEnglish";
            textBoxEnglish.Size = new Size(179, 23);
            textBoxEnglish.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 302);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 4;
            label3.Text = "English";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(214, 303);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(48, 15);
            label4.TabIndex = 5;
            label4.Text = "Spanish";
            // 
            // textBoxSpanish
            // 
            textBoxSpanish.Location = new Point(217, 321);
            textBoxSpanish.Margin = new Padding(4, 3, 4, 3);
            textBoxSpanish.Name = "textBoxSpanish";
            textBoxSpanish.Size = new Size(179, 23);
            textBoxSpanish.TabIndex = 6;
            // 
            // button1
            // 
            button1.Location = new Point(19, 361);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(377, 44);
            button1.TabIndex = 7;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(416, 361);
            button2.Name = "button2";
            button2.Size = new Size(172, 44);
            button2.TabIndex = 8;
            button2.Text = "Start game";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // FormPreGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(939, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBoxSpanish);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBoxEnglish);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(label1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormPreGame";
            Text = "Lang-Sync / Pre game";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxEnglish;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSpanish;
        private System.Windows.Forms.Button button1;
        private Button button2;
    }
}