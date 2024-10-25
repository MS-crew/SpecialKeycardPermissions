using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Linq;

namespace SpecialKeycardPermissions
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;
        public void KeycardCheck(InteractingDoorEventArgs ev)
        {
            if (ev.Player == null || ev.Door.IsLocked || ev.Player.ReferenceHub.serverRoles.BypassMode)
                return;

            if (Plugin.Instance.Config.SpecialDoorList.TryGetValue(ev.Door.Type, out var keycard))
            {
                bool hasValidKeycard = ev.Player.CurrentItem?.IsKeycard == true && keycard.Contains(ev.Player.CurrentItem.Type);
                Log.Debug(hasValidKeycard ? "Özel kapı bu kartla açılabilir." : "Özel kapıyı açmak için uygun kart yok.");
                ev.IsAllowed = hasValidKeycard;
                return;
            }

            if (ev.Door.KeycardPermissions.HasFlag(KeycardPermissions.None) || ev.Player.CurrentItem?.IsKeycard != true)
                return;

            if (Plugin.Instance.Config.SpecialPermission.TryGetValue(ev.Player.CurrentItem.Type, out var permissions))
            {
                ev.IsAllowed = permissions.Any(permission => ev.Door.KeycardPermissions.HasFlag(permission));
                Log.Debug(ev.IsAllowed ? "Bu kart özel izin ile kapıyı açtı." : "Kartın özel izni kapıyı açmaya yetmedi.");
            }
        }
        public void KeycardCheckLocker(InteractingLockerEventArgs ev)
        {
            if (ev.InteractingChamber.RequiredPermissions == KeycardPermissions.None || ev.Player.CurrentItem?.IsKeycard != true ||
                ev.InteractingLocker.Type is LockerType.Misc or LockerType.Adrenaline or LockerType.Medkit ||
                ev.Player == null || ev.Player.ReferenceHub.serverRoles.BypassMode)
                return;

            if (Plugin.Instance.Config.SpecialPermission.TryGetValue(ev.Player.CurrentItem.Type, out var keycardPermissions))
            {
                ev.IsAllowed = keycardPermissions.Any(permission => ev.InteractingChamber.RequiredPermissions.HasFlag(permission));

                Log.Debug(ev.IsAllowed
                    ? "Bu kart özel izinle locker'ı açtı."
                    : $"Kartın özel izni locker'ı açmaya yetmedi - Chamber: {ev.InteractingChamber}, Locker: {ev.InteractingLocker.Type}, İzinler: {ev.InteractingChamber.RequiredPermissions}");
            }
        }
        public void KeycardCheckGenerator(UnlockingGeneratorEventArgs ev)
        {
            if (ev.Player == null)
            {
                Log.Debug("Oyuncu null");
                return;
            }

            if (ev.Player.ReferenceHub.serverRoles.BypassMode || ev.Player.IsScp)
            {
                Log.Debug("Oyuncu bypass modunda veya SCP");
                return;
            }

            if (ev.Player.CurrentItem?.IsKeycard == true &&
                Plugin.Instance.Config.SpecialPermission.TryGetValue(ev.Player.CurrentItem.Type, out var keycardPermissions))
            {
                Log.Debug("Kart özel izinlere sahip ve jeneratör izni gerektiriyor.");

                ev.IsAllowed = keycardPermissions.Any(permission => ev.Generator.KeycardPermissions.HasFlag(permission));

                Log.Debug(ev.IsAllowed
                    ? "Kartın özel izni jeneratörü açtı."
                    : "Kartın özel izni jeneratörü açmaya yetmedi.");
            }
        }

    }
}
