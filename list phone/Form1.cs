using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Printing;

namespace list_phone
{
    public partial class Form1 : Form
    {
        const string path = "list.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo c = new("Fa-IR");
            Application.CurrentInputLanguage = InputLanguage.FromCulture(c);
            if (File.Exists(path))
            {
                string[] list = File.ReadAllLines(path);
                for (int i = 2; i < list.Length; i++)
                {
                    listBox1.Items.Add(list[i]);
                }
                radialtxt();
                
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.Enabled == false)
            {
                listBox1.Items[listBox1.SelectedIndex]= textBox1.Text;
                string[] list = new string[listBox1.Items.Count];
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    list[i] = listBox1.Items[i].ToString();
                }
                Save(list);
                listBox1.Enabled = true;
                textBox1.Text = "";
                return;
            }
            if (textBox1.Text.Trim()=="")
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "Erorr:text note is empty!!!";
            }
            else
            {
                listBox1.Items.Add(textBox1.Text);
                File.AppendAllText(path, textBox1.Text + Environment.NewLine, Encoding.UTF8);
                textBox1.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(toolStripStatusLabel1.Text !="")
            {
                toolStripStatusLabel1.Text = "Everything is correct !!!";
                toolStripStatusLabel1.ForeColor = Color.Black;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new();
            Form1 f1 = new();
            f2.ShowDialog(this);
            radialtxt();
        }
        void radialtxt()
        {
            string[] all = File.ReadAllLines(path);
            radioButton1.Text = all[0];
            radioButton2.Text = all[1];
            PrinterSettings ps = new();

            if (ps.PrinterName == radioButton1.Text)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            if (radioButton1.Checked)
            {
                MyPrinters.SetDefaultPrinter(radioButton1.Text);
            }
            else
            {
                MyPrinters.SetDefaultPrinter(radioButton2.Text);
            }
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
            MyPrinters.SetDefaultPrinter(radioButton1.Text);

        }
        public static class MyPrinters
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Name);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MyPrinters.SetDefaultPrinter(radioButton2.Text);

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void editeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;

            }
            listBox1.Enabled = false;
            textBox1.Text = listBox1.SelectedItem.ToString();
            textBox1.Focus();

            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;
                
            }
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            string[] list =new string[listBox1.Items.Count] ;
            for (int i=0; i<listBox1.Items.Count;i++)
            {
                list[i]= listBox1.Items[i].ToString();
            }
            Save(list);
        }
        void Save(string [] listItem)
        {
            string[] list = new string[listItem.Length+2];
            list[0] = radioButton1.Text;
            list[1] = radioButton2.Text;
           for(int i = 2; i < list.Length; i++)
            {
                list[i] = listItem[i - 2];
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllLines(path, list);
        }
    }
}
