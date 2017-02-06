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
            this.ConnectedLabel = new System.Windows.Forms.Label();
            this.ConversationText = new System.Windows.Forms.TextBox();
            this.SendMessageText = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConnectedLabel
            // 
            this.ConnectedLabel.AutoSize = true;
            this.ConnectedLabel.Location = new System.Drawing.Point(12, 9);
            this.ConnectedLabel.Name = "ConnectedLabel";
            this.ConnectedLabel.Size = new System.Drawing.Size(35, 13);
            this.ConnectedLabel.TabIndex = 0;
            this.ConnectedLabel.Text = "label1";
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
            this.SendMessageText.Size = new System.Drawing.Size(781, 20);
            this.SendMessageText.TabIndex = 2;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(802, 112);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // GameChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 568);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.SendMessageText);
            this.Controls.Add(this.ConversationText);
            this.Controls.Add(this.ConnectedLabel);
            this.Name = "GameChatForm";
            this.Text = "GameChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameChatForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ConnectedLabel;
        private System.Windows.Forms.TextBox ConversationText;
        private System.Windows.Forms.TextBox SendMessageText;
        private System.Windows.Forms.Button SendButton;
    }
}

