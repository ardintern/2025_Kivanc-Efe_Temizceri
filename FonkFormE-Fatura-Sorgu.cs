using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Unidox
{
    public partial class FonkFormE_Fatura_Sorgu : Form
    {
        public FonkFormE_Fatura_Sorgu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
 


            this.Hide();
            QueryUsers form = new QueryUsers();
            form.Show();


        }

        private void button2_Click(object sender, EventArgs e)
        {

            string[] text = new string[] { "ABC"};



            string vkn = "0370368198";

            Unidox.baslik.BaslikYardimci.GetLastInvoiceIdAndDate(vkn, text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
   


            this.Hide();
            QueryOutboxDocument form = new  QueryOutboxDocument();
            form.Show();


        }

        private void button4_Click(object sender, EventArgs e)
        {

          

            this.Hide();
            QueryOutboxDocumentWithDocumentDate form = new QueryOutboxDocumentWithDocumentDate();
            form.Show();




        }

        private void button5_Click(object sender, EventArgs e)
        {


            string envelopeUUID = "20e670e8-422a-4a60-98f1-6ad791962ed7";

            int processState = 1;

            int processResult = 1;

            string documentUUID = "09c9f178-7c75-4c6d-9e0d-227a22a1a740";

            string sourceID = "";


            Unidox.baslik.BaslikYardimci.SorguEnvelope(envelopeUUID,processState,processResult,documentUUID,sourceID);

        }

        private void button6_Click(object sender, EventArgs e)
        {


            string startDate = "2025-07-18 17:39:43";
            string endDate = "2025-07-21 14:19:10";
            string documentType = "1";
            string queried = "NO";
            string withXML = "XML";
            string minRecordId = "35000";



            Unidox.baslik.BaslikYardimci.OutboxDocumentWithReceivedDate(startDate, endDate, documentType, queried, withXML, minRecordId);




        }

        private void button7_Click(object sender, EventArgs e)
        {


            string localId = "1";


            Unidox.baslik.BaslikYardimci.OutboxDocumentWithLocalId(localId);



        }

        private void button8_Click(object sender, EventArgs e)
        {


            List<string> guidList = new List<string>
            {


               "652b9876-9778-458f-85a0-f79637214a46",
               "e421fa2c-ef66-482d-b18d-ea27dd8e9ae9"


            };

            string documentType = "1";

            Unidox.baslik.BaslikYardimci.OutboxDocumentsWithGUIDList(guidList, documentType);

        }

        private void button9_Click(object sender, EventArgs e)
        {


            var paramType = "Document_UUID";
            var parameter = "09c9f178-7c75-4c6d-9e0d-227a22a1a740";
            var withXML = "XML";


            Unidox.baslik.BaslikYardimci.InboxDocument(paramType, parameter, withXML);


        }

        private void button10_Click(object sender, EventArgs e)
        {



            string startDate = "2025-05-18";
            string endDate = "2025-07-21";
            string documentType = "1";
            string queried = "ALL";
            string withXML = "XML";
            string takenFromEntegrator ="ALL";
            string minRecordId = "1";



            Unidox.baslik.BaslikYardimci.InboxDocumentWithDocumentDate(startDate, endDate, documentType, queried, withXML, takenFromEntegrator ,minRecordId);




        }

        private void button11_Click(object sender, EventArgs e)
        {




            string startDate = "2025-05-18 17:39:43";
            string endDate = "2025-06-21 14:19:10";
            string documentType = "1";
            string queried = "ALL";
            string withXML = "XML";
            string takenFromEntegrator = "ALL";
            string minRecordId = "1";



            Unidox.baslik.BaslikYardimci.InboxDocumentWithReceivedDate(startDate, endDate, documentType, queried, withXML, takenFromEntegrator, minRecordId);



        }





        private void button12_Click(object sender, EventArgs e)
        {



            List<string> guidList = new List<string>
            {


               "09c9f178-7c75-4c6d-9e0d-227a22a1a740",
               "e421fa2c-ef66-482d-b18d-ea27dd8e9ae9"


            };

            string documentType = "1";

            Unidox.baslik.BaslikYardimci.InboxDocumentsWithGUIDList(guidList, documentType);

        }




        private void button13_Click(object sender, EventArgs e)
        {

            Unidox.baslik.BaslikYardimci.getUserGBList();
        }




        private void button14_Click(object sender, EventArgs e)
        {
            Unidox.baslik.BaslikYardimci.getUserPKList();
       
        
        }

        private void button15_Click(object sender, EventArgs e)
        {


            string[] documentUUID = { "09c9f178-7c75-4c6d-9e0d-227a22a1a740", "e421fa2c-ef66-482d-b18d-ea27dd8e9ae9" };


            Unidox.baslik.BaslikYardimci.setTakenFromEntegrator(documentUUID);

        }

        private void button16_Click(object sender, EventArgs e)
        {

       

            this.Hide();
            QueryAppResponseOfOutboxDocument form = new QueryAppResponseOfOutboxDocument();
            form.Show();




        }

        private void button17_Click(object sender, EventArgs e)
        {


            //string docmentUUID = "09c9f178-7c75-4c6d-9e0d-227a22a1a740";
            //string withXML = "YES";

            //Unidox.baslik.BaslikYardimci.queryAppResponseOfInboxDocument(docmentUUID, withXML);


            this.Hide();
            QueryAppResponseOfInboxDocument form = new QueryAppResponseOfInboxDocument();
            form.Show();


        }

        private void button18_Click(object sender, EventArgs e)
        {

            this.Hide();
            ServiceForm form = new ServiceForm();
            form.Show();

        }

        private void FonkFormE_Fatura_Sorgu_Load(object sender, EventArgs e)
        {

        }
    }
}
