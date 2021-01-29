using System;
using System.Collections.Generic;
using System.Text;

namespace craftersmine.LVM.Core
{
    public sealed class SettingsManager
    {
        public static bool EnableLogging { get; set; } = true;
        public static CustomFontManager DisplayFontManager { get; set; }
    }
}
