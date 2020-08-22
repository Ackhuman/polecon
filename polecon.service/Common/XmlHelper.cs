using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace polecon.service.Xml
{
    public static class XmlHelper
    {
        public static XmlElement FromString(string xml)
        {
            var xEl = XElement.Parse(xml);
            var doc = new XmlDocument();
            var element = doc.ReadNode(xEl.CreateReader()) as XmlElement;
            return element;
        }
    }
}
