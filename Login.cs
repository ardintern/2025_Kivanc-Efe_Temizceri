using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unidox
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            textBox1.Text = "51304154104";  // kullanıcı adı
            textBox2.Text = "ne9167";      // şifre



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            string username = textBox1.Text;
            string password = textBox2.Text;

            if (username == "51304154104" && password == "ne9167")
            {
                MessageBox.Show("Giriş başarılı!");

                
                Unidox.baslik.BaslikYardimci.Session.username = username;
                Unidox.baslik.BaslikYardimci.Session.password = password;




                
                this.Hide();
                ServiceForm form = new ServiceForm(); 
                form.Show();
                
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı veya şifre.");
            }




        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }






    }
}
