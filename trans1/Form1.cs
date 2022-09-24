using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trans1
{
    public partial class Form1 : Form
    {
        database class1 = new database();
        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataReader dr;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            database.Create_db();
            this.TopMost = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            ara();
            this.Text ="yavuz.akan@gmail.com";
            


        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        public const int WM_DRAWCLIPBOARD = 0x0308;

        private void Form1_Load(object sender, EventArgs e)
        {
            SetClipboardViewer(this.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != WM_DRAWCLIPBOARD)
                return;

            string veri = Clipboard.GetText();
            //Code To handle Clipboard change event
            String test = textBox1.Text;



            if (veri.Equals(test))
            { 
            
            }
            else

            {
               // textBox1.Text = veri;

                database.add(veri);
                cevir();
                ara();

            }
            
            

        }





        public void ara()
        {

            dataGridView1.Rows.Clear();
            string path = "deneme.db";
            string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";
            var con = new SQLiteConnection(cs);
            SQLiteDataReader dr;
            con.Open();

            //string stm = "select * FROM data ORDER BY id ASC  ";
            //SELECT * FROM (SELECT * FROM graphs WHERE sid=2 ORDER BY id DESC LIMIT 10) g ORDER BY g.id
            string stm = "select * FROM data ORDER BY id asc LIMIT 10  ";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dataGridView1.Rows.Insert(0, dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), dr.GetValue(2).ToString());

            }

            con.Close();

            this.dataGridView1.AllowUserToAddRows = false;


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;

            // dataGridView1.Columns[0].Visible = false;

            try
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[0];

                //textBox1.Text = dataGridViewRow.Cells["veri2"].Value.ToString();

                textBox1.Text =dataGridViewRow.Cells["veri"].Value.ToString() + Environment.NewLine + Environment.NewLine + dataGridViewRow.Cells["veri2"].Value.ToString();
            }
            catch
            { 
            
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];

                textBox1.Text =dataGridViewRow.Cells["veri"].Value.ToString() + Environment.NewLine +  Environment.NewLine +  dataGridViewRow.Cells["veri2"].Value.ToString();


            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];


                textBox1.Text =dataGridViewRow.Cells["veri"].Value.ToString() + Environment.NewLine +  Environment.NewLine +  dataGridViewRow.Cells["veri2"].Value.ToString();





            }
        }

        private void deleteOldDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Sure", "Delete All Notes", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
                string path = "deneme.db";
                string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";
                var con = new SQLiteConnection(cs);
                SQLiteDataReader dr;
                con.Open();

                //string stm = "select * FROM data ORDER BY id ASC  ";
                //SELECT * FROM (SELECT * FROM graphs WHERE sid=2 ORDER BY id DESC LIMIT 10) g ORDER BY g.id
                string stm = "delete from data";
                var cmd = new SQLiteCommand(stm, con);
                dr = cmd.ExecuteReader();

                stm = "delete from sqlite_sequence where name='data'";
                cmd = new SQLiteCommand(stm, con);
                dr = cmd.ExecuteReader();

                con.Close();

                this.dataGridView1.AllowUserToAddRows = false;


                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;

                // dataGridView1.Columns[0].Visible = false;

                textBox1.Text ="";
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }




        public void cevir()
        {
            string q = "";
            try
            {
                String komut = "transpy.exe";
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C " + komut;

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();




                while (!process.HasExited)
                {
                    q += process.StandardOutput.ReadToEnd();
                }



            }
            catch (Exception ex)
            {

                

            }
         


        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                // notifyIcon1.ShowBalloonTip(1000);
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }
    }
}
