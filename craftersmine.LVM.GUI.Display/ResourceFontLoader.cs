using craftersmine.LVM.GUI.Display.Properties;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.GUI.Display
{
    /// <summary>
    /// Provides static methods to load font from byte array
    /// </summary>
    public static class ResourceFontLoader
    {
        /// <summary>
        /// Loads font from byte array
        /// </summary>
        /// <param name="data">Byte array containing font data</param>
        /// <returns><see cref="PrivateFontCollection"/> containing fonts</returns>
        public static PrivateFontCollection AddFontFromMemory(byte[] data)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();

            unsafe
            {
                fixed (byte* pFontData = data)
                {
                    pfc.AddMemoryFont((System.IntPtr)pFontData, data.Length);
                }
            }

            return pfc;
        }
    }
}
