using LangSync.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LangSync
{
    public partial class FormIntro : Form
    {
        public FormIntro()
        {
            InitializeComponent();
            this.CenterToScreen();
            Thread thread = new Thread(new ThreadStart(changeTitle));
            thread.Start();
        }

        private void UpdateLabel(string newText)
        {
            // Check if the label control needs to be invoked from a different thread
            if (label1.InvokeRequired)
            {
                // If it does, invoke the UpdateLabel method on the UI thread
                BeginInvoke(new Action<string>(UpdateLabel), newText);
            }
            else
            {
                // If it doesn't, update the label text directly
                label1.Text = newText;
            }
        }

        private void changeTitle()
        {
            List<string> list = new List<string> {
                "Lang-Synk",
                "Lang$ync",
                "LangSync-3",
                "L@ng-Sync",
                "L4ng-Sync",
                "LangSynchron1ze",
                "Lang-Sync+",
                "LangS^nc",
                "L4ng$ync",
                "LangSync123",
                "L∆ngSyncX",
                "Lang-Sync","Lang-Synk",
                "Lang$ync",
                "LangSync-3",
                "L@ng-Sync",
                "L4ng-Sync",
                "LangSynchron1ze",
                "Lang-Sync+",
                "LangS^nc",
                "L4ng$ync",
                "LangSync123",
                "L∆ngSyncX",
                "Lang-Sync"
            };

            foreach (string s in list)
            {
                //UpdateLabel(s);
                Thread.Sleep(150);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Instantiate and show the new form
            this.Hide();
            //FormDashboard dashboardForm = new FormDashboard();
            FormGrammar formPreGame = new FormGrammar();
            formPreGame.Closed += (s, args) => this.Close();
            formPreGame.Show();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
