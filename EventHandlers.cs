using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Item;
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

        /*public void PickupCheck(PickupAddedEventArgs ev)
        {
            if (ev.Pickup.Category != ItemCategory.Keycard)
                return;

            if (plugin.Config.SpecialPermission.IsEmpty())
                return;

            if (!plugin.Config.SpecialPermission.TryGetValue(ev.Pickup.Type, out var permissions))
                return;

            if (ev.Pickup.Is<KeycardPickup>(out KeycardPickup keycard))
               keycard.Permissions = permissions;
        }

        public void KeycardItemCheck(ItemAddedEventArgs ev)
        {
            if (ev.Item.Category != ItemCategory.Keycard)
                return;

            if (plugin.Config.SpecialPermission.IsEmpty())
                return;

            if (CustomItem.TryGet(ev.Item.Serial, out _))
                return;

            if (!plugin.Config.SpecialPermission.TryGetValue(ev.Item.Type, out var permissions))
                return;

            if (ev.Item.Is<Keycard>(out Keycard keycard))
                keycard.Permissions = permissions;
        }*/

        public void KeycardCheck(InteractingDoorEventArgs ev)
        {
            if (ev.Player == null || ev.Door.IsLocked || ev.Player.IsBypassModeEnabled)
                return;

            Log.Debug($"Door id: {ev.Door.Base.DoorId}, Doortype: {ev.Door.Type}");

            ItemType[] validKeycards = GetValidKeycards(ev.Door);
            if (validKeycards == null)
                return;

            bool hasValidKeycard = plugin.Config.HeldKeycard
                ? ev.Player.CurrentItem?.IsKeycard == true && validKeycards.Contains(ev.Player.CurrentItem.Type)
                : ev.Player.Items.Any(item => item.IsKeycard && validKeycards.Contains(item.Type));

            Log.Debug(hasValidKeycard
                ? "This special door can be opened with this card."
                : "There is no proper card to open this special door.");

            ev.IsAllowed = hasValidKeycard;
        }

        public void ThrowKeycardCheck(KeycardInteractingEventArgs ev)
        {
            if (ev.Door.IsLocked)
                return;

            Log.Debug($"Door id: {ev.Door.Base.DoorId}, Doortype: {ev.Door.Type}");

            ItemType[] validKeycards = GetValidKeycards(ev.Door);
            if (validKeycards == null)
                return;

            bool hasValidKeycard = validKeycards.Contains(ev.KeycardPickup.Type);

            Log.Debug(hasValidKeycard
                ? "This special door can be opened with this card."
                : "There is no proper card to open this special door.");

            ev.IsAllowed = hasValidKeycard;
        }

        private ItemType[] GetValidKeycards(Door door)
        {
            if (plugin.Config.SpecialDoorIdList.TryGetValue(door.Base.DoorId, out ItemType[] idKeycards))
            {
                Log.Debug($"The door id {door.Base.DoorId} is in the Special Door Id List");
                return idKeycards;
            }

            else if (plugin.Config.SpecialDoorList.TryGetValue(door.Type, out ItemType[] typeKeycards))
            {
                Log.Debug($"The door type {door.Type} is in the Special Door List");
                return typeKeycards;
            }

            return null;
        }
    }
}
