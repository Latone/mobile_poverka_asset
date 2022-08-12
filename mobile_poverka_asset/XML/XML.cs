using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace mobile_poverka_asset.XML
{
    public static class XML
    {
        private static XElement xmlMarkUp;
        public static XElement getMarkup() {
            return xmlMarkUp;
        }
        public static void CreateXML(List<Models.Item> items, Models.Spisok spisok) {
            xmlMarkUp = new XElement("Full");
            XElement branch1 = new XElement("Pribori");
            XElement branch2 = new XElement("Spisok",
                                    new XElement("ID", spisok.Id),
                                    new XElement("Name",spisok.Name),
                                    new XElement("Date", spisok.Date),
                                    new XElement("Count", spisok.Count),
                                    new XElement("Complete", spisok.Complete),
                                    new XElement("Comment", spisok.Comment));


            foreach (Models.Item item in items) {
                branch1.Add(new XElement("Serial", item.Serial),
                    new XElement("Channel_ID", item.idchannel));
                            //new XElement ("Spisok_ID", spisok.Id));
            }

            xmlMarkUp.Add(branch2);
            xmlMarkUp.Add(branch1);

            Console.WriteLine(xmlMarkUp);
        }
    }
}
