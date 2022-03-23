using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ItemRestrictions.Services
{
    public class RestrictedItem
    {
        [XmlText]
        public ushort id { get; set; }
        [XmlAttribute]
        public string Bypass { get; set; }
    }
}
