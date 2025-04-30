using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SpecialKeycardPermissions
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Should you have the keycard in hand?")]
        public bool HeldKeycard { get; set; } = false;

        [Description("This list shows which card will change your permissions.")]
        public Dictionary<ItemType, KeycardPermissions> SpecialPermission { get; set; } = new Dictionary<ItemType, KeycardPermissions>
        {
            {ItemType.KeycardJanitor, KeycardPermissions.ContainmentLevelOne | KeycardPermissions.ArmoryLevelTwo | KeycardPermissions.Intercom},
        };

        [Description("This list shows which Doors with id will change your permissions.")]
        public Dictionary<byte, KeycardPermissions> SpecialDoorIds { get; set; } = new Dictionary<byte, KeycardPermissions>
        {
            { 122, KeycardPermissions.Checkpoints | KeycardPermissions.AlphaWarhead },
            { 123, KeycardPermissions.Checkpoints | KeycardPermissions.AlphaWarhead },
            { 124, KeycardPermissions.Checkpoints | KeycardPermissions.AlphaWarhead },
            { 125, KeycardPermissions.Checkpoints | KeycardPermissions.AlphaWarhead },
        }; 
        [Description("This list shows which Doors with Type will change your permissions.")]
        public Dictionary<DoorType, KeycardPermissions> SpecialDoorTypes { get; set; } = new Dictionary<DoorType, KeycardPermissions> 
        {
            {DoorType.Airlock , KeycardPermissions.ArmoryLevelOne | KeycardPermissions.Intercom},
            {DoorType.EscapeFinal , KeycardPermissions.ArmoryLevelOne | KeycardPermissions.Intercom},
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
        [Description("In this list you can add a special doorid that can only be opened with the cards you specify.")]
        public Dictionary<byte, ItemType[]> SpecialDoorIdList { get; set; } = new()
        {
            {
            2,
            new ItemType[]
                {
                ItemType.KeycardJanitor,
                ItemType.KeycardGuard
                }
            },
            {
            3,
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

        public KeycardPermissions[] KeycardPermissionsExp { get; set; } = [.. Enum.GetValues(typeof(KeycardPermissions)).Cast<KeycardPermissions>()];

        public DoorType[] DoorTypeExp { get; set; } = [.. Enum.GetValues(typeof(DoorType)).Cast<DoorType>()];
    }
}
