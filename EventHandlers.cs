using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.CustomItems;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;

namespace SpecialKeycardPermissions
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;
        public void Start()
        {
            foreach (Item keycard in Item.List)
            {
                if (Plugin.Instance.Config.SpecialPermission.TryGetValue(keycard.Type, out var permissions))
                {
                    if (keycard.Is<Keycard>(out Keycard keycard1))
                        keycard1.Permissions = permissions;
                }
            }
        }
        public void KeycardItemCheck(ItemAddedEventArgs ev)
        {
            if (CustomItem.TryGet(ev.Item.Serial, out _))
                return;

            if (Plugin.Instance.Config.SpecialPermission.TryGetValue(ev.Item.Type, out var permissions))
                if (ev.Item.Is<Keycard>(out Keycard keycard))
                    keycard.Permissions = permissions;
        }
        public void DoorCheck()
        {
            foreach (Door door in Door.List)
            {
                if (plugin.Config.SpecialDoorIds.ContainsKey(door.Base.DoorId))
                    door.KeycardPermissions = plugin.Config.SpecialDoorIds[door.Base.DoorId];

                else if (plugin.Config.SpecialDoorTypes.ContainsKey(door.Type))
                    door.KeycardPermissions = plugin.Config.SpecialDoorTypes[door.Type];
            }
        }
        public void PickupCheck(PickupAddedEventArgs ev)
        {
            if (Plugin.Instance.Config.SpecialPermission.TryGetValue(ev.Pickup.Type, out var permissions))
                if (ev.Pickup.Is<KeycardPickup>(out KeycardPickup keycard))
                    keycard.Permissions = permissions;
        }
        public void KeycardCheck(InteractingDoorEventArgs ev)
        {
            if (ev.Player == null || ev.Door.IsLocked || ev.Player.ReferenceHub.serverRoles.BypassMode)
                return;
            Log.Debug($"Door id: {ev.Door.Base.DoorId}");

            if (Plugin.Instance.Config.SpecialDoorıdList.TryGetValue(ev.Door.Base.DoorId, out var doorIdKeycards))
            {
                bool hasValidKeycard;
                if (!plugin.Config.HeldKeycard)
                    hasValidKeycard = ev.Player.Items.Any(item => item.IsKeycard && doorIdKeycards.Contains(item.Type));
                else
                    hasValidKeycard = ev.Player.CurrentItem?.IsKeycard == true && doorIdKeycards.Contains(ev.Player.CurrentItem.Type);

                Log.Debug(hasValidKeycard ? "Özel idli kapı bu kartla açılabilir." : "Özel idli kapıyı açmak için uygun kart yok.");
                ev.IsAllowed = hasValidKeycard;
                return;
            }

            else if (Plugin.Instance.Config.SpecialDoorList.TryGetValue(ev.Door.Type, out var doorTypeKeycards))
            {
                bool hasValidKeycard;
                if (!plugin.Config.HeldKeycard)
                    hasValidKeycard = ev.Player.Items.Any(item => item.IsKeycard && doorTypeKeycards.Contains(item.Type));
                else
                    hasValidKeycard = ev.Player.CurrentItem?.IsKeycard == true && doorTypeKeycards.Contains(ev.Player.CurrentItem.Type);

                Log.Debug(hasValidKeycard ? "Özel typleli kapı bu kartla açılabilir." : "Özel typleli kapıyı açmak için uygun kart yok.");
                ev.IsAllowed = hasValidKeycard;
                return;
            }
        }
    }
}
