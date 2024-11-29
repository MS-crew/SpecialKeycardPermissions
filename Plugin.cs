using System;
using Exiled.API.Features;
using P = Exiled.Events.Handlers.Player;

namespace SpecialKeycardPermissions
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }
        public override string Author => "ZurnaSever";
        public override string Name => "SpecialKeycardPermission";
        public override string Prefix => "SpecialKeycardPermission";
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);
        public override Version Version { get; } = new Version(1, 0, 3);

        private EventHandlers eventHandler;
        public override void OnEnabled()
        {
            Instance = this;
            eventHandler = new EventHandlers(this);

            P.InteractingDoor += eventHandler.KeycardCheck;
            P.InteractingLocker += eventHandler.KeycardCheckLocker;
            P.UnlockingGenerator += eventHandler.KeycardCheckGenerator;
            
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            P.InteractingDoor -= eventHandler.KeycardCheck;
            P.InteractingLocker -= eventHandler.KeycardCheckLocker;
            P.UnlockingGenerator -= eventHandler.KeycardCheckGenerator;

            eventHandler = null;
            Instance = null;
            
            base.OnDisabled();
        }
    }
}
