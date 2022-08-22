using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using System.Net.Sockets;
using System.Net;
using Xamarin.Essentials;
using Xamarin.Forms;
using mobile_poverka_asset.Services;

namespace mobile_poverka_asset.XML
{
    
    public static class XML
    {
        private static XElement xmlMarkUp;
        private static XDocument xdoc;
        private static string m_fileName;
        private static string ip;
        private static int port;
        private static List<Models.Item> items;
        private static Models.Spisok spisok;
        public static XElement getMarkup() {
            return xmlMarkUp;
        }
        public static void Assign(List<Models.Item> itemsX, Models.Spisok spisokX)
        {
            items = itemsX;
            spisok = spisokX;
        }
        [STAThread]
        public static void CreateXML(string ipXML, int portXML) {
            if (items == null || spisok == null) return;

            try
            {
                xmlMarkUp = new XElement("Full");
                XElement branch1 = new XElement("Devices");
                XElement branch2 = new XElement("List",
                                        new XElement("ID", spisok.Id),
                                        new XElement("Name", spisok.Name),
                                        new XElement("Date", spisok.Date),
                                        new XElement("Count", spisok.Count),
                                        new XElement("Complete", spisok.Complete),
                                        new XElement("Comment", spisok.Comment));


                foreach (Models.Item item in items)
                {
                    branch1.Add(new XElement("Device",
                                    new XElement("Serial", item.Serial),
                                    new XElement("Channel_ID", item.idchannel)));
                    //new XElement ("Spisok_ID", spisok.Id));
                }

                xmlMarkUp.Add(branch2);
                xmlMarkUp.Add(branch1);

                xdoc = new XDocument(xmlMarkUp);

                //Console.WriteLine(xmlMarkUp);
                m_fileName = spisok.Name;
                ip = ipXML;
                port = portXML;
                Thread t = new Thread(new ThreadStart(ClientThreadStart));
                t.Start();
                t.Join();

                items = null;
                spisok = null;
            }
            catch (Exception ex) {
                Console.WriteLine("-<-XML PAGE ERROR-<-" + ex.Message);
            }
        }
        private static void ClientThreadStart()
        {
            try
            {
                Socket clientSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);


                clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            
            // Send the file name.
            clientSocket.Send(Encoding.ASCII.GetBytes(m_fileName + ".xml\r\n"));

                // Receive the length of the filename.
                byte[] data = new byte[128];
                clientSocket.Receive(data);
                int length = BitConverter.ToInt32(data, 0);

                clientSocket.Send(Encoding.ASCII.GetBytes(xdoc.ToString() + "\r\n"));

                clientSocket.Send(Encoding.ASCII.GetBytes("\r\n"));

                // Get the total length
                clientSocket.Receive(data);
                length = BitConverter.ToInt32(data, 0);
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                //DependencyService.Get<IMessage>().ShortAlert("Ошибка при отправке.\nПроверьте IP и Port");
                return;
            }
}
    }
}
