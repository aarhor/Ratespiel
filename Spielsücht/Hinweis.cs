using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spielsücht
{
    public partial class Hinweis : Form
    {
        public Hinweis()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.By_Ueber == false)
            {
                Auswahl FormAuswahl = new Auswahl();
                FormAuswahl.Show();
                Hide();
            }
            else
            {
                Properties.Settings.Default.By_Ueber = false;
                Properties.Settings.Default.Save();
                Hide();
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Properties.Settings.Default.Hinweis_OK = true;
                Properties.Settings.Default.Save();
                button1.Enabled = true;
            }
            else
            {
                Properties.Settings.Default.Hinweis_OK = false;
                Properties.Settings.Default.Save();
                button1.Enabled = false;
            }
        }

        private void Hinweis_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = Properties.Resources.Hinweis;

            if (Properties.Settings.Default.Hinweis_OK)
            {
                checkBox1.Checked = true;
                button1.Enabled = true;
            }
            else
            {
                checkBox1.Checked = false;
                button1.Enabled = false;
            }
        }

        private void Hinweis_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.Hinweis_OK == false)
            {
                MessageBox.Show("Bitte lese dir den Hinweis durch.");
                e.Cancel = true;
            }
        }
    }
}
