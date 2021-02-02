
namespace craftersmine.LVM.Core.Configurators
{
    partial class EEPROMConfigurator
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
            this.useCustomRom = new System.Windows.Forms.RadioButton();
            this.useInternalRom = new System.Windows.Forms.RadioButton();
            this.eepromPath = new craftersmine.LVM.Util.Controls.PathSelector();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.isReadOnly = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.eepromPath);
            this.groupBox1.Controls.Add(this.useCustomRom);
            this.groupBox1.Controls.Add(this.useInternalRom);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EEPROM Location";
            // 
            // useCustomRom
            // 
            this.useCustomRom.AutoSize = true;
            this.useCustomRom.Location = new System.Drawing.Point(6, 42);
            this.useCustomRom.Name = "useCustomRom";
            this.useCustomRom.Size = new System.Drawing.Size(110, 17);
            this.useCustomRom.TabIndex = 1;
            this.useCustomRom.Text = "Use Custom ROM";
            this.useCustomRom.UseVisualStyleBackColor = true;
            this.useCustomRom.CheckedChanged += new System.EventHandler(this.useCustomRom_CheckedChanged);
            // 
            // useInternalRom
            // 
            this.useInternalRom.AutoSize = true;
            this.useInternalRom.Checked = true;
            this.useInternalRom.Location = new System.Drawing.Point(6, 19);
            this.useInternalRom.Name = "useInternalRom";
            this.useInternalRom.Size = new System.Drawing.Size(110, 17);
            this.useInternalRom.TabIndex = 0;
            this.useInternalRom.TabStop = true;
            this.useInternalRom.Text = "Use Internal ROM";
            this.useInternalRom.UseVisualStyleBackColor = true;
            this.useInternalRom.CheckedChanged += new System.EventHandler(this.useInternalRom_CheckedChanged);
            // 
            // eepromPath
            // 
            this.eepromPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eepromPath.DialogTitle = "Select Lua Machine ROM file";
            this.eepromPath.Enabled = false;
            this.eepromPath.FileFilter = "Lua Source Files (*.lua)|*.lua|LVM ROM Files (*.lvr)|*.lvr|All Supported Files (*" +
    ".lua, *.lvr)|*.lua;*.lvr|All Files (*.*)|*.*";
            this.eepromPath.Location = new System.Drawing.Point(6, 64);
            this.eepromPath.Name = "eepromPath";
            this.eepromPath.SelectedPath = null;
            this.eepromPath.SelectorType = craftersmine.LVM.Util.Controls.SelectorType.File;
            this.eepromPath.Size = new System.Drawing.Size(438, 26);
            this.eepromPath.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.isReadOnly);
            this.groupBox2.Location = new System.Drawing.Point(3, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 44);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "EEPROM Settings";
            // 
            // isReadOnly
            // 
            this.isReadOnly.AutoSize = true;
            this.isReadOnly.Checked = true;
            this.isReadOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isReadOnly.Location = new System.Drawing.Point(6, 19);
            this.isReadOnly.Name = "isReadOnly";
            this.isReadOnly.Size = new System.Drawing.Size(144, 17);
            this.isReadOnly.TabIndex = 3;
            this.isReadOnly.Text = "Set EEPROM Read-Only";
            this.isReadOnly.UseVisualStyleBackColor = true;
            // 
            // EEPROMConfigurator
            // 
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "EEPROMConfigurator";
            this.Size = new System.Drawing.Size(456, 145);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton useCustomRom;
        private System.Windows.Forms.RadioButton useInternalRom;
        private Util.Controls.PathSelector eepromPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox isReadOnly;
    }
}
