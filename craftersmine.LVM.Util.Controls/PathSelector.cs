using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace craftersmine.LVM.Util.Controls
{
    public partial class PathSelector : UserControl
    {
        public SelectorType SelectorType { get; set; }
        public string FileFilter { get; set; }
        public string DialogTitle { get; set; }
        public string SelectedPath { get { return path.Text; } set { path.Text = value; } }

        public PathSelector()
        {
            InitializeComponent();
        }

        private void select_Click(object sender, EventArgs e)
        {
            switch (SelectorType)
            {
                case SelectorType.File:
                    using (OpenFileDialog dlg = new OpenFileDialog())
                    {
                        dlg.Filter = FileFilter;
                        dlg.Title = DialogTitle;
                        switch (dlg.ShowDialog())
                        {
                            case DialogResult.OK:
                                SelectedPath = dlg.FileName;
                                path.Text = SelectedPath;
                                break;
                        }
                    }
                    break;
                case SelectorType.Folder:
                    using (FolderBrowserDialog dlg = new FolderBrowserDialog())
                    {
                        dlg.Description = DialogTitle;
                        switch (dlg.ShowDialog())
                        {
                            case DialogResult.OK:
                                SelectedPath = dlg.SelectedPath;
                                path.Text = SelectedPath;
                                break;
                        }
                    }
                    break;
            }
        }
    }

    public enum SelectorType 
    {
        Folder,
        File
    }
}
