using ItemRestrictions.Services;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.API.Serialisation;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.Core.Plugins;
using Rocket.Unturned.Enumerations;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ItemRestrictions
{
    public class Main : RocketPlugin<ItemRestrictionsConfiguration>
    {
        // Setting Instance
        public static Main Instance;
        protected override void Load()
        {
            Instance = this;
            base.Load();
            Logger.Log("#############################################", ConsoleColor.Yellow);
            Logger.Log("###        ItemRestrictions Loaded        ###", ConsoleColor.Yellow);
            Logger.Log("###   Plugin Created By blazethrower320   ###", ConsoleColor.Yellow);
            Logger.Log("###            Join my Discord:           ###", ConsoleColor.Yellow);
            Logger.Log("###     https://discord.gg/YsaXwBSTSm     ###", ConsoleColor.Yellow);
            Logger.Log("#############################################", ConsoleColor.Yellow);
            // Subs
            UnturnedPlayerEvents.OnPlayerInventoryAdded += OnPlayerInventoryAdded;
        }
        protected override void Unload()
        {
            Logger.Log("ItemRestrictions Unloaded");
            UnturnedPlayerEvents.OnPlayerInventoryAdded -= OnPlayerInventoryAdded;
        }

        private void OnPlayerInventoryAdded(UnturnedPlayer player, InventoryGroup inventoryGroup, byte inventoryIndex, ItemJar P)
        {
            RestrictedItem item = this.Configuration.Instance.RestrictedItems.FirstOrDefault<RestrictedItem>((Func<RestrictedItem, bool>)(x => (int)x.id == (int)P.item.id));
            if(player.IsAdmin && Main.Instance.Configuration.Instance.IgnoreAdmins == true || player.GetPermissions().Any<Permission>((Func<Permission, bool>)(x => x.Name == "ignore.*")) || item == null || player.GetPermissions().Any<Permission>((Func<Permission, bool>) (x => x.Name == item.Bypass))){
                return;
            }
            player.Inventory.removeItem((byte)inventoryGroup, inventoryIndex);
            string itemName = Assets.find(EAssetType.ITEM, P.item.id)?.FriendlyName;
            ChatManager.say(player.CSteamID, Main.Instance.Translate("ItemRestrictions_ItemRemoved", itemName, item.Bypass), Color.red, true);
        }
        public override TranslationList DefaultTranslations => new TranslationList
        {
            {"ItemRestrictions_ItemRemoved", "<color=#FF0000>[Item Restrictions] </color><color=#3E65FF> {0} Restricted!</color><color=#F3F3F3> Missing Permission: </color><color=#3E65FF> {1}</color>"},
        };
    }
}

/*
#F3F3F3 - White
#FF0000 - Red
#3E65FF - Blue
#4CDB3D - Green
*/