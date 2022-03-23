using ItemRestrictions.Services;
using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ItemRestrictions
{
    public class ItemRestrictionsConfiguration : IRocketPluginConfiguration
    {
        public bool IgnoreAdmins { get; set; }
        [XmlArrayItem(ElementName = "Item")]
        public List<RestrictedItem> RestrictedItems;
        public void LoadDefaults()
        {
            IgnoreAdmins = false;
            RestrictedItems = new List<RestrictedItem>()
            {
                new RestrictedItem()
                {
                    Bypass = "Bypass.Permission",
                    id = (ushort) 1
                }
            };
        }
    }
}
