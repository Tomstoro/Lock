using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Lock
{
    public partial class Form1 : Form
    {
        int[] progresso = new int[4];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Dati dati1 = new Dati()
            {
                groupbox = groupBox1,
                textbox = textBox1,
                pb = progressBar1,
                indice = 0,
                progresso_bgw = progresso
            };

            Dati dati2 = new Dati()
            {
                groupbox = groupBox2,
                textbox = textBox2,
                pb = progressBar2,
                indice = 1,
                progresso_bgw = progresso
            };

            Dati dati3 = new Dati()
            {
                groupbox = groupBox3,
                textbox = textBox3,
                pb = progressBar3,
                indice = 2,
                progresso_bgw = progresso
            };

            Dati dati4 = new Dati()
            {
                groupbox = groupBox4,
                textbox = textBox4,
                pb = progressBar4,
                indice = 3,
                progresso_bgw = progresso
            };
            int j = 0;
            for (int i = 0; i < 4; i++)
            {
                progresso[i] = j;
                j++;
            }

            backgroundWorker1.RunWorkerAsync(dati1);
            backgroundWorker2.RunWorkerAsync(dati2);
            backgroundWorker3.RunWorkerAsync(dati3);
            backgroundWorker4.RunWorkerAsync(dati4);
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            Dati d = (Dati)e.Argument;
            d.pb.Value = 0;
            BackgroundWorker bgw = (BackgroundWorker)sender;
            do
            {
                lock (progresso)
                {
                    if (d.pb.Value != d.pb.Maximum)
                    {
                        Thread.Sleep(500);
                        bgw.ReportProgress(progresso[d.indice], d);
                        Thread.Sleep(500);
                    }
                }
            } while (d.pb.Value!=d.pb.Maximum);
           
        }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Dati d = (Dati)e.UserState;
            d.pb.Value += 1;
            d.textbox.Text = d.pb.Value.ToString();
        }
    }
}
