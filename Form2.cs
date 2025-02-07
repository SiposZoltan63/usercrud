using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Org.BouncyCastle.Bcpg;
using MySqlX.XDevAPI.Common;

namespace usercrud
{
    public partial class Form2 : Form
    {
        private Form1 form1 { get; set; }

        public Form2()
        {
            InitializeComponent();
            ControlBox = false;
            Form1 form1 = new Form1();
            form1.Controls.Add(form1);
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}