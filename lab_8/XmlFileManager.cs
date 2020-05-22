using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace lab_8
{
    public class XmlFileManager
    {
        public void Save(IEnumerable<PCStruct> items, string filename)
        {
            var file = new XElement("PCs");
            items.ToList().ForEach(item => file.Add(item.GetXElement()));
            file.Save(filename);
        }

        public IEnumerable<PCStruct> Load(string filename)
        {
            var file = XElement.Load(filename);
            return file.Descendants("PC").Select(item => PCStruct.FromXElement(item)).ToList();
        }
    }
}
