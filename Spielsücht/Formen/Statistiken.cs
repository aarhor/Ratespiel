using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Spielsücht
{
    public partial class Statistiken : Form
    {
        public Statistiken()
        {
            InitializeComponent();
        }

        double Aktuelles_Guthaben = Ratespiel.Properties.Settings.Default.Aktueller_Kontostand;
        double Gewonnen = Ratespiel.Properties.Settings.Default.Gewonnen;
        double Verloren = Ratespiel.Properties.Settings.Default.Verloren;
        double Spiel_beendet = Ratespiel.Properties.Settings.Default.Spiel_beendet;

        public void Daten()
        {
            double Aktuelles_Guthaben = Ratespiel.Properties.Settings.Default.Aktueller_Kontostand;
            double Gewonnen = Ratespiel.Properties.Settings.Default.Gewonnen;
            double Verloren = Ratespiel.Properties.Settings.Default.Verloren;
            double Spiel_beendet = Ratespiel.Properties.Settings.Default.Spiel_beendet;
            double GV = Math.Round(Gewonnen / Verloren, 3);

            lbl_Guthaben.Text = "Aktuelles Guthaben: " + string.Format("{0:c}", Aktuelles_Guthaben);
            lbl_Gewonnen.Text = "Gewonnen: " + Gewonnen.ToString();
            lbl_Verloren.Text = "Verloren: " + Verloren.ToString();
            lbl_Spiel_beendet.Text = "Spiele beendet: " + Spiel_beendet.ToString();
            lbl_GV.Text = "Gewonnen / Verloren: " + GV.ToString();
        }

        void Reset()
        {
            Aktuelles_Guthaben = 1000;
            Ratespiel.Properties.Settings.Default.Aktueller_Kontostand = Aktuelles_Guthaben;
            Ratespiel.Properties.Settings.Default.Save();

            Gewonnen = 0;
            Ratespiel.Properties.Settings.Default.Gewonnen = Gewonnen;
            Ratespiel.Properties.Settings.Default.Save();

            Verloren = 0;
            Ratespiel.Properties.Settings.Default.Verloren = Verloren;
            Ratespiel.Properties.Settings.Default.Save();

            Spiel_beendet = 0;
            Ratespiel.Properties.Settings.Default.Spiel_beendet = Spiel_beendet;
            Ratespiel.Properties.Settings.Default.Save();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Daten();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Möchtest du ALLE der gennanten Werte auf ihrer Standart werte zurücksetzten?\n" +
                "Dadurch wird auch das Guthaben auf " + string.Format("{0:c}", 1000) + " zurückgesetzt", "Hinweis", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Reset();
                Daten();
            }
        }

        private void Statistiken_Load(object sender, EventArgs e)
        {
            if (Ratespiel.Properties.Settings.Default.time_an)
                timer1.Enabled = true;
            else
                timer1.Enabled = false;

            Daten();
        }
    }
}
