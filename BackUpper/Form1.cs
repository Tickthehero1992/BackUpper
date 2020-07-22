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
using System.IO;

namespace BackUpper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBox2.KeyPress += textBox2_KeyPress;
            textBox2.Text = TimeToApp.ToString();
        }


        private string folderNameIn="";
        private string folderNameOut;
        int TimeToApp=10;
        private void выбратьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            folderBrowserDialog1.ShowNewFolderButton = false;
            if (result == DialogResult.OK)
            {
                folderNameIn=folderBrowserDialog1.SelectedPath;
                MessageBox.Show(folderNameIn,"Папка с данными сохранений");
                textBox1.ForeColor = Color.Black;
                textBox1.Text = folderNameIn;
                textBox1.Enabled = false;
                textBox1.BackColor = Color.LightGray;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void инфоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выберите папку. В строке время, укажите вреся в минутах для Бэкапа");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderNameIn=="")
            {
                MessageBox.Show("Выберите папку пути");
                return;
            }
            TimeToApp = Int32.Parse(textBox2.Text);
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string[] nameDir = folderNameIn.Split('\\');
                    
                folderNameOut = folderBrowserDialog1.SelectedPath+"\\BackApps\\" +nameDir.Last();
                MessageBox.Show(folderNameOut);
                DirectoryInfo dirInfo = new DirectoryInfo(folderNameOut);
                if (!dirInfo.Exists)
                {
                   Directory.CreateDirectory(folderNameOut);
                    
                }
                CopyDir(folderNameIn, folderNameOut, true);
                MessageBox.Show("Начинаю сохранение бэкаппов в вашу папку", "Startanul");
                textBox2.Enabled = false;
                textBox2.BackColor = Color.Gray;
                button1.Enabled = false;
                button1.BackColor = Color.Gray;
                CopiingTimer();
            }
        }
        private static System.Windows.Forms.Timer aTimer = new System.Windows.Forms.Timer();
        public void CopiingTimer()
        {
            aTimer.Stop();
            aTimer.Enabled = false;
            aTimer.Interval = TimeToApp*60000;
            aTimer.Tick += new EventHandler(Cooping);
            aTimer.Enabled = true;
        }
        private void Cooping(object ob, EventArgs e)
        {
            CopyDir(folderNameIn,folderNameOut,true);

        }
        private void CopyDir(string pathIn, string pathOut,bool Subdir)
        {
            DirectoryInfo dir = new DirectoryInfo(pathIn);
            FileInfo[] files = dir.GetFiles();
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(pathOut))
            {
                Directory.CreateDirectory(pathOut);
            }

            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(pathOut, file.Name);
                file.CopyTo(temppath, true);
            }
            if (Subdir)
            {
                foreach (DirectoryInfo d in dirs)
                {
                    string temppath = Path.Combine(pathOut, d.Name);
                    CopyDir(d.FullName, temppath, Subdir);
                }
            }
        }

        private void ввестиПапкуВручнуюToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderNameIn = textBox1.Text;
            if (textBox1.Text.Length == 0)
            {
                return;
            }
            DirectoryInfo dir = new DirectoryInfo(folderNameIn);
            if (dir.Exists)
            {
                MessageBox.Show(folderNameIn, "Отсюда берем");
                textBox1.Enabled = false;
                textBox1.BackColor = Color.LightGray;
            }
            else 
            {
                MessageBox.Show("Некорректный путь", "Ошибка");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            aTimer.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox1.BackColor = Color.White ;
            textBox2.BackColor = Color.White;
            button1.Enabled = true;
            button1.BackColor=System.Drawing.SystemColors.ActiveCaption;
        }
    }
    
}
