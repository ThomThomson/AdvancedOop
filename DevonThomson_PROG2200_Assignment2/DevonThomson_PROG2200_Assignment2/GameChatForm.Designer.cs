namespace ChatGUI {
    partial class GameChatForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.ConversationText = new System.Windows.Forms.TextBox();
            this.SendMessageText = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConversationText
            // 
            this.ConversationText.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ConversationText.Enabled = false;
            this.ConversationText.Location = new System.Drawing.Point(15, 141);
            this.ConversationText.Multiline = true;
            this.ConversationText.Name = "ConversationText";
            this.ConversationText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ConversationText.Size = new System.Drawing.Size(891, 415);
            this.ConversationText.TabIndex = 1;
            // 
            // SendMessageText
            // 
            this.SendMessageText.Location = new System.Drawing.Point(15, 115);
            this.SendMessageText.Name = "SendMessageText";
            this.SendMessageText.Size = new System.Drawing.Size(810, 20);
            this.SendMessageText.TabIndex = 2;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(831, 112);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(918, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectMenuItem,
            this.DisconnectMenuItem,
            this.ExitMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // ConnectMenuItem
            // 
            this.ConnectMenuItem.Name = "ConnectMenuItem";
            this.ConnectMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ConnectMenuItem.Text = "Connect";
            this.ConnectMenuItem.Click += new System.EventHandler(this.ConnectMenuItem_Click);
            // 
            // DisconnectMenuItem
            // 
            this.DisconnectMenuItem.Name = "DisconnectMenuItem";
            this.DisconnectMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DisconnectMenuItem.Text = "Disconnect";
            this.DisconnectMenuItem.Click += new System.EventHandler(this.DisconnectMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // GameChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 568);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.SendMessageText);
            this.Controls.Add(this.ConversationText);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameChatForm";
            this.Text = "GameChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameChatForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ConversationText;
        private System.Windows.Forms.TextBox SendMessageText;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConnectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisconnectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
    }
}

