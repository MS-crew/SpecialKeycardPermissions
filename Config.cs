using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpecialKeycardPermissions
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("This list shows which card will change your permissions.")]
        public Dictionary<ItemType, KeycardPermissions[]> SpecialPermission { get; set; } = new()
        {
            {
            ItemType.KeycardJanitor,
            new KeycardPermissions[]
            {
              KeycardPermissions.ContainmentLevelOne,
              KeycardPermissions.ArmoryLevelTwo,
              KeycardPermissions.Intercom,
            }
            },
        };

        [Description("In this list you can add a special door that can only be opened with the cards you specify.")]
        public Dictionary<DoorType, ItemType[]> SpecialDoorList { get; set; } = new()
        {
            {
            DoorType.LczWc,
            new ItemType[]
            { 
                ItemType.KeycardJanitor,
                ItemType.KeycardGuard
            }
            },
            {
            DoorType.GateA,
            new ItemType[]
            { 
                ItemType.KeycardChaosInsurgency
            }
            },
        };

        [Description("Example Types you can add")]
        public ItemType[] KeycardTypes { get; set; } =
        {
        ItemType.KeycardJanitor,
        ItemType.KeycardScientist,
        ItemType.KeycardResearchCoordinator,
        ItemType.KeycardZoneManager,
        ItemType.KeycardGuard,
        ItemType.KeycardMTFPrivate,
        ItemType.KeycardContainmentEngineer,
        ItemType.KeycardMTFOperative,
        ItemType.KeycardMTFCaptain,
        ItemType.KeycardFacilityManager,
        ItemType.KeycardChaosInsurgency,
        ItemType.KeycardO5
        };

        public KeycardPermissions[] KeycardPermissionsExp { get; set; } =
        {
        KeycardPermissions.Checkpoints,          // 1: Opens checkpoints
        KeycardPermissions.ExitGates,            // 2: Opens Gate A and Gate B
        KeycardPermissions.Intercom,             // 4: Opens the Intercom door
        KeycardPermissions.AlphaWarhead,         // 8: Opens the Alpha Warhead detonation button on surface
        KeycardPermissions.ContainmentLevelOne,  // 0x10: Opens Scp914Gate
        KeycardPermissions.ContainmentLevelTwo,  // 0x20: Opens Containment Level Two areas
        KeycardPermissions.ContainmentLevelThree,// 0x40: Opens Containment Level Three areas
        KeycardPermissions.ArmoryLevelOne,       // 0x80: Opens Light Containment armory
        KeycardPermissions.ArmoryLevelTwo,       // 0x100: Opens Heavy Containment armories
        KeycardPermissions.ArmoryLevelThree,     // 0x200: Opens MicroHID room
        KeycardPermissions.ScpOverride           // 0x400: Override SCP-specific doors
        };

        public DoorType[] DoorTypeExp { get; set; } =
        {
        DoorType.Scp914Door,
        DoorType.GR18Inner,
        DoorType.Scp049Gate,
        DoorType.Scp049Armory,
        DoorType.Scp079First,
        DoorType.Scp079Second,
        DoorType.Scp096,
        DoorType.Scp079Armory,
        DoorType.Scp106Primary,
        DoorType.Scp106Secondary,
        DoorType.Scp173Gate,
        DoorType.Scp173Connector,
        DoorType.Scp173Armory,
        DoorType.Scp173Bottom,
        DoorType.GR18Gate,
        DoorType.Scp914Gate,
        DoorType.Scp939Cryo,
        DoorType.CheckpointLczA,
        DoorType.CheckpointLczB,
        DoorType.EntranceDoor,
        DoorType.EscapePrimary,
        DoorType.EscapeSecondary,
        DoorType.ServersBottom,
        DoorType.GateA,
        DoorType.GateB,
        DoorType.HczArmory,
        DoorType.HeavyContainmentDoor,
        DoorType.HID,
        DoorType.HIDLeft,
        DoorType.HIDRight,
        DoorType.Intercom,
        DoorType.LczArmory,
        DoorType.LczCafe,
        DoorType.LczWc,
        DoorType.LightContainmentDoor,
        DoorType.NukeArmory,
        DoorType.NukeSurface,
        DoorType.PrisonDoor,
        DoorType.SurfaceGate,
        DoorType.Scp330,
        DoorType.Scp330Chamber,
        DoorType.CheckpointGate,
        DoorType.SurfaceDoor,
        DoorType.CheckpointEzHczA,
        DoorType.CheckpointEzHczB,
        DoorType.UnknownGate,
        DoorType.UnknownElevator,
        DoorType.ElevatorGateA,
        DoorType.ElevatorGateB,
        DoorType.ElevatorNuke,
        DoorType.ElevatorScp049,
        DoorType.ElevatorLczA,
        DoorType.ElevatorLczB,
        DoorType.CheckpointArmoryA,
        DoorType.CheckpointArmoryB,
        DoorType.Airlock,
        DoorType.Scp173NewGate
        };
    }
}
