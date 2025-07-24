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
    public partial class QueryOutboxDocument : Form
    {

        string paramType;
        string parameter;
        string withXML;


        public QueryOutboxDocument()
        {
            InitializeComponent();
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void QueryOutboxDocument_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Document_UUID";
            textBox2.Text = "e421fa2c-ef66-482d-b18d-ea27dd8e9ae9";
            textBox3.Text = "XML";

            paramType = textBox1.Text;
            parameter = textBox2.Text;
            withXML = textBox3.Text;



        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Unidox.baslik.BaslikYardimci.QueryOutboxDocument(paramType, parameter, withXML);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FonkFormE_Fatura_Sorgu form = new FonkFormE_Fatura_Sorgu();
            form.Show();
        }
    }
}
