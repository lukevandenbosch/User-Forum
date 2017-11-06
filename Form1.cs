using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void updateDatabase(SqlConnection cnn)
        {
            

            SqlCommand addUser;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sqlCode;

            sqlCode = "INSERT INTO LogInfo.Users  (UserName, UserPassword, FirstName, LastName, Email)" +
                        "VALUES ( '" + textBox2.Text + "', '" + textBox3.Text + "', '" +
                        textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "')";
            addUser = new SqlCommand(sqlCode, cnn);
            adapter.InsertCommand = new SqlCommand(sqlCode, cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            MessageBox.Show(textBox2.Text + "User added");

            addUser.Dispose();
        }

        private bool passCondition()
        {
            if (!textBox3.Text.Any(char.IsDigit))
            {
                return false;
            }
            if (!textBox3.Text.Any(char.IsLower))
            {
                return false;
            }
            if (!textBox3.Text.Any(char.IsUpper))
            {
                return false;
            }
            return true;
        }
        private bool emailCondition(SqlConnection cnn)
        {
           

            if (textBox7.Text.Contains("@") && textBox7.Text.Contains("."))
            {
                string[] separators = { "@" };
                string[] arrayEmail = textBox7.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (arrayEmail.Length == 2 && arrayEmail[1].Contains("."))
                {
                    return true;
                }
            }
            return false;
        }

        private bool UserExistCondition()
        {
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectString;
            SqlConnection cnn;
            connectString = @"Data Source=LAPTOP-B1078L3L\LUKESSERVER;
                                Initial Catalog=LoginDatabase; 
                                Trusted_Connection=Yes";
            cnn = new SqlConnection(connectString);
            cnn.Open();
            if (textBox3.Text == textBox4.Text)
            {
                if (passCondition())
                {
                    if (emailCondition(cnn))
                    {
                        if (UserExistCondition())
                        {
                            updateDatabase(cnn);
                        }
                        else
                        {
                            MessageBox.Show("UserName already exists");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email is Invalid or already exists");
                    }
                }
                else
                {
                    MessageBox.Show("Password must contain number, uppercase, and lowercase Character");
                }
            }
            else
            {
                MessageBox.Show("Passwords Don't Match");
            }
            cnn.Close();
        }
    }
}
