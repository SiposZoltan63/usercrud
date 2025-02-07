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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ControlBox = false;
            radioButton1.Checked = true;
            showReg();
            hideReg();
            feltolt();
            Update();
        }

        private Connect conn = new Connect();
        public static int userId = 0;
        private bool beleptet(string FirstName, string LastName, string pass)
        {
            try
            {
                conn.Connection.Open();

                string sql = $"SELECT `Id` FROM `data` WHERE `FirstName` = '{FirstName}' and `LastName` = '{LastName}' AND `Password` = '{pass}'";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                MySqlDataReader dr = cmd.ExecuteReader();

                bool van = dr.Read();

                conn.Connection.Close();
                return van;
            }
            catch (Exception)
            {
                return false;
            } 
        }

        private string regisztral(string FirstName, string LastName, string pass)
        {
            try
            {
                conn.Connection.Open();

                string sql = $"INSERT INTO `data`(`FirstName`, `LastName`, `Password`, `CreatedTime`, `UpdateTime`) VALUES ('{FirstName}','{LastName}','{pass}','{DateTime.Now.ToString("yyyy-MM-dd")}','{DateTime.Now.ToString("yyyy-MM-dd")}')";
                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                var result = cmd.ExecuteNonQuery();

                conn.Connection.Close();
                feltolt();

                return result > 0 ? "Sikeres regisztráció" : "Sikertelen Regisztráció";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true && textBox11 == textBox10)
                {
                    update(userId, textBox12.Text, textBox9.Text, textBox11.Text);
                    MessageBox.Show("Sikeres frissítés");
                    listBox1.Items.Clear();
                    feltolt();
                    hideReg();
                }
                else if (textBox11 == textBox10 && radioButton1.Checked == false)
                {
                    MessageBox.Show(regisztral(textBox9.Text, textBox12.Text, textBox11.Text));

                }
                if (textBox11.Text == textBox10.Text)
                {
                    MessageBox.Show(regisztral(textBox9.Text, textBox12.Text, textBox11.Text));
                    hideReg();
                }
                else
                {

                    MessageBox.Show("A két jelszó nem egyezik");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void regisztraltTag()
        {
            try
            {
                MessageBox.Show("Regisztrált tag.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void nemRegisztraltTag()
        {
            try
            {
                MessageBox.Show("Még nem regisztrált.");
                showReg();
                string[] darabol2 = textBox7.Text.Split(' ');
                textBox9.Text = darabol2[0];
                textBox12.Text = darabol2[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                string[] darabol = textBox7.Text.Split(' ');

                if (beleptet(darabol[0], darabol[1], textBox8.Text) == true)
                {
                    regisztraltTag();
                }
                else
                {
                    nemRegisztraltTag();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void feltolt()
        {
            try
            {
                conn.Connection.Open();

                string sql = $"SELECT `Id`,`LastName`,`FirstName`,`CreatedTime`,`UpdateTime` FROM `data`;";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                MySqlDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {
                    listBox1.Items.Add($"{dr.GetInt32(0)}. {dr.GetString(1)} {dr.GetString(2)} {dr.GetDateTime(3).ToString("yyyy-MM-dd")} {dr.GetDateTime(4).ToString("yyyy-MM-dd")}");
                }

                conn.Connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void hideReg()
        {
            try
            {
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;

                textBox9.Visible = false;
                textBox12.Visible = false;
                textBox11.Visible = false;
                textBox10.Visible = false;

                button3.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void showReg()
        {
            try
            {
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;

                textBox9.Visible = true;
                textBox12.Visible = true;
                textBox11.Visible = true;
                textBox10.Visible = true;

                button3.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void torol()
        {
            try
            {
                string id = listBox1.SelectedItem.ToString();
                string[] idDarabol = id.Split('.');

                conn.Connection.Open();

                string sql = $"DELETE FROM `data` WHERE `Id` = '{idDarabol[0]}'";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
                var result = cmd.ExecuteNonQuery();
                conn.Connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (radioButton2.Checked == true)
                {
                    torol();
                    listBox1.Items.Clear();
                    feltolt();
                }
                else
                {
                    showReg();
                    string[] darabol = listBox1.SelectedItem.ToString().Split(' ');
                    textBox12.Text = darabol[1];
                    textBox9.Text = darabol[2];
                    string[] darabol2 = listBox1.SelectedItem.ToString().Split('.');
                    userId = int.Parse(darabol2[0].TrimEnd());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void update(int id, string FirstName, string LastName, string Password)
        {
            try
            {
                conn.Connection.Open();

                string sql = $"UPDATE 'data' SET `FirstName`= '{FirstName}',`LastName`='{LastName}',`Password`='{Password}' WHERE `Id`= '{id}' ;";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

                var result = cmd.ExecuteNonQuery();

                conn.Connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}