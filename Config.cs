using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.ComponentModel;
using Exiled.API.Extensions;
using System.Collections.Generic;

namespace SpecialKeycardPermissions
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Should you have the keycard in hand?")]
        public bool HeldKeycard { get; set; } = false;

        [Description("(FOR NOW ITS DISABLED)This list shows which card will change your permissions.")]
        public Dictionary<ItemType, KeycardPermissions> SpecialPermission { get; set; } = new Dictionary<ItemType, KeycardPermissions>
        {
            {ItemType.KeycardJanitor, KeycardPermissions.ContainmentLevelOne | KeycardPermissions.ArmoryLevelTwo | KeycardPermissions.Intercom},
        };

        [Description("This list shows which Doors with id will change your permissions.")]
        public Dictionary<byte, KeycardPermissions> SpecialDoorIds { get; set; } = [];

        [Description("This list shows which Doors with Type will change your permissions.")]
        public Dictionary<DoorType, KeycardPermissions> SpecialDoorTypes { get; set; } = new Dictionary<DoorType, KeycardPermissions>
        {
            {DoorType.EscapeFinal , KeycardPermissions.ArmoryLevelThree | KeycardPermissions.AlphaWarhead},
        };

        [Description("In this list you can add a special door that can only be opened with the cards you specify.")]
        public Dictionary<DoorType, ItemType[]> SpecialDoorList { get; set; } = new()
        {
            [DoorType.LczWc] = new[]
            {
                ItemType.KeycardGuard,
                ItemType.KeycardJanitor,
                ItemType.KeycardScientist,
            },

            [DoorType.GateA] = new[]
            {
                ItemType.KeycardChaosInsurgency
            },
        };

        [Description("In this list you can add a special doorid that can only be opened with the cards you specify.")]
        public Dictionary<byte, ItemType[]> SpecialDoorIdList { get; set; } = new()
        {
            [2] = new[]
            {
                ItemType.KeycardJanitor,
                ItemType.KeycardGuard
            },

            [3] = new[]
            {
                ItemType.KeycardChaosInsurgency
            },
        };

        public DoorType[] DoorTypeExp { get; set; } = [.. Enum.GetValues(typeof(DoorType)).Cast<DoorType>()];
        public ItemType[] KeycardTypeExp { get; set; } = [.. Enum.GetValues(typeof(ItemType)).Cast<ItemType>().Where(x => x.ToString().Contains("Keycard"))];
        public KeycardPermissions[] KeycardPermissionsExp { get; set; } = [.. Enum.GetValues(typeof(KeycardPermissions)).Cast<KeycardPermissions>()];
    }
}
