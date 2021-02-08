
namespace craftersmine.LVM.Core.Configurators
{
    partial class MachineConfigurator
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.machineRootDir = new craftersmine.LVM.Util.Controls.PathSelector();
            this.label2 = new System.Windows.Forms.Label();
            this.machineName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.machineRootDir);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.machineName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Machine Configuration";
            // 
            // machineRootDir
            // 
            this.machineRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.machineRootDir.DialogTitle = "Select machine root directory";
            this.machineRootDir.FileFilter = null;
            this.machineRootDir.Location = new System.Drawing.Point(6, 74);
            this.machineRootDir.Name = "machineRootDir";
            this.machineRootDir.SelectedPath = "";
            this.machineRootDir.SelectorType = craftersmine.LVM.Util.Controls.SelectorType.Folder;
            this.machineRootDir.Size = new System.Drawing.Size(408, 26);
            this.machineRootDir.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Machine Root Directory:";
            // 
            // machineName
            // 
            this.machineName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.machineName.Location = new System.Drawing.Point(9, 35);
            this.machineName.Name = "machineName";
            this.machineName.Size = new System.Drawing.Size(405, 20);
            this.machineName.TabIndex = 1;
            this.machineName.Text = "Unnamed Machine";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Machine Name:";
            // 
            // MachineConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "MachineConfigurator";
            this.Size = new System.Drawing.Size(426, 110);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Util.Controls.PathSelector machineRootDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox machineName;
        private System.Windows.Forms.Label label1;
    }
}
