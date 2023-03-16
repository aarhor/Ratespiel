using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spielsücht
{
    public partial class Changelog : Form
    {
        public Changelog()
        {
            InitializeComponent();
        }

        private void ChangelogLoad(object sender, EventArgs e)
        {
            string Pfad = Ratespiel.Properties.Resources.Changelog.ToString();
            richTextBox_Changelog.Text = Pfad;
        }
    }
}
