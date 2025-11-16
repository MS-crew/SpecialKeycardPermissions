using System;
using Exiled.API.Features;
using Map = Exiled.Events.Handlers.Map;
using Server = Exiled.Events.Handlers.Server;
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

        public override Version Version { get; } = new Version(2, 2, 1);

        public override Version RequiredExiledVersion { get; } = new Version(9, 10, 0);

        public override void OnEnabled()
        {
            eventHandler = new EventHandlers(this.Config);

            Map.PickupAdded += eventHandler.PickupCheck;
            Server.RoundStarted += eventHandler.DoorCheck;
            Player.ItemAdded += eventHandler.KeycardItemCheck;
            Player.InteractingDoor += eventHandler.KeycardCheck;
            Item.KeycardInteracting += eventHandler.ThrowKeycardCheck;

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Map.PickupAdded -= eventHandler.PickupCheck;
            Server.RoundStarted -= eventHandler.DoorCheck;
            Player.ItemAdded -= eventHandler.KeycardItemCheck;
            Player.InteractingDoor -= eventHandler.KeycardCheck;
            Item.KeycardInteracting -= eventHandler.ThrowKeycardCheck;

            eventHandler = null;
            base.OnDisabled();
        }
    }
}
