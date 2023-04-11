namespace MyServer
{
    partial class DebugSelector
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
            this.HostButton = new System.Windows.Forms.Button();
            this.ClientButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // HostButton
            // 
            this.HostButton.Location = new System.Drawing.Point(13, 13);
            this.HostButton.Name = "HostButton";
            this.HostButton.Size = new System.Drawing.Size(75, 23);
            this.HostButton.TabIndex = 0;
            this.HostButton.Text = "Host";
            this.HostButton.UseVisualStyleBackColor = true;
            this.HostButton.Click += new System.EventHandler(this.HostButton_Click);
            // 
            // ClientButton
            // 
            this.ClientButton.Location = new System.Drawing.Point(95, 12);
            this.ClientButton.Name = "ClientButton";
            this.ClientButton.Size = new System.Drawing.Size(75, 23);
            this.ClientButton.TabIndex = 1;
            this.ClientButton.Text = "Client";
            this.ClientButton.UseVisualStyleBackColor = true;
            this.ClientButton.Click += new System.EventHandler(this.ClientButton_Click);
            // 
            // DebugSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(181, 43);
            this.Controls.Add(this.ClientButton);
            this.Controls.Add(this.HostButton);
            this.Name = "DebugSelector";
            this.Text = "DebugSelector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button HostButton;
        private System.Windows.Forms.Button ClientButton;
    }
}