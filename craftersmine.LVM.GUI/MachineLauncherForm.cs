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
                Properties.Settings.Default.AddedMachines.Add(createdMachine.MachineRootDirectory);
                UpdateMachines();
            }
        }

        private void UpdateMachines()
        {
            machines.Items.Clear();
            foreach (var m in Properties.Settings.Default.AddedMachines)
            {
                var machine = Machine.LoadMachine(m);
                machines.Items.Add(new ListViewItem() { Text = machine.MachineName, ImageKey = "default", Tag = machine });
            }
        }
    }
}
