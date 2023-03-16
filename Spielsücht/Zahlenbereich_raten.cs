using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Spielsücht
{
    public partial class Zahlenbereich_raten : Form
    {
        public Zahlenbereich_raten()
        {
            InitializeComponent();
        }

        double Aktuelles_Guthaben = Properties.Settings.Default.Aktueller_Kontostand;
        int Auswahl;    //1 = 1 - 007, 2 = 8 - 10, 3 = 11 - 15, 3 = 16 - 20
        Random zufall = new Random();
        int Maximal_Random = 21;
        int Einsatz = 0;
        int Zufallszahl_zusatzgewinn = 0;
        double Verzinsung = 0.13;
        double zusatzgewinn;
        int Zaeler_Gewinn = 0;
        int Highscore_Nr = Properties.Settings.Default.score_Nr;
        string csvDateiName;
        string AppDataPfad;
        FileInfo csvDateiInfo;
        string Windows_Version = Klassen.Windows_Version.GetWindwosClientVersion();

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Auswahl = 1;
            if (radioButton1.Checked)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Auswahl = 2;
            if (radioButton2.Checked)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Auswahl = 3;
            if (radioButton3.Checked)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Auswahl = 4;
            if (radioButton4.Checked)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            csvDateiName = "Highscore_Spiel3.csv";
            if (Windows_Version == "Windows XP")
                AppDataPfad = Application.UserAppDataPath;
            else
                AppDataPfad = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\Spielsücht\Spielsücht";
            csvDateiInfo = new FileInfo(AppDataPfad + @"\" + csvDateiName);

            int Zufalls_zahl = zufall.Next(1, Maximal_Random);
            bool Ergebnis = Klassen.Bereich_raten.Double_o_seven(Zufalls_zahl, Auswahl);
            string Einsatz_Text = txtBox_Einsatz.Text;
            txtBox_Einsatz.Text = Einsatz_Text.Replace("-", "");

            try
            {
                Einsatz = Convert.ToInt32(txtBox_Einsatz.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Bitte keinen text eingeben.\n" + "Ebendfalls bitte das maximum: " + string.Format("{0:0,0}", Int32.MaxValue) + " beachten.");
                button1.Enabled = false;
                return;
            }

            lbl_Ergebnis.Text = "Letztes Ergebnis: " + Zufalls_zahl;

            if (Ergebnis == true)
            {
                Zufallszahl_zusatzgewinn = Convert.ToInt32(Math.Round(zufall.NextDouble(), 2));
                Properties.Settings.Default.Gewonnen++;
                Properties.Settings.Default.Save();
                Zaeler_Gewinn++;
                if (Zufallszahl_zusatzgewinn == 0.2 || Zufallszahl_zusatzgewinn == 0.8)
                {
                    MessageBox.Show("Gewonnen. Du erhälst sogar einen kleinen Bonus in höhe von " + string.Format("{0:0%}", Zufallszahl_zusatzgewinn) + "\nvon deinem eingesetzten Wert.");
                    zusatzgewinn = Zufallszahl_zusatzgewinn * Einsatz;
                    Aktuelles_Guthaben = Aktuelles_Guthaben + zusatzgewinn;
                }
                else
                {
                    MessageBox.Show("Gewonnen. Aber leider keinen Zusatzgewinn erhalten.\nDie Zufallszahl war: " + Zufallszahl_zusatzgewinn.ToString());
                    Aktuelles_Guthaben = Aktuelles_Guthaben + Einsatz;
                }
            }
            else
            {
                if (Aktuelles_Guthaben < 0)
                {
                    Int32 Verzinster_Einsatz = Convert.ToInt32(Einsatz * Verzinsung + Einsatz);
                    if (Aktuelles_Guthaben <= -100)
                    {
                        Aktuelles_Guthaben = Aktuelles_Guthaben - Verzinster_Einsatz;
                        MessageBox.Show("Du hast das komplette Spiel verloren.\nDein Guthaben beträgt: " + string.Format("{0:c}", Aktuelles_Guthaben) + "\nEin neues Spiel wird gestartet.");
                        Aktuelles_Guthaben = 1000;
                        Properties.Settings.Default.Spiel_beendet++;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Aktuelles_Guthaben = Aktuelles_Guthaben - Verzinster_Einsatz;
                        MessageBox.Show(string.Format("{0:c}", Verzinster_Einsatz) + " Wurde ihnen durch Schulden und Zinsen abgezogen.\nDie Zinsen betragen einen Wert von: " + string.Format("{0:c}", Einsatz * Verzinsung));
                    }
                }
                else
                {
                    MessageBox.Show("Verloren");
                    Aktuelles_Guthaben = Aktuelles_Guthaben - Einsatz;
                    Properties.Settings.Default.Verloren++;
                    Properties.Settings.Default.Save();
                }
                
                string lines = Highscore_Nr + ";" + Zaeler_Gewinn + ";" + Properties.Settings.Default.Name + ";" + DateTime.Now.ToString();

                using (StreamWriter file = new StreamWriter(csvDateiInfo.FullName, true))
                {
                    file.WriteLine(lines);
                }
                Zaeler_Gewinn = 0;
            }

            Properties.Settings.Default.Aktueller_Kontostand = Aktuelles_Guthaben;
            Properties.Settings.Default.Save();
            lbl_Guthaben.Text = "Guthaben: " + string.Format("{0:c}", Aktuelles_Guthaben);
        }

        private void TxtBox_Einsatz_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_Einsatz.TextLength > 0)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void Zahlenbereich_raten_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.time_an)
                timer1.Enabled = true;
            else
                timer1.Enabled = false;

            lbl_Guthaben.Text = "Guthaben: " + string.Format("{0:c}", Aktuelles_Guthaben);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Aktuelles_Guthaben = Properties.Settings.Default.Aktueller_Kontostand;
            lbl_Guthaben.Text = "Guthaben: " + string.Format("{0:c}", Aktuelles_Guthaben);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string All_in = Aktuelles_Guthaben.ToString();
            txtBox_Einsatz.Text = All_in.Replace("-", "");
        }
    }
}
