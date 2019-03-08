using BarRaider.SdTools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowplay
{
    public class RegistryHandler
    {
        public const string REG_SHADOWPLAY = @"HKEY_CURRENT_USER\Software\NVIDIA Corporation\Global\ShadowPlay\NVSPCAPS";
        public const string REG_COUNT_PREFIX = "Count";

        public int[] GetCommandKeystrokes(string commandRegKey)
        {
            List<int> keyStrokes = new List<int>();
            int? strokes = ExtractRegistryIntValue(REG_SHADOWPLAY, commandRegKey + REG_COUNT_PREFIX);
            
            if (strokes != null)
            {
                for (int idx = 0; idx < strokes.Value; idx++)
                {
                    int? currStroke = ExtractRegistryIntValue(REG_SHADOWPLAY, commandRegKey + idx.ToString());
                    if (currStroke == null)
                    {
                        Logger.Instance.LogMessage(TracingLevel.ERROR, $"Could not complete keystroke for: {commandRegKey}. Invalid stroke number: {idx}");
                        return null;
                    }
                    keyStrokes.Add(currStroke.Value);
                }
            }
            return keyStrokes.ToArray();
        }

        private int? ExtractRegistryIntValue(string key, string value)
        {
            byte[] result = new byte[16];
            result = (byte[])Registry.GetValue(key, value, null);

            if (result == null)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, $"Registry value not found: {value}");
                return null;
            }
            return result[0];
        }
    }
}
