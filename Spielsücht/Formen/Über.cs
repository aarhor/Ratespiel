using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spielsücht
{
    public partial class Über : Form
    {
        public Über()
        {
            InitializeComponent();
        }

        private void Über_Load(object sender, EventArgs e)
        {
            label3.Text = Properties.Settings.Default.Version;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            Changelog FormChangelog = new Changelog();
            FormChangelog.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.By_Ueber = true;
            Properties.Settings.Default.Save();

            Hinweis FormHinweis = new Hinweis();
            FormHinweis.Show();
        }
    }
}
