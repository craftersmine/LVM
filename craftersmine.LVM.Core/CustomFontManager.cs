using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text;

namespace craftersmine.LVM.Core
{
    public sealed class CustomFontManager
    {
        private PrivateFontCollection pfc = new PrivateFontCollection();
        public Font LoadedFont { get; private set; }
        public string FontName { get { return LoadedFont.Name; } }
        public string FontFilePath { get; private set; }

        private CustomFontManager() { }
        public CustomFontManager(string path)
        {
            LoadFont(path, 8f, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        public CustomFontManager(string path, float size)
        {
            LoadFont(path, size, FontStyle.Regular, GraphicsUnit.Pixel);
        }
        public CustomFontManager(string path, float size, FontStyle style, GraphicsUnit graphicsUnit)
        {
            LoadFont(path, size, style, graphicsUnit);
        }

        public bool LoadFont(string path, float size, FontStyle style, GraphicsUnit graphicsUnit)
        {
            Logger.Instance.Log(LogEntryType.Info, "Loading font from \"" + path + "\"...");
            try
            {
                pfc.AddFontFile(path);
                LoadedFont = new Font(pfc.Families[0], size, style, graphicsUnit);
                Logger.Instance.Log(LogEntryType.Info, "Font from \"" + path + "\" successfully loaded!");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogEntryType.Error, "Error while loading font from \"" + path + "\"! Loading fallback font");
                Logger.Instance.LogException(LogEntryType.Error, ex);
                LoadedFont = new Font("Consolas", 8f);
                return false;
            }
        }
    }
}
