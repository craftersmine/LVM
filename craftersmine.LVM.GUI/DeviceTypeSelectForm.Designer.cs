
namespace craftersmine.LVM.GUI
{
    partial class DeviceTypeSelectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.devList = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.icons = new System.Windows.Forms.ImageList(this.components);
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // devList
            // 
            this.devList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name});
            this.devList.FullRowSelect = true;
            this.devList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.devList.HideSelection = false;
            this.devList.Location = new System.Drawing.Point(12, 12);
            this.devList.MultiSelect = false;
            this.devList.Name = "devList";
            this.devList.Size = new System.Drawing.Size(299, 421);
            this.devList.SmallImageList = this.icons;
            this.devList.TabIndex = 0;
            this.devList.UseCompatibleStateImageBehavior = false;
            this.devList.View = System.Windows.Forms.View.Details;
            // 
            // name
            // 
            this.name.Text = "Device Type Name";
            this.name.Width = 295;
            // 
            // icons
            // 
            this.icons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.icons.ImageSize = new System.Drawing.Size(16, 16);
            this.icons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ok
            // 
            this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok.Location = new System.Drawing.Point(211, 439);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(100, 23);
            this.ok.TabIndex = 1;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(110, 439);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(95, 23);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // DeviceTypeSelectForm
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(323, 474);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.devList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DeviceTypeSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DeviceTypeSelectForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView devList;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ImageList icons;
    }
}