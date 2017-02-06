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
            this.ConversationText.Location = new System.Drawing.Point(15, 141);
            this.ConversationText.Name = "ConversationText";
            this.ConversationText.Size = new System.Drawing.Size(891, 20);
            this.ConversationText.TabIndex = 1;
            // 
            // GameChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 568);
            this.Controls.Add(this.ConversationText);
            this.Controls.Add(this.ConnectedLabel);
            this.Name = "GameChatForm";
            this.Text = "GameChat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ConnectedLabel;
        private System.Windows.Forms.TextBox ConversationText;
    }
}

