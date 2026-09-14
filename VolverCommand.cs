using System;
using System.Linq;
using CommandSystem;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using LabApi.Features.Wrappers;
using PlayerRoles;
using RemoteAdmin;

namespace FalconUtils
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class VolverCommand : ICommand
    {
        public string Command => "Volver";
        public string[] Aliases => null;
        public string Description => "Volver del .ayuda";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            PlayerCommandSender playerSender = sender as PlayerCommandSender;
            Player player = Player.Get(playerSender.ReferenceHub);

            if (player != null && !(player.Role == RoleTypeId.Tutorial))
            {
                response = "Debes de tener tutorial para usar este comando.";
                return false;
            }

            if (!AyudaData.JugadoresEnAyuda.Contains(player.PlayerId))
            {
                response = "Solo puedes usar '.volver' si usaste el comando '.ayuda'";
                return false;
            }

            player.Role = RoleTypeId.Spectator;
            
            foreach (Player staffMember in Player.List.Where(p => p.RemoteAdminAccess))
            {
                Hint hint = new Hint();
                {
                    hint.Text = "La solicitud de ayuda fue cancelado/terminado.";
                    hint.Alignment = HintAlignment.Center;
                    hint.YCoordinate = 250;
                    hint.FontSize = 40;
                    hint.HideAfter(8f);
                }
                PlayerDisplay playerDisplay = PlayerDisplay.Get(staffMember);
                playerDisplay.AddHint(hint);
            }

            response = "Has vuelto a ser espectador.";
            return true;
        }
    }
}