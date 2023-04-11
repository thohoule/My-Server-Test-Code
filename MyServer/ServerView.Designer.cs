namespace MyServer
{
    partial class ServerView
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.ServerStatusLabel = new System.Windows.Forms.Label();
            this.ConnectionsLB = new System.Windows.Forms.ListBox();
            this.RoomsTV = new System.Windows.Forms.TreeView();
            this.tbCommandLine = new System.Windows.Forms.TextBox();
            this.ToSelectedButton = new System.Windows.Forms.Button();
            this.BroadcastButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ConsoleTB = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(813, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Status:";
            // 
            // ServerStatusLabel
            // 
            this.ServerStatusLabel.AutoSize = true;
            this.ServerStatusLabel.Location = new System.Drawing.Point(58, 24);
            this.ServerStatusLabel.Name = "ServerStatusLabel";
            this.ServerStatusLabel.Size = new System.Drawing.Size(0, 13);
            this.ServerStatusLabel.TabIndex = 2;
            // 
            // ConnectionsLB
            // 
            this.ConnectionsLB.FormattingEnabled = true;
            this.ConnectionsLB.Location = new System.Drawing.Point(15, 41);
            this.ConnectionsLB.Name = "ConnectionsLB";
            this.ConnectionsLB.Size = new System.Drawing.Size(164, 446);
            this.ConnectionsLB.TabIndex = 3;
            // 
            // RoomsTV
            // 
            this.RoomsTV.Location = new System.Drawing.Point(663, 38);
            this.RoomsTV.Name = "RoomsTV";
            this.RoomsTV.Size = new System.Drawing.Size(138, 449);
            this.RoomsTV.TabIndex = 5;
            // 
            // tbCommandLine
            // 
            this.tbCommandLine.Location = new System.Drawing.Point(185, 467);
            this.tbCommandLine.Name = "tbCommandLine";
            this.tbCommandLine.Size = new System.Drawing.Size(306, 20);
            this.tbCommandLine.TabIndex = 6;
            // 
            // ToSelectedButton
            // 
            this.ToSelectedButton.Enabled = false;
            this.ToSelectedButton.Location = new System.Drawing.Point(582, 467);
            this.ToSelectedButton.Name = "ToSelectedButton";
            this.ToSelectedButton.Size = new System.Drawing.Size(75, 23);
            this.ToSelectedButton.TabIndex = 8;
            this.ToSelectedButton.Text = "To Selected";
            this.ToSelectedButton.UseVisualStyleBackColor = true;
            // 
            // BroadcastButton
            // 
            this.BroadcastButton.Location = new System.Drawing.Point(501, 467);
            this.BroadcastButton.Name = "BroadcastButton";
            this.BroadcastButton.Size = new System.Drawing.Size(75, 23);
            this.BroadcastButton.TabIndex = 9;
            this.BroadcastButton.Text = "Broadcast";
            this.BroadcastButton.UseVisualStyleBackColor = true;
            this.BroadcastButton.Click += new System.EventHandler(this.BroadcastButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(660, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "World";
            // 
            // ConsoleTB
            // 
            this.ConsoleTB.Location = new System.Drawing.Point(185, 38);
            this.ConsoleTB.Multiline = true;
            this.ConsoleTB.Name = "ConsoleTB";
            this.ConsoleTB.Size = new System.Drawing.Size(469, 420);
            this.ConsoleTB.TabIndex = 11;
            // 
            // ServerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 494);
            this.Controls.Add(this.ConsoleTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BroadcastButton);
            this.Controls.Add(this.ToSelectedButton);
            this.Controls.Add(this.tbCommandLine);
            this.Controls.Add(this.RoomsTV);
            this.Controls.Add(this.ConnectionsLB);
            this.Controls.Add(this.ServerStatusLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ServerView";
            this.Text = "Server View";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox ConnectionsLB;
        private System.Windows.Forms.Label ServerStatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TreeView RoomsTV;
        private System.Windows.Forms.Button BroadcastButton;
        private System.Windows.Forms.Button ToSelectedButton;
        private System.Windows.Forms.TextBox tbCommandLine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ConsoleTB;
    }
}