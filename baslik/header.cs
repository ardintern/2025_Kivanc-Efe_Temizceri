using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using Unidox.ServiceReference1; 
using Unidox.ServiceReference2;

namespace Unidox.baslik
{
    public static class BaslikYardimci
    {
        public class Session
        {
            public static string username { get; set; }
            public static string password { get; set; }
            public static bool IsAuthenticated { get; set; }
            public static string Document_UUID { get; set; }
           
        }







        public static UserAddresInfo[] Alias(string[] tckn)

        {

            var client = new InvoiceWSClient();



            using (var scope = new OperationContextScope(client.InnerChannel))
            {



                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                var bilgi = client.getUserAliases(tckn);



                return bilgi;

            }


        }










        public static EntResponse ControlInvoiceXML(string invoiceXML)

        {

            var client = new InvoiceWSClient();



            using (var scope = new OperationContextScope(client.InnerChannel))
            {



                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                var text = client.controlInvoiceXML(invoiceXML);

                return text;

            }


        }








        public static ServiceReference1.DocumentQueryResponse GetPrefixList()

        {

            var client = new InvoiceWSClient();



            using (var scope = new OperationContextScope(client.InnerChannel))
            {



                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                var prefix = client.getPrefixList();

                return prefix;

            }


        }















        public static ServiceReference1.UserQueryResponse GbList()

        {

            var client = new InvoiceWSClient();



            using (var scope = new OperationContextScope(client.InnerChannel))
            {



                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                var gb = client.getCustomerGBList();

                return gb;

            }


        }







        public static ServiceReference1.UserQueryResponse PkList()

        {

            var client = new InvoiceWSClient();



            using (var scope = new OperationContextScope(client.InnerChannel))
            {



                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                var pk = client.getCustomerPKList();

                return pk;

            }


        }










        public static ServiceReference1.CreditInfo Info(string tckn)

        {

            var client = new InvoiceWSClient();



            using (var scope = new OperationContextScope(client.InnerChannel))
            {

               

                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                var kalankredi = client.getCustomerCreditCount(tckn);


                


                return kalankredi;

            }
            

        }









        public static ServiceReference1.CreditInfo CreditSpace(string tckn)

        {

            var client = new InvoiceWSClient();



            using (var scope = new OperationContextScope(client.InnerChannel))
            {



                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                var kalankredi = client.getCustomerCreditSpace(tckn);





                return kalankredi;

            }


        }






