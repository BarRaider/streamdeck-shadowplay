using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BarRaider.SdTools;
using WindowsInput.Native;

namespace Shadowplay.Actions
{
    [PluginActionId("com.barraider.shadowplay.ptt")]
    public class MicrophonePTTAction : ActionBase
    {
        private const string REGISTRY_COMMAND_KEY = "MicPTTHKey";
        private static readonly int[] FALLBACK_HOTKEY = { 192 };
        bool keyReleased = false;

        public MicrophonePTTAction(SDConnection connection, InitialPayload payload) : base(connection, payload, REGISTRY_COMMAND_KEY, FALLBACK_HOTKEY)
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Plugin loaded {this.GetType()}");
        }

        public override void Dispose()
        {
        }

        public override void KeyReleased(KeyPayload payload)
        {
            keyReleased = true;
        }

        public override void KeyPressed(KeyPayload payload)
        {
            try
            {
                Logger.Instance.LogMessage(TracingLevel.INFO, $"Key Pressed {RegistryValue}");
                keyReleased = false;
                VirtualKeyCode keyCode;
                var keyStrokes = GetKeyStrokes();
                
                // Move the last stroke to keyCode
                keyCode = keyStrokes.Last();
                Task.Run(() => SimulateKeyDown(keyCode));
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, $"PTT KeyPress Exception: {ex}");
            }
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

        private void SimulateKeyDown(VirtualKeyCode keyCode)
        {
            while (!keyReleased)
            {
                iis.Keyboard.KeyDown(keyCode);
                Thread.Sleep(30);
            }
        }
    }
}
