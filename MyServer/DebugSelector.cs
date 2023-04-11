using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyServer
{
    public partial class DebugSelector : Form
    {
        public DebugSelector()
        {
            InitializeComponent();
        }

        private void HostButton_Click(object sender, EventArgs e)
        {
            ServerView server = new ServerView();
            server.Show();

            server.FormClosed += (s, args) => this.Close();
            Hide();
        }

        private void ClientButton_Click(object sender, EventArgs e)
        {
            ClientView server = new ClientView();
            server.Show();

            server.FormClosed += (s, args) => this.Close();
            Hide();
        }
    }
}
