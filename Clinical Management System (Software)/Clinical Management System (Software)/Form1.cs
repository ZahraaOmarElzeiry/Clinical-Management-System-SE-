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

namespace Clinical_Management_System__Software_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = Clinical_Management_System__Software_.Properties.Resources.connectionString;
            SQL
        SqlConnection Con = new SqlConnection(connectionString);
            SqlCommand command = Con.CreateCommand();
            command.CommandText = "SELECT user_id FROM  [user] WHERE user_username = @username AND user_password = @password";
            command.Parameters.AddWithValue("@username", textBox1.Text);
            command.Parameters.AddWithValue("@password", textBox2.Text);

            Con.Open();
            var result = command.ExecuteScalar();
            Con.Close();

            if (result != null)
            { 
                // Authentication
                if (textBox1.Text ==  "admin")
                {
                    // Admin Panel
                    Hide();
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.ShowDialog();
                    Show();

                }
                else
                {
                    Con.Open();
                    command.CommandText = " SELECT account_id, account_type FROM account WHERE account_user_id=@user_id";
                    command.Parameters.AddWithValue("user_id", result.ToString());
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int account_id = reader.GetInt32(0);
                        int account_type = reader.GetInt32(1);

                        Con.Close();

                        if (account_type == 0)
                        {
                            //Secretary Panel
                            Hide();
                            SecretaryPanel secretaryPanel = new SecretaryPanel(account_id);
                            secretaryPanel.ShowDialog();
                            Show();
                        }
                        else if (account_type == 1)
                        {
                            //Doctor Panel
                            Hide();
                            DoctorPanel doctorPanel = new DoctorPanel(account_id);
                            doctorPanel.ShowDialog();
                            Show();
                        }
                    }


                }

            

            }
            else
            {
                // Aurhentication Error

                MessageBox.Show("Authentication Failed!");

            }


        }
    }
}
