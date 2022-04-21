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
namespace list_phone
{
    public partial class Form2 : Form
    {
        const string path = "list.txt";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var printer = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            for (int i=0; i< printer.Count;i++)
            {
                listBox1.Items.Add(printer[i].Trim());
                listBox2.Items.Add(printer[i].Trim());
               
            }
            if (File.Exists(path))
            {
                string[] all = File.ReadAllLines(path);
                listBox1.SelectedItem = all[0];
                listBox2.SelectedItem = all[1];
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] all = File.ReadAllLines(path);
            all[0] = listBox1.SelectedItem.ToString();
            all[1] = listBox2.SelectedItem.ToString();
            File.Delete(path);
            File.WriteAllLines(path, all);
            this.Close();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.SelectedItem.ToString() == listBox1.SelectedItem.ToString())
                {
                    MessageBox.Show(@"This Printer is selected"+ Environment.NewLine + "please select another printer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    try
                    {
                        listBox1.SelectedIndex += 1;
                    }
                    catch
                    {
                        listBox1.SelectedIndex = 0;
                    }

                }
            }
            catch
            {
                
            }

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem.ToString() == listBox2.SelectedItem.ToString())
            {
                MessageBox.Show(@"This Printer is selected" + Environment.NewLine + "please select another printer", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                try
                {
                    listBox2.SelectedIndex += 1;
                }
                catch
                {
                    listBox2.SelectedIndex = 0;
                }
                
            }
        }
    }
}
