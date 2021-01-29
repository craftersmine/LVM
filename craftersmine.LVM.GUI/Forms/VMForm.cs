using craftersmine.LVM.Core;
using craftersmine.LVM.Core.VM;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace craftersmine.LVM.GUI.Forms
{
    public partial class VMForm : Form
    {
        private string testStr = "Test string";
        private VM vm = new VM();

        public VMForm()
        {
            InitializeComponent();
            consoleDisplay1.SetFont(SettingsManager.DisplayFontManager.LoadedFont);
            vm.Run();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            consoleDisplay1.Initialize();
            for (int i = 0; i < testStr.Length; i++)
            {
                consoleDisplay1.DrawChar(testStr[i], i, 0);
            }
            consoleDisplay1.SetForegroundColor(Color.Red);
            consoleDisplay1.SetBackgroundColor(Color.Blue);

            for (int i = 0; i < testStr.Length; i++)
            {
                consoleDisplay1.DrawChar(testStr[i], i + testStr.Length + 1, 0);
            }

            vm.Stop();
        }
    }
}
