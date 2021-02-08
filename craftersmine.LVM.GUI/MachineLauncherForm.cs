using craftersmine.LVM.Core;
using craftersmine.LVM.Core.Components;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Screen = craftersmine.LVM.Core.Components.Screen;

namespace craftersmine.LVM.GUI
{
    public partial class MachineLauncherForm : Form
    {
        public MachineLauncherForm()
        {
            InitializeComponent();
            MachineEvents.MachineHalted += MachineEvents_MachineHalted;

            UpdateMachines();
        }

        private void MachineEvents_MachineHalted(object sender, MachineHaltedEventArgs e)
        {

            if (InvokeRequired)
                Invoke(new Action(() => { this.WindowState = FormWindowState.Normal; }));
            else { this.WindowState = FormWindowState.Normal; }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // TODO: Remove method

            string machineDir = "D:\\TestLVMMachine";
            Machine machine = Machine.LoadMachine(machineDir);
            var machForm = new MachineForm(machine);
            WindowState = FormWindowState.Minimized;
            machForm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // TODO: Remove method

            using (var createForm = new MachineCreateForm())
            {
                createForm.ShowDialog();
                Machine createdMachine = createForm.CreatedMachine;
                createdMachine.SaveMachine();
                if (craftersmine.LVM.GUI.Properties.Settings.Default.AddedMachines == null)
                    craftersmine.LVM.GUI.Properties.Settings.Default.AddedMachines = new System.Collections.Specialized.StringCollection();
                craftersmine.LVM.GUI.Properties.Settings.Default.AddedMachines.Add(createdMachine.MachineRootDirectory);
                craftersmine.LVM.GUI.Properties.Settings.Default.Save();
                UpdateMachines();
            }
        }

        private void UpdateMachines()
        {
            List<string> removePending = new List<string>();
            machines.Items.Clear();
            if (craftersmine.LVM.GUI.Properties.Settings.Default.AddedMachines != null)
                foreach (var m in craftersmine.LVM.GUI.Properties.Settings.Default.AddedMachines)
                {
                    try
                    {
                        var machine = Machine.LoadMachine(m);
                        machines.Items.Add(new ListViewItem() { Text = machine.MachineName, ImageKey = "default", Tag = machine.MachineRootDirectory });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + ": \"" + m + "\"", "Error loading machine!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        removePending.Add(m);
                    }
                }

            foreach (var rm in removePending)
            {
                craftersmine.LVM.GUI.Properties.Settings.Default.AddedMachines.Remove(rm);
            }
            craftersmine.LVM.GUI.Properties.Settings.Default.Save();
        }

        private void run_Click(object sender, EventArgs e)
        {
            if (machines.SelectedItems.Count > 0)
            {
                string machineDir = (string)machines.SelectedItems[0].Tag;
            }
            
        }
    }
}
