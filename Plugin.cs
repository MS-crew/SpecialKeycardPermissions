using System;
using Exiled.API.Features;
using M = Exiled.Events.Handlers.Map;
using P = Exiled.Events.Handlers.Player;

namespace SpecialKeycardPermissions
{
    public class Plugin : Plugin<Config>
    {
        private EventHandlers eventHandler;

        public override string Author => "ZurnaSever";

        public override string Name => "SpecialKeycardPermission";

        public override string Prefix => "SpecialKeycardPermission";

        public override Version Version { get; } = new Version(2, 3, 1);

        public override Version RequiredExiledVersion { get; } = new Version(9, 3, 0);

        public override void OnEnabled()
        {
            eventHandler = new EventHandlers(this);

            M.Generated += eventHandler.DoorCheck; 
            M.PickupAdded += eventHandler.PickupCheck; 
            P.ItemAdded += eventHandler.KeycardItemCheck;
            P.InteractingDoor += eventHandler.KeycardCheck;

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            M.Generated -= eventHandler.DoorCheck;
            M.PickupAdded -= eventHandler.PickupCheck;
            P.ItemAdded -= eventHandler.KeycardItemCheck;
            P.InteractingDoor -= eventHandler.KeycardCheck;

            eventHandler = null;
            base.OnDisabled();
        }
    }
}
