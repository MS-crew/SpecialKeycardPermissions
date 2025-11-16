using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Enums;
using Exiled.API.Features.Items;

using KeycardPickup = Exiled.API.Features.Pickups.KeycardPickup;
using Exiled.CustomItems.API.Features;

namespace SpecialKeycardPermissions
{
    public class EventHandlers
    {
        private readonly Config config;

        public EventHandlers(Config config) => this.config = config;

        public void DoorCheck()
        {
            foreach (Door door in Door.List)
            {
                if (config.SpecialDoorIds.TryGetValue(door.Base.DoorId, out KeycardPermissions idPerms))
                {
                    door.KeycardPermissions = idPerms;
                    Log.Debug($"Door id: {door.Base.DoorId}, Permissions: {door.KeycardPermissions}");
                    return;
                }
                    
                if (config.SpecialDoorTypes.TryGetValue(door.Type, out KeycardPermissions typePerms))
                {
                    door.KeycardPermissions = typePerms;
                    Log.Debug($"Door type: {door.Type}, Permissions: {typePerms}");
                    return;
                }
            }
        }
        
        public void PickupCheck(PickupAddedEventArgs ev)
        {
            if (ev.Pickup.Category != ItemCategory.Keycard)
                return;

            if (!config.SpecialPermission.TryGetValue(ev.Pickup.Type, out KeycardPermissions permissions))
                return;

            if (ev.Pickup is not KeycardPickup keycardpickup)
                return;

            keycardpickup.Permissions = permissions;
        }

        public void KeycardItemCheck(ItemAddedEventArgs ev)
        {
            if (ev.Item.Category != ItemCategory.Keycard)
                return;

            if (CustomItem.TryGet(ev.Item.Serial, out _))
                return;

            if (!config.SpecialPermission.TryGetValue(ev.Item.Type, out KeycardPermissions permissions))
                return;

            if (ev.Item is not Keycard keycard)
                return;

            keycard.Permissions = permissions;
        }
        
        public void KeycardCheck(InteractingDoorEventArgs ev)
        {
            if (ev.Player == null || ev.Door.IsLocked || ev.Player.IsBypassModeEnabled)
                return;

            Log.Debug($"Door id: {ev.Door.Base.DoorId}, Doortype: {ev.Door.Type}");

            ItemType[] validKeycards = GetValidKeycards(ev.Door);
            if (validKeycards == null)
                return;

            bool hasValidKeycard = config.HeldKeycard
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
            if (config.SpecialDoorIdList.TryGetValue(door.Base.DoorId, out ItemType[] idKeycards))
            {
                Log.Debug($"The door id {door.Base.DoorId} is in the Special Door Id List");
                return idKeycards;
            }

            else if (config.SpecialDoorList.TryGetValue(door.Type, out ItemType[] typeKeycards))
            {
                Log.Debug($"The door type {door.Type} is in the Special Door List");
                return typeKeycards;
            }

            return null;
        }
    }
}