        public static void GonderXML(string xmlIcerik)
        {
            var faturaServisi = new InvoiceWSClient();

            using (var scope = new OperationContextScope(faturaServisi.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                string newUUID;
                string newID;


                
                string rawXml = xmlIcerik; ;

                
                newUUID = Guid.NewGuid().ToString();
                newID = "KET" + DateTime.Now.ToString("yyyyMMddHH") + new Random().Next(100, 999).ToString(); // KET ile başlayanlar benim

                
                rawXml = Regex.Replace(rawXml, @"<cbc:UUID>.*?</cbc:UUID>", $"<cbc:UUID>{newUUID}</cbc:UUID>", RegexOptions.IgnoreCase);

                
                rawXml = Regex.Replace(rawXml, @"<cbc:ID>.*?</cbc:ID>", $"<cbc:ID>{newID}</cbc:ID>", RegexOptions.IgnoreCase);

                
                rawXml = Regex.Replace(rawXml, @"<cbc:ID>.*?</cbc:ID>(\s*)</cac:AdditionalDocumentReference>", $"<cbc:ID>{newUUID}</cbc:ID>$1</cac:AdditionalDocumentReference>", RegexOptions.IgnoreCase);

                
                var document = new InputDocument[]
                {
        new InputDocument
        {
            documentDate = DateTime.Now.ToString("yyyy-MM-dd"),
            documentUUID = newUUID,
            documentId = newID,
            sourceUrn = "urn:mail:defaultgb@univera.com.tr",
            destinationUrn = "urn:mail:defaultpkhotmail@hotmail.com",
            localId = "1",
            documentNoPrefix = "EEE",
            submitForApproval = true,
            xmlContent = rawXml,
            note = "aa"
        }
                };

                
                try
                {
                    var yanit = faturaServisi.sendInvoice(document);
                    string mesaj = $"Kod: {yanit[0].code}\nAçıklama: {yanit[0].explanation}";
                    if (!string.IsNullOrEmpty(yanit[0].cause))
                        mesaj += $"\nHata: {yanit[0].cause}";
                    MessageBox.Show(mesaj, "Fatura Gönderim Yanıtı");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gönderim sırasında hata oluştu:\n" + ex.Message, "Hata");
                }
            }
        }





        //*****************************************************_Yeni Servis_**************************************************************************








        public static void QueryUsers(DateTime startDate, DateTime finishDate, string vknTckn)
        {
            var client = new QueryDocumentWSClient(); 

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.QueryUsers(
                        startDate.ToString("yyyy-MM-dd"),
                        finishDate.ToString("yyyy-MM-dd"),
                        vknTckn
                    );

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Kullanıcı Sayısı: {yanit.userCount}");

                    if (yanit?.users != null)
                    {
                        foreach (var user in yanit.users)
                        {
                            sb.AppendLine($"VKN/TCKN: {user.vkn_tckn}");
                            sb.AppendLine($"Unvan: {user.unvan_ad}");
                            sb.AppendLine($"Etiket: {user.etiket}");
                            sb.AppendLine($"Tip: {user.tip}");
                            sb.AppendLine($"İlk Kayıt Zamanı: {user.ilkKayitZamani}");
                            sb.AppendLine($"Etiket Kayıt Türü: {user.etiketKayitTuru}");
                            sb.AppendLine(new string('-', 60));
                        }
                    }
                    else
                    {
                        sb.AppendLine("Kullanıcı listesi boş veya null döndü.");
                    }

                    MessageBox.Show(sb.ToString(), "QueryUsers Yanıtı");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("QueryUsers sorgusunda hata:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }






        public static void GetLastInvoiceIdAndDate(string vknTckn, string[] documentIdPrefix)
        {
            var sorguServisi = new QueryDocumentWSClient(); 

            using (var scope = new OperationContextScope(sorguServisi.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = sorguServisi.GetLastInvoiceIdAndDate(vknTckn,documentIdPrefix);

                    string mesaj = $"Durum: {yanit.queryState} - {yanit.stateExplanation}\nDoküman Sayısı: {yanit.documentsCount}\nMax Record: {yanit.maxRecordIdinList}";
                    MessageBox.Show(mesaj, "Son Fatura Bilgisi");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("GetLastInvoiceIdAndDate hatası:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }












        public static void QueryOutboxDocument(string paramType, string parameter, string withXml)
        {
            var sorguServisi = new QueryDocumentWSClient(); 

            using (var scope = new OperationContextScope(sorguServisi.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();
               
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);
                
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = sorguServisi.QueryOutboxDocument(paramType, parameter, withXml);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Doküman Sayısı: {yanit.documentsCount}");
                    sb.AppendLine(new string('-', 50));

                    foreach (var doc in yanit.documents)
                    {
                        sb.AppendLine($"UUID: {doc.document_uuid}");
                        sb.AppendLine($"ID: {doc.document_id}");
                        sb.AppendLine($"Envelope UUID: {doc.envelope_uuid}");
                        sb.AppendLine($"Document Profile: {doc.document_profile}");
                        sb.AppendLine($"Oluşturulma Zamanı: {doc.system_creation_time}");
                        sb.AppendLine($"Tarih: {doc.document_issue_date}");
                        sb.AppendLine($"Source ID: {doc.source_id}");
                        sb.AppendLine($"Destination ID: {doc.destination_id}");
                        sb.AppendLine($"Source Urn: {doc.source_urn}");
                        sb.AppendLine($"Source Title: {doc.source_title}");
                        sb.AppendLine($"Destination Urn: {doc.destination_urn}");
                        sb.AppendLine($"Para Birimi: {doc.currency_code}");
                        sb.AppendLine($"Ödenecek Tutar: {doc.invoice_total}");
                        sb.AppendLine($"Durum Kodu: {doc.state_code}");
                        sb.AppendLine($"Durum Açıklaması: {doc.state_explanation}");
                        sb.AppendLine($"Belge Tipi: {doc.content_type}");



                        if (withXml != "NONE" && doc.document_content != null)
                        {
                            //string xml = Encoding.UTF8.GetString(doc.document_content);
                            //sb.AppendLine("Fatura içeriği (XML):");                               ------> Tüm XML içeriğini istiyorsanız
                            //sb.AppendLine(xml);

                            string contentPreview = Encoding.UTF8.GetString(doc.document_content);
                            sb.AppendLine($"İçerik (ilk 100 karakter): {contentPreview.Substring(0, Math.Min(100, contentPreview.Length))}");

                        }


                        sb.AppendLine($"Doküman Tipi: {doc.document_type_code}");


                        sb.AppendLine($"Vergi Hariç Tutar: {doc.taxExlusiveAmount}");
                        sb.AppendLine($"Vergi Dahil Tutar: {doc.taxInclusiveAmount}");
                        sb.AppendLine($"Toplam İskonto: {doc.allowanceTotalAmount}");
                        sb.AppendLine($"Mal Hizmet Toplam Tutar: {doc.lineExtensionAmount}");
                        sb.AppendLine($"Alıcı: {doc.customerPersonName}");





                        sb.AppendLine(new string('-', 50));
                    }

                    MessageBox.Show(sb.ToString(), "Giden Fatura Sorgu Sonucu");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("QueryOutboxDocument hatası:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }









        public static void OutboxDocumentWithDocumentDate(
    DateTime startDate, DateTime endDate, string documentType,
    string queried, string withXML, string minRecordId)
        {
            var client = new QueryDocumentWSClient();

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();

                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.QueryOutboxDocumentsWithDocumentDate(
                        startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), documentType, queried, withXML, minRecordId);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Dönen belge sayısı: {yanit.documentsCount}");

                    if (yanit.documents != null)
                    {
                        foreach (var doc in yanit.documents)
                        {
                            sb.AppendLine($"UUID: {doc.document_uuid}");
                            sb.AppendLine($"ID: {doc.document_id}");;
                            sb.AppendLine($"Tarih: {doc.document_issue_date}");
                            sb.AppendLine($"Source ID: {doc.source_id}");
                            sb.AppendLine($"Fatura Türü: {doc.document_profile}");
                            sb.AppendLine($"Kod: {doc.state_code}");
                            sb.AppendLine($"Fatura Durumu: {doc.state_explanation}");
                            sb.AppendLine($"Yanıt: {doc.response_code}");





                            if (withXML != "NONE" && doc.document_content != null)
                            {
                                //string xml = Encoding.UTF8.GetString(doc.document_content);
                                //sb.AppendLine("Fatura içeriği (XML):");
                                //sb.AppendLine(xml);


                            }





                            sb.AppendLine(new string('-', 100));
                        }
                    }

                    string path = Path.Combine(Path.GetTempPath(), "SorguSonucu.txt");
                    File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

                    Process.Start("notepad.exe", path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorgu sırasında hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }












        public static void SorguEnvelope(string envelopeUUID, int processState, int processResult, string documentUuid, string sourceId)
        {
            var sorguServisi = new QueryDocumentWSClient(); 

            using (var scope = new OperationContextScope(sorguServisi.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = sorguServisi.QueryEnvelope(envelopeUUID, processState,processResult,documentUuid, sourceId);

                    string mesaj = $"Durum: {yanit.queryState} - {yanit.stateExplanation}\nDoküman Sayısı: {yanit.documentsCount}\nMax Record: {yanit.maxRecordIdinList}";
                    MessageBox.Show(mesaj, "Son Fatura Bilgisi");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("GetLastInvoiceIdAndDate hatası:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }












            public static void OutboxDocumentWithReceivedDate(
             string startDate, string endDate, string documentType,
             string queried, string withXML,string minRecordId)
        {
            var client = new QueryDocumentWSClient();

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();

                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.QueryOutboxDocumentsWithReceivedDate(
                        startDate, endDate, documentType, queried, withXML, minRecordId);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Dönen belge sayısı: {yanit.documentsCount}");

                    if (yanit.documents != null)
                    {
                        foreach (var doc in yanit.documents)
                        {
                            sb.AppendLine($"UUID: {doc.document_uuid}");
                            sb.AppendLine($"ID: {doc.document_id}"); ;
                            sb.AppendLine($"Tarih: {doc.document_issue_date}");
                            sb.AppendLine($"Source ID: {doc.source_id}");
                            sb.AppendLine($"Fatura Türü: {doc.document_profile}");
                            sb.AppendLine($"Kod: {doc.state_code}");
                            sb.AppendLine($"Fatura Durumu: {doc.state_explanation}");




                            if (withXML != "NONE" && doc.document_content != null)
                            {
                                //string xml = Encoding.UTF8.GetString(doc.document_content);
                                //sb.AppendLine("Fatura içeriği (XML):");
                                //sb.AppendLine(xml);


                            }





                            sb.AppendLine(new string('-', 100));
                        }
                    }

                    string path = Path.Combine(Path.GetTempPath(), "SorguSonucu.txt");
                    File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

                    Process.Start("notepad.exe", path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorgu sırasında hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }










        public static void OutboxDocumentWithLocalId(string localId)
        {
            var client = new QueryDocumentWSClient();

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();

                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.QueryOutboxDocumentWithLocalId(localId);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Dönen belge sayısı: {yanit.documentsCount}");
                    


                    if (yanit.documents != null)
                    {
                        foreach (var doc in yanit.documents)
                        {

                            sb.AppendLine($"Durum Açıklaması: {doc.state_explanation}");
                            sb.AppendLine($"UUID: {doc.document_uuid}");
                            sb.AppendLine($"ID: {doc.document_id}");
                            sb.AppendLine($"Envelope UUID: {doc.envelope_uuid}");
                            sb.AppendLine($"Document Profile: {doc.document_profile}");
                            sb.AppendLine($"Oluşturulma Zamanı: {doc.system_creation_time}");
                            sb.AppendLine($"Tarih: {doc.document_issue_date}");
                            sb.AppendLine($"Source ID: {doc.source_id}");
                            sb.AppendLine($"Destination ID: {doc.destination_id}");
                            sb.AppendLine($"Source Urn: {doc.source_urn}");
                            sb.AppendLine($"Source Title: {doc.source_title}");
                            sb.AppendLine($"Destination Urn: {doc.destination_urn}");
                            sb.AppendLine($"Para Birimi: {doc.currency_code}");
                            sb.AppendLine($"Ödenecek Tutar: {doc.invoice_total}");
                            sb.AppendLine($"Durum Kodu: {doc.state_code}");
                           



                            sb.AppendLine(new string('-', 100));
                        }
                    }

                    
                    string path = Path.Combine(Path.GetTempPath(), "SorguSonucu.txt");
                    File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

                    Process.Start("notepad.exe", path);
               
                
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorgu sırasında hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }







        public static void OutboxDocumentsWithGUIDList(List<string> guidList, string documentType)
        {
            var client = new QueryDocumentWSClient();

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.QueryOutboxDocumentsWithWithGUIDList(guidList.ToArray(), documentType); 

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Dönen belge sayısı: {yanit.documentsCount}");
                    sb.AppendLine(new string('-', 100));

                    if (yanit.documents != null)
                    {
                        foreach (var doc in yanit.documents)
                        {
                            sb.AppendLine($"Durum Açıklaması: {doc.state_explanation}");
                            sb.AppendLine($"UUID: {doc.document_uuid}");
                            sb.AppendLine($"ID: {doc.document_id}");
                            sb.AppendLine($"Envelope UUID: {doc.envelope_uuid}");
                            sb.AppendLine($"Document Profile: {doc.document_profile}");
                            sb.AppendLine($"Oluşturulma Zamanı: {doc.system_creation_time}");
                            sb.AppendLine($"Tarih: {doc.document_issue_date}");
                            sb.AppendLine($"Source ID: {doc.source_id}");
                            sb.AppendLine($"Destination ID: {doc.destination_id}");
                            sb.AppendLine($"Source Urn: {doc.source_urn}");
                            sb.AppendLine($"Source Title: {doc.source_title}");
                            sb.AppendLine($"Destination Urn: {doc.destination_urn}");
                            sb.AppendLine($"Para Birimi: {doc.currency_code}");
                            sb.AppendLine($"Ödenecek Tutar: {doc.invoice_total}");
                            sb.AppendLine($"Durum Kodu: {doc.state_code}");


                            sb.AppendLine(new string('-', 100));
                        }
                    }


                    string path = Path.Combine(Path.GetTempPath(), "SorguSonucu.txt");
                    File.WriteAllText(path, sb.ToString());
                    System.Diagnostics.Process.Start(path);


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorgu sırasında hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }












        public static void InboxDocument(string paramType, string parameter, string withXml)
        {
            var client = new QueryDocumentWSClient(); // doğru WSDL endpoint’ine göre ayarlanmalı

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();

                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.QueryInboxDocument(paramType, parameter, withXml);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Doküman Sayısı: {yanit.documentsCount}");
                    sb.AppendLine($"Max Record: {yanit.maxRecordIdinList}");


                    sb.AppendLine(new string('-', 50));



                    if (yanit.documents != null)
                    {
                        foreach (var doc in yanit.documents)
                        {

                            sb.AppendLine($"UUID: {doc.document_uuid}");
                            sb.AppendLine($"ID: {doc.document_id}");
                            sb.AppendLine($"Envelope UUID: {doc.envelope_uuid}");
                            sb.AppendLine($"Document Profile: {doc.document_profile}");
                            sb.AppendLine($"Oluşturulma Zamanı: {doc.system_creation_time}");
                            sb.AppendLine($"Tarih: {doc.document_issue_date}");
                            sb.AppendLine($"Source ID: {doc.source_id}");
                            sb.AppendLine($"Destination ID: {doc.destination_id}");
                            sb.AppendLine($"Source Urn: {doc.source_urn}");
                            sb.AppendLine($"Source Title: {doc.source_title}");
                            sb.AppendLine($"Destination Urn: {doc.destination_urn}");
                            sb.AppendLine($"Para Birimi: {doc.currency_code}");
                            sb.AppendLine($"Ödenecek Tutar: {doc.invoice_total}");

                           

                            if (withXml != "NONE" && doc.document_content != null)
                            {
                                //string xml = Encoding.UTF8.GetString(doc.document_content);
                                //sb.AppendLine("Fatura içeriği (XML):");
                                //sb.AppendLine(xml);


                            }




                            sb.AppendLine(new string('-', 50));
                        }
                    }


                    string path = Path.Combine(Path.GetTempPath(), "SorguSonucu.txt");
                    File.WriteAllText(path, sb.ToString());
                    System.Diagnostics.Process.Start(path);




                }
                catch (Exception ex)
                {
                    MessageBox.Show("QueryOutboxDocument hatası:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }






        public static void InboxDocumentWithDocumentDate(
        string startDate, string endDate, string documentType,
        string queried, string withXML, string takenFromEntegrator ,string minRecordId)
        {
            var client = new QueryDocumentWSClient();

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();

                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.QueryInboxDocumentsWithDocumentDate(
                        startDate, endDate, documentType, queried, withXML, takenFromEntegrator, minRecordId);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Dönen belge sayısı: {yanit.documentsCount}");


                    sb.AppendLine(new string('-', 50));

                    if (yanit.documents != null)
                    {
                        foreach (var doc in yanit.documents)
                        {
                            sb.AppendLine($"UUID: {doc.document_uuid}");
                            sb.AppendLine($"ID: {doc.document_id}"); ;
                            sb.AppendLine($"Tarih: {doc.document_issue_date}");
                            sb.AppendLine($"Source ID: {doc.source_id}");
                            sb.AppendLine($"Fatura Türü: {doc.document_profile}");
                            sb.AppendLine($"Dosya Türü: {doc.content_type}");
                            sb.AppendLine($"Yanıt: {doc.response_code}");






                            if (withXML != "NONE" && doc.document_content != null)
                            {
                                //string xml = Encoding.UTF8.GetString(doc.document_content);
                                //sb.AppendLine("Fatura içeriği (XML):");
                                //sb.AppendLine(xml);

                          


                            }





                            sb.AppendLine(new string('-', 100));
                        }
                    }

                    string path = Path.Combine(Path.GetTempPath(), "SorguSonucu.txt");
                    File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

                    Process.Start("notepad.exe", path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorgu sırasında hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }






        public static void InboxDocumentWithReceivedDate(
        string startDate, string endDate, string documentType,
        string queried, string withXML, string takenFromEntegrator ,string minRecordId)
        {
            var client = new QueryDocumentWSClient();

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();

                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.QueryInboxDocumentsWithReceivedDate(
                        startDate, endDate, documentType, queried, withXML, takenFromEntegrator ,minRecordId);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Dönen belge sayısı: {yanit.documentsCount}");


                    sb.AppendLine(new string('-', 100));

                    if (yanit.documents != null)
                    {
                        foreach (var doc in yanit.documents)
                        {
                            sb.AppendLine($"UUID: {doc.document_uuid}");
                            sb.AppendLine($"ID: {doc.document_id}"); ;
                            sb.AppendLine($"Tarih: {doc.document_issue_date}");
                            sb.AppendLine($"Source ID: {doc.source_id}");
                            sb.AppendLine($"Fatura Türü: {doc.document_profile}");
                            sb.AppendLine($"Dosya Türü: {doc.content_type}");





                            if (withXML != "NONE" && doc.document_content != null)
                            {
                                //string xml = Encoding.UTF8.GetString(doc.document_content);
                                //sb.AppendLine("Fatura içeriği (XML):");
                                //sb.AppendLine(xml);



                            }





                            sb.AppendLine(new string('-', 100));
                        }
                    }

                    string path = Path.Combine(Path.GetTempPath(), "SorguSonucu.txt");
                    File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

                    Process.Start("notepad.exe", path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorgu sırasında hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }








        public static void InboxDocumentsWithGUIDList(List<string> guidList, string documentType)
        {
            var client = new QueryDocumentWSClient();

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.QueryInboxDocumentsWithGUIDList(guidList.ToArray(), documentType);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Dönen belge sayısı: {yanit.documentsCount}");
                    sb.AppendLine(new string('-', 100));

                    if (yanit.documents != null)
                    {
                        foreach (var doc in yanit.documents)
                        {
                            
                            sb.AppendLine($"UUID: {doc.document_uuid}");
                            sb.AppendLine($"ID: {doc.document_id}");
                            sb.AppendLine($"Envelope UUID: {doc.envelope_uuid}");
                            sb.AppendLine($"Document Profile: {doc.document_profile}");
                            sb.AppendLine($"Oluşturulma Zamanı: {doc.system_creation_time}");
                            sb.AppendLine($"Tarih: {doc.document_issue_date}");
                            sb.AppendLine($"Source ID: {doc.source_id}");
                            sb.AppendLine($"Destination ID: {doc.destination_id}");
                            sb.AppendLine($"Source Urn: {doc.source_urn}");
                            sb.AppendLine($"Source Title: {doc.source_title}");
                            sb.AppendLine($"Destination Urn: {doc.destination_urn}");
                            sb.AppendLine($"Para Birimi: {doc.currency_code}");
                            sb.AppendLine($"Ödenecek Tutar: {doc.invoice_total}");
                           






                            sb.AppendLine(new string('-', 100));
                        }
                    }


                    string path = Path.Combine(Path.GetTempPath(), "SorguSonucu.txt");
                    File.WriteAllText(path, sb.ToString());
                    System.Diagnostics.Process.Start(path);


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorgu sırasında hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }









        public static void getUserGBList()
        {
            var client = new QueryDocumentWSClient();

            using (var scope = new OperationContextScope(client.InnerChannel))
            {


                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;


                var result = client.GetNewUserGBList();

                try
                {
                    string fileName = result.fileName ?? "output.zip";
                    byte[] byteIcerik = result.fileContent;

                    if (byteIcerik != null && byteIcerik.Length > 0)
                    {
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string fullPath = Path.Combine(desktopPath, fileName);

                        File.WriteAllBytes(fullPath, byteIcerik);

                        MessageBox.Show("Zip dosyası masaüstüne kaydedildi:\n" + fullPath, "Başarılı");
                    }
                    else
                    {
                        MessageBox.Show("Dosya içeriği boş.", "Uyarı");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata");
                }
            }
        }












        public static void getUserPKList()
        {
            var client = new QueryDocumentWSClient();

            using (var scope = new OperationContextScope(client.InnerChannel))
            {


                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;


                var result = client.GetNewUserPKList();

                try
                {
                    string fileName = result.fileName ?? "output.zip";
                    byte[] byteIcerik = result.fileContent;

                    if (byteIcerik != null && byteIcerik.Length > 0)
                    {
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string fullPath = Path.Combine(desktopPath, fileName);

                        File.WriteAllBytes(fullPath, byteIcerik);

                        MessageBox.Show("Zip dosyası masaüstüne kaydedildi:\n" + fullPath, "Başarılı");
                    }
                    else
                    {
                        MessageBox.Show("Dosya içeriği boş.", "Uyarı");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata");
                }
            }
        }








        public static void setTakenFromEntegrator(string[] documentUUID)
        {
            var client = new QueryDocumentWSClient(); // doğru WSDL endpoint’ine göre ayarlanmalı

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();

                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.SetTakenFromEntegrator(documentUUID);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Doküman Sayısı: {yanit.documentsCount}");
                    sb.AppendLine($"Max Record: {yanit.maxRecordIdinList}");

                    sb.AppendLine(new string('-', 65));

                    if (yanit.documents != null)
                    {
                        foreach (var doc in yanit.documents)
                        {
                         
                            sb.AppendLine($"UUID: {doc.document_uuid}");
                         
                            sb.AppendLine(new string('-', 65));
                        }
                    }


                    MessageBox.Show(sb.ToString(), "Giden Fatura Sorgu Sonucu");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("QueryOutboxDocument hatası:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }









        public static void queryAppResponseOfOutboxDocument(string documentUUID, string withXML)
        {
            var client = new QueryDocumentWSClient();

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.QueryAppResponseOfOutboxDocument(documentUUID, withXML);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Dönen belge sayısı: {yanit.documentsCount}");
                    sb.AppendLine(new string('-', 50));

                    if (yanit.documents != null)
                    {
                        foreach (var doc in yanit.documents)
                        {

                            sb.AppendLine($"UUID: {doc.document_uuid}");
                            sb.AppendLine($"ID: {doc.document_id}");
                            sb.AppendLine($"Envelope UUID: {doc.envelope_uuid}");
                            sb.AppendLine($"Document Profile: {doc.document_profile}");
                            sb.AppendLine($"Oluşturulma Zamanı: {doc.system_creation_time}");
                            sb.AppendLine($"Tarih: {doc.document_issue_date}");
                            sb.AppendLine($"Source ID: {doc.source_id}");
                            sb.AppendLine($"Destination ID: {doc.destination_id}");
                            sb.AppendLine($"Source Urn: {doc.source_urn}");
                            sb.AppendLine($"Source Title: {doc.source_title}");
                            sb.AppendLine($"Destination Urn: {doc.destination_urn}");
                            sb.AppendLine($"Para Birimi: {doc.currency_code}");
                            sb.AppendLine($"Ödenecek Tutar: {doc.invoice_total}");



                            if (withXML != "NONE" && doc.document_content != null)
                            {
                                string xml = Encoding.UTF8.GetString(doc.document_content);
                                sb.AppendLine("Fatura içeriği (XML):");
                                sb.AppendLine(xml);


                            }




                            sb.AppendLine(new string('-', 50));
                        }
                    }


                    string path = Path.Combine(Path.GetTempPath(), "SorguSonucu.txt");
                    File.WriteAllText(path, sb.ToString());
                    System.Diagnostics.Process.Start(path);


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorgu sırasında hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }









        public static void queryAppResponseOfInboxDocument(string documentUUID, string withXML)
        {
            var client = new QueryDocumentWSClient();

            using (var scope = new OperationContextScope(client.InnerChannel))
            {
                var prop = new HttpRequestMessageProperty();
                prop.Headers.Add("Username", Session.username);
                prop.Headers.Add("Password", Session.password);

                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = prop;

                try
                {
                    var yanit = client.QueryAppResponseOfOutboxDocument(documentUUID, withXML);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Durum: {yanit.queryState} - {yanit.stateExplanation}");
                    sb.AppendLine($"Dönen belge sayısı: {yanit.documentsCount}");
                    sb.AppendLine(new string('-', 50));

                    if (yanit.documents != null)
                    {
                        foreach (var doc in yanit.documents)
                        {

                            sb.AppendLine($"UUID: {doc.document_uuid}");
                            sb.AppendLine($"ID: {doc.document_id}");
                            sb.AppendLine($"Envelope UUID: {doc.envelope_uuid}");
                            sb.AppendLine($"Document Profile: {doc.document_profile}");
                            sb.AppendLine($"Oluşturulma Zamanı: {doc.system_creation_time}");
                            sb.AppendLine($"Tarih: {doc.document_issue_date}");
                            sb.AppendLine($"Source ID: {doc.source_id}");
                            sb.AppendLine($"Destination ID: {doc.destination_id}");
                            sb.AppendLine($"Source Urn: {doc.source_urn}");
                            sb.AppendLine($"Source Title: {doc.source_title}");
                            sb.AppendLine($"Destination Urn: {doc.destination_urn}");
                            sb.AppendLine($"Para Birimi: {doc.currency_code}");
                            sb.AppendLine($"Ödenecek Tutar: {doc.invoice_total}");



                            if (withXML != "NONE" && doc.document_content != null)
                            {
                                string xml = Encoding.UTF8.GetString(doc.document_content);
                                sb.AppendLine("Fatura içeriği (XML):");
                                sb.AppendLine(xml);


                            }




                            sb.AppendLine(new string('-', 50));
                        }
                    }


                    string path = Path.Combine(Path.GetTempPath(), "SorguSonucu.txt");
                    File.WriteAllText(path, sb.ToString());
                    System.Diagnostics.Process.Start(path);


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorgu sırasında hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }










    }
}