﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace g_l_i_t_c_h___e_r
{
    public partial class Form1 : Form
    {

        string choosenFile;
        public Form1()
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(w_e_l_c_o_m_eH_u_man));
            t.Start();
            System.Threading.Thread.Sleep(2000);
            InitializeComponent();
            t.Abort();
            
        }

        public void w_e_l_c_o_m_eH_u_man()
        {
            Application.Run(new Ethereal_Splash());
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            pb_bg.Size = Size;
            pb_bg.Location = new Point(-1, -1);
            pb_bg.SendToBack();
            CenterToScreen();
            button2.Visible = false;
            richTextBox1.Visible = false;
            label2.BackColor = label1.BackColor = Color.LightPink;
            label2.ForeColor = label1.ForeColor = Color.Teal;
            Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            
            label2.Text = "Loading the file...";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                choosenFile = openFileDialog1.FileName;
                DialogResult _dg = MessageBox.Show(
                "Ａｌｓｏ　ａｐｐｅｎｄ　ｔｏ　ｔｅｘｔｂｏｘ？　ス現ぅ加",
                "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

                if (_dg == DialogResult.Yes)
                {
                    richTextBox1.Visible = true;
                    using (var file = File.Open(openFileDialog1.FileName, FileMode.Open))
                    {
                        int b;
                        int countBytes = 0;
                        int countLines = 0;
                        StringBuilder builtString = new StringBuilder();
                        while ((b = file.ReadByte()) >= 0)
                        {
                            countLines++;
                            label1.SuspendLayout();
                            label1.Text = "Lines:" + countLines;
                            label1.ResumeLayout();

                            string s = b.ToString("X");
                            if (s.Length < 2)
                                s = "0" + s;
                            builtString.Append(s + " ");
                            countBytes++;
                            if (countBytes == 15)
                            {
                                builtString.AppendLine();
                                countBytes = 0;
                            }


                        }
                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                        richTextBox1.SelectedText = builtString.ToString();
                        file.Close();

                    }
                } else
                {
                    label1.Visible = false;
                    richTextBox1.Visible = true;
                }
                // richTextBox1.Text = String.Join("", Array.ConvertAll(x, byteValue => byteValue.ToString()));

                label2.Text = "Finished...";
                button2.Visible = true;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DirectoryInfo info = Directory.GetParent(choosenFile);
            string parent = info.ToString()+@"\exported_1.bmp";
            ByteArrayToFile(parent);
        }

        private void ByteArrayToFile(string fileName)
        {
            string strPath = Path.GetFullPath(choosenFile);
            BinaryReader binReader = new BinaryReader(File.Open(strPath, FileMode.Open));

            long lfileLength = binReader.BaseStream.Length; // init the buffer
            byte[] btFile = new byte[lfileLength]; //create the damn array

            for (long lIdx = 0; lIdx < lfileLength; lIdx++)
            {
                btFile[lIdx] = binReader.ReadByte();
            }
           // MessageBox.Show("dsds "+fileName + " "+btFile.Length );
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(btFile, 0, btFile.Length); //append dat shit and create the image
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ ex);
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
