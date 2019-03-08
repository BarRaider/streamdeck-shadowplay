using BarRaider.SdTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace Shadowplay.Actions
{
    public abstract class ActionBase : PluginBase 
    {
        protected string RegistryValue { get; private set; }
        protected int[]  FallbackHotkey { get; private set; }

        protected RegistryHandler rh = new RegistryHandler();
        protected InputSimulator iis = new InputSimulator();

        public ActionBase(SDConnection connection, InitialPayload payload, string registryValue, int[] fallbackHotkey) : base(connection, payload)
        {
            RegistryValue = registryValue;
            FallbackHotkey = fallbackHotkey;
        }

        public override void KeyPressed(KeyPayload payload)
        {
            try
            {
                Logger.Instance.LogMessage(TracingLevel.INFO, $"Key Pressed {RegistryValue}");
                VirtualKeyCode keyCode;
                var keyStrokes = GetKeyStrokes();

                // Move the last stroke to keyCode
                keyCode = keyStrokes.Last();
                keyStrokes.Remove(keyCode);
                iis.Keyboard.ModifiedKeyStroke(keyStrokes.ToArray(), keyCode);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, $"KeyPress Exception: {ex}");
            }
        }

        public List<VirtualKeyCode> GetKeyStrokes()
        {
            List<VirtualKeyCode> modifierKeys = new List<VirtualKeyCode>();
            var strokes = rh.GetCommandKeystrokes(RegistryValue);
            if (strokes == null || strokes.Length == 0)
            {
                strokes = FallbackHotkey;
            }
            foreach (int stroke in strokes)
            {
                modifierKeys.Add((VirtualKeyCode)stroke);
            }

            return modifierKeys;
        }
    }
}
