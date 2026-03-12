using System;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Loader.Features.Plugins;
using PlayerRoles;

namespace FalconUtils;

public class EventHandler : CustomEventsHandler
{
    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
       if (ev.NewRole.RoleTypeId == RoleTypeId.FacilityGuard)
        {
            ev.Player.AddAmmo(ItemType.Ammo9x19, 90);
        }
    }
}