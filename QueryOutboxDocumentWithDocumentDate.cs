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
    public partial class QueryOutboxDocumentWithDocumentDate : Form
    {


        DateTime startDate;
        DateTime endDate;
        string documentType;
        string queried ;
        string withXML = "XML";
        string minRecordId = "1";






        public QueryOutboxDocumentWithDocumentDate()
        {
            InitializeComponent();
        }

        private void QueryOutboxDocumentWithDocumentDate_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1";
            textBox2.Text = "ALL";
            textBox3.Text = "XML";
            textBox4.Text = "1";


            documentType = textBox1.Text;
            queried = textBox2.Text;
            withXML = textBox3.Text;
            minRecordId = textBox4.Text;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            startDate = dateTimePicker1.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            endDate = dateTimePicker2.Value; 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Unidox.baslik.BaslikYardimci.OutboxDocumentWithDocumentDate(startDate, endDate, documentType, queried, withXML, minRecordId);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FonkFormE_Fatura_Sorgu form = new FonkFormE_Fatura_Sorgu();
            form.Show();
        }
    }
}
