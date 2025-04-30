using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Exiled.CustomItems.API.Features;

namespace SpecialKeycardPermissions
{
    public class EventHandlers
    {
        private readonly Plugin plugin;

        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        public void DoorCheck()
        {
            foreach (Door door in Door.List)
            {
                if (plugin.Config.SpecialDoorIds.TryGetValue(door.Base.DoorId, out var idPerms))
                    door.KeycardPermissions = idPerms;

                else if (plugin.Config.SpecialDoorTypes.TryGetValue(door.Type, out var typePerms))
                    door.KeycardPermissions = typePerms;
            }
        }

        public void PickupCheck(PickupAddedEventArgs ev)
        {
            if (plugin.Config.SpecialPermission.IsEmpty())
                return;

            if (!plugin.Config.SpecialPermission.TryGetValue(ev.Pickup.Type, out var permissions))
                return;

            if (ev.Pickup.Is<KeycardPickup>(out KeycardPickup keycard))
                keycard.Permissions = permissions;
        }

        public void KeycardItemCheck(ItemAddedEventArgs ev)
        {
            if (plugin.Config.SpecialPermission.IsEmpty())
                return;

            if (CustomItem.TryGet(ev.Item.Serial, out _))
                return;

            if (!plugin.Config.SpecialPermission.TryGetValue(ev.Item.Type, out var permissions))
                return;

            if (ev.Item.Is<Keycard>(out Keycard keycard))
                keycard.Permissions = permissions;
        }
        
        public void KeycardCheck(InteractingDoorEventArgs ev)
        {
            if (ev.Player == null || ev.Door.IsLocked || ev.Player.ReferenceHub.serverRoles.BypassMode)
                return;

            Log.Debug($"Door id: {ev.Door.Base.DoorId}, Doortype: {ev.Door.Type}");

            ItemType[] validKeycards = null;

            if (plugin.Config.SpecialDoorIdList.TryGetValue(ev.Door.Base.DoorId, out ItemType[] idKeycards))
            {
                validKeycards = idKeycards;
                Log.Debug("Kapı idsi özel listede.");
            }
            else if (plugin.Config.SpecialDoorList.TryGetValue(ev.Door.Type, out ItemType[] typeKeycards))
            {
                validKeycards = typeKeycards;
                Log.Debug("Kapı tipi özel listede.");
            }

            if (validKeycards == null)
                return;

            bool hasValidKeycard = plugin.Config.HeldKeycard
                ? ev.Player.CurrentItem?.IsKeycard == true && validKeycards.Contains(ev.Player.CurrentItem.Type)
                : ev.Player.Items.Any(item => item.IsKeycard && validKeycards.Contains(item.Type));

            Log.Debug(hasValidKeycard
                ? "Bu özel kapı bu kartla açılabilir."
                : "Bu özel kapıyı açmak için uygun kart yok.");

            ev.IsAllowed = hasValidKeycard;
        }
    }
}
