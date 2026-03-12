using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace FalconUtils
{
    public class FalconUtils : Plugin<Config>
    {
        public override string Name => "FalconUtils";

        public override string Description => "Un plugin donde tendra muchos plugins utiles para el server de FalconCommunity";
        public override string Author => "AdrianoElAldeano";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredApiVersion => new Version(LabApiProperties.CompiledVersion);
        private EventHandler EventHandler = new EventHandler();
        private Sup_InvHandler Sup_InvHandler;
        
        public override void Enable()
        {
            Sup_InvHandler = new Sup_InvHandler(this);
            CustomHandlersManager.RegisterEventsHandler(EventHandler);
            CustomHandlersManager.RegisterEventsHandler(Sup_InvHandler);
        }

        public override void Disable()
        {
            CustomHandlersManager.UnregisterEventsHandler(EventHandler);
            CustomHandlersManager.UnregisterEventsHandler(Sup_InvHandler);
        }
    }
}