using System.Windows.Forms;
using System.Xml.Linq;

namespace lab_8
{
    public struct PCStruct
    {
        public string Code { get; set; }
        public string Manufacturer { get; set; }
        public string Proc { get; set; }
        public double Freq { get; set; }
        public double Mem { get; set; }
        public double HDD { get; set; }
        public double Video { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        public static PCStruct FromXElement(XElement element)
        {
            return new PCStruct
            {
                Code = element.Attribute("Code").Value,
                Manufacturer = element.Attribute("Manufacturer").Value,
                Proc = element.Attribute("Proc").Value,
                Freq = double.Parse(element.Attribute("Freq").Value),
                Mem = double.Parse(element.Attribute("Mem").Value),
                HDD = double.Parse(element.Attribute("HDD").Value),
                Video = double.Parse(element.Attribute("Video").Value),
                Price = int.Parse(element.Attribute("Price").Value),
                Count = int.Parse(element.Attribute("Count").Value)
            };
        }

        public XElement GetXElement()
        {
            var element = new XElement("PC");
            element.SetAttributeValue("Code", Code);
            element.SetAttributeValue("Manufacturer", Manufacturer);
            element.SetAttributeValue("Proc", Proc);
            element.SetAttributeValue("Freq", Freq.ToString());
            element.SetAttributeValue("Mem", Mem.ToString());
            element.SetAttributeValue("HDD", HDD.ToString());
            element.SetAttributeValue("Video", Video.ToString());
            element.SetAttributeValue("Price", Price);
            element.SetAttributeValue("Count", Count);
            return element;
        }

        public ListViewItem GetListViewItem()
        {
            var listItem = new ListViewItem(Code);
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, Manufacturer));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, Proc));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, Freq.ToString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, Mem.ToString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, HDD.ToString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, Video.ToString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, Price.ToString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, Count.ToString()));
            listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, Proc));
            return listItem;
        }
    }
}
