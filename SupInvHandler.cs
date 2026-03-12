using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Loader.Features.Plugins;
using PlayerRoles;
using Random = System.Random;

namespace FalconUtils;

public class Sup_InvHandler : CustomEventsHandler
{
    private static readonly Random random = new Random();

    private readonly FalconUtils plugin;
    public Sup_InvHandler(FalconUtils plugin) => this.plugin = plugin;

    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        switch (ev.NewRole.RoleTypeId)
        {
            case RoleTypeId.Scientist:
                
                if (random.Next(1, 101) <= plugin.Config.SuccessChance)
                {
                    ev.Player.AddItem(ItemType.KeycardResearchCoordinator);
                    ev.Player.RemoveItem(ItemType.KeycardScientist);
                }
                break;
        }
    }
}