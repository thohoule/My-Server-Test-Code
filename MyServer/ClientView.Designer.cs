namespace MyServer
{
    partial class ClientView
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
            this.ConsoleTB = new System.Windows.Forms.TextBox();
            this.CommandTB = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MoveRoomButton = new System.Windows.Forms.Button();
            this.LeaveRoomButton = new System.Windows.Forms.Button();
            this.RoomTB = new System.Windows.Forms.TextBox();
            this.StressCB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ConsoleTB
            // 
            this.ConsoleTB.Location = new System.Drawing.Point(13, 13);
            this.ConsoleTB.Multiline = true;
            this.ConsoleTB.Name = "ConsoleTB";
            this.ConsoleTB.Size = new System.Drawing.Size(482, 307);
            this.ConsoleTB.TabIndex = 0;
            // 
            // CommandTB
            // 
            this.CommandTB.Location = new System.Drawing.Point(13, 326);
            this.CommandTB.Name = "CommandTB";
            this.CommandTB.Size = new System.Drawing.Size(401, 20);
            this.CommandTB.TabIndex = 1;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(420, 326);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 2;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(502, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Test";
            // 
            // MoveRoomButton
            // 
            this.MoveRoomButton.Location = new System.Drawing.Point(501, 78);
            this.MoveRoomButton.Name = "MoveRoomButton";
            this.MoveRoomButton.Size = new System.Drawing.Size(75, 23);
            this.MoveRoomButton.TabIndex = 5;
            this.MoveRoomButton.Text = "Move Room";
            this.MoveRoomButton.UseVisualStyleBackColor = true;
            this.MoveRoomButton.Click += new System.EventHandler(this.MoveRoomButton_Click);
            // 
            // LeaveRoomButton
            // 
            this.LeaveRoomButton.Location = new System.Drawing.Point(501, 107);
            this.LeaveRoomButton.Name = "LeaveRoomButton";
            this.LeaveRoomButton.Size = new System.Drawing.Size(76, 23);
            this.LeaveRoomButton.TabIndex = 6;
            this.LeaveRoomButton.Text = "Leave Room";
            this.LeaveRoomButton.UseVisualStyleBackColor = true;
            this.LeaveRoomButton.Click += new System.EventHandler(this.LeaveRoomButton_Click);
            // 
            // RoomTB
            // 
            this.RoomTB.Location = new System.Drawing.Point(501, 29);
            this.RoomTB.Name = "RoomTB";
            this.RoomTB.Size = new System.Drawing.Size(76, 20);
            this.RoomTB.TabIndex = 7;
            // 
            // StressCB
            // 
            this.StressCB.AutoSize = true;
            this.StressCB.Location = new System.Drawing.Point(505, 55);
            this.StressCB.Name = "StressCB";
            this.StressCB.Size = new System.Drawing.Size(55, 17);
            this.StressCB.TabIndex = 8;
            this.StressCB.Text = "Stress";
            this.StressCB.UseVisualStyleBackColor = true;
            // 
            // ClientView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 358);
            this.Controls.Add(this.StressCB);
            this.Controls.Add(this.RoomTB);
            this.Controls.Add(this.LeaveRoomButton);
            this.Controls.Add(this.MoveRoomButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.CommandTB);
            this.Controls.Add(this.ConsoleTB);
            this.Name = "ClientView";
            this.Text = "ClientView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ConsoleTB;
        private System.Windows.Forms.TextBox CommandTB;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button MoveRoomButton;
        private System.Windows.Forms.Button LeaveRoomButton;
        private System.Windows.Forms.TextBox RoomTB;
        private System.Windows.Forms.CheckBox StressCB;
    }
}