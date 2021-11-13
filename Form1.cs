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
using System.Security.Cryptography;

namespace DES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string key1;
        private void button1_Click(object sender, EventArgs e)
        {
            key1 = textBox1.Text;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string text = openFileDialog1.FileName;
                saveFileDialog1.Filter = "des files |*.des";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string shipr = saveFileDialog1.FileName;
                    EncryptedFile(text, shipr, key1);
                }

            }
        }
        private void EncryptedFile(string text, string shipr, string key1)
        {
            FileStream fsInput = new FileStream(text, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(shipr, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider Des = new DESCryptoServiceProvider();
            try
            {
                Des.Key = ASCIIEncoding.ASCII.GetBytes(key1);
                Des.IV = ASCIIEncoding.ASCII.GetBytes(key1);
                ICryptoTransform desencrypt = Des.CreateEncryptor();
                CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptostream.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка!!!");
                return;
            }
            fsInput.Close();
            fsEncrypted.Close();
        }

        private void DecryptedFile(string text, string shipr, string key1)
        {
            FileStream fsInput = new FileStream(text, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(shipr, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider Des = new DESCryptoServiceProvider();
            try
            {
                Des.Key = ASCIIEncoding.ASCII.GetBytes(key1);
                Des.IV = ASCIIEncoding.ASCII.GetBytes(key1);
                ICryptoTransform desencrypt = Des.CreateDecryptor();
                CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptostream.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка!!!");
                return;
            }
            fsInput.Close();
            fsEncrypted.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            key1 = textBox1.Text;
            openFileDialog1.Filter = "des files |*.des";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string text = openFileDialog1.FileName;
                saveFileDialog1.Filter = "txt files |*.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string shipr = saveFileDialog1.FileName;
                    DecryptedFile(text, shipr, key1);
                }

            }
        }
    }
}
