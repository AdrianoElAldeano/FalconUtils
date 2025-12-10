using System;
using System.Linq;
using CommandSystem;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Utilities;
using LabApi.Features.Wrappers;
using PlayerRoles;
using RemoteAdmin;
using Hint = HintServiceMeow.Core.Models.Hints.Hint;

namespace FalconUtils
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class AyudaCommand : ICommand
    {
        public string Command => "ayuda";
        public string[] Aliases => ["Ayuda"];
        public string Description => "Comando para ir a torre y dar un aviso al staff disponible para que te ayude.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            PlayerCommandSender playerSender = sender as PlayerCommandSender;
            Player player = Player.Get(playerSender.ReferenceHub);

            if (player != null && !(player.Role == RoleTypeId.Spectator))
            {
                response = "Debes ser espectador para usar el comando.";
                return false;
            }

            player.Role = RoleTypeId.Tutorial;
            
            AyudaData.JugadoresEnAyuda.Add(player.PlayerId);

            foreach (Player staffMember in Player.List.Where(p => p.RemoteAdminAccess))
            {
                Hint hint = new Hint();
                {
                    hint.Text = "Alguien solicito ayuda.";
                    hint.Alignment = HintAlignment.Center;
                    hint.YCoordinate = 250;
                    hint.FontSize = 40;
                    hint.HideAfter(8f);
                }
                PlayerDisplay playerDisplay = PlayerDisplay.Get(staffMember);
                playerDisplay.AddHint(hint);
            }
            Hint hintPlayer = new Hint();
            {
                hintPlayer.Text = "Si nadie acude usa '.volver'.";
                hintPlayer.Alignment = HintAlignment.Center;
                hintPlayer.YCoordinate = 400;
                hintPlayer.FontSize = 30;
                hintPlayer.HideAfter(8f);
            }
            PlayerDisplay pDisplay = PlayerDisplay.Get(player);
            pDisplay.AddHint(hintPlayer);
            response = "Has enviado una solicitud de ayuda al staff";
            return true;
        }
    }
}