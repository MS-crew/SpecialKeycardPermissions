using System;
using Exiled.API.Features;
using Map = Exiled.Events.Handlers.Map;
using Item = Exiled.Events.Handlers.Item;
using Player = Exiled.Events.Handlers.Player;

namespace SpecialKeycardPermissions
{
    public class Plugin : Plugin<Config>
    {
        private EventHandlers eventHandler;

        public override string Author => "ZurnaSever";

        public override string Name => "SpecialKeycardPermission";

        public override string Prefix => "SpecialKeycardPermission";

        public override Version Version { get; } = new Version(2, 1, 1);

        public override Version RequiredExiledVersion { get; } = new Version(9, 3, 0);

        public override void OnEnabled()
        {
            eventHandler = new EventHandlers(this);

            Map.Generated += eventHandler.DoorCheck; 
            //Map.PickupAdded += eventHandler.PickupCheck; 
            //Player.ItemAdded += eventHandler.KeycardItemCheck;
            Player.InteractingDoor += eventHandler.KeycardCheck;
            Item.KeycardInteracting += eventHandler.ThrowKeycardCheck;

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Map.Generated -= eventHandler.DoorCheck;
            //Map.PickupAdded -= eventHandler.PickupCheck;
            //Player.ItemAdded -= eventHandler.KeycardItemCheck;
            Player.InteractingDoor -= eventHandler.KeycardCheck;
            Item.KeycardInteracting -= eventHandler.ThrowKeycardCheck;

            eventHandler = null;
            base.OnDisabled();
        }
    }
}
