﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarRaider.SdTools;

namespace Shadowplay.Actions
{
    [PluginActionId("com.barraider.shadowplay.settings")]
    public class SettingsAction : ActionBase
    {
        private const string REGISTRY_COMMAND_KEY = "GFEOverlayHKeyV2";
        private static readonly int[] FALLBACK_HOTKEY = { 18, 90 };

        public SettingsAction(SDConnection connection, InitialPayload payload) : base(connection, payload, REGISTRY_COMMAND_KEY, FALLBACK_HOTKEY)
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Plugin loaded {this.GetType()}");
        }

        public override void Dispose()
        {
        }

        public override void KeyReleased(KeyPayload payload)
        {
        }

        public override void OnTick()
        {
        }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload)
        {
        }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
        }
    }
}
