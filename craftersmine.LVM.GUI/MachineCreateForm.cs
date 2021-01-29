using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace craftersmine.LVM.GUI
{
    public partial class MachineCreateForm : Form
    {
        public MachineCreateForm()
        {
            InitializeComponent();
        }

        private void MachineCreateForm_Load(object sender, EventArgs e)
        {

        }

        private void addDev_Click(object sender, EventArgs e)
        {
            var selForm = new DeviceTypeSelectForm();
            selForm.ShowDialog();
            selForm.Dispose();
        }
    }
}
