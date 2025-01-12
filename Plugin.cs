using System;
using Exiled.API.Features;
using M = Exiled.Events.Handlers.Map;
using P = Exiled.Events.Handlers.Player;

namespace SpecialKeycardPermissions
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }
        public override string Author => "ZurnaSever";
        public override string Name => "SpecialKeycardPermission";
        public override string Prefix => "SpecialKeycardPermission";
        public override Version RequiredExiledVersion { get; } = new Version(9, 3, 0);
        public override Version Version { get; } = new Version(2, 3, 0);

        private EventHandlers eventHandler;
        public override void OnEnabled()
        {
            Instance = this;
            eventHandler = new EventHandlers(this);
            M.Generated += eventHandler.DoorCheck;
            P.InteractingDoor += eventHandler.KeycardCheck;

            if (!Plugin.Instance.Config.SpecialPermission.IsEmpty())
            {
                M.PickupAdded += eventHandler.PickupCheck;
                P.ItemAdded += eventHandler.KeycardItemCheck;
            }

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            M.Generated -= eventHandler.DoorCheck;
            P.InteractingDoor -= eventHandler.KeycardCheck;

            if (!Plugin.Instance.Config.SpecialPermission.IsEmpty())
            {
                M.PickupAdded -= eventHandler.PickupCheck;
                P.ItemAdded -= eventHandler.KeycardItemCheck;
            }
            eventHandler = null;
            Instance = null;
            
            base.OnDisabled();
        }
    }
}
