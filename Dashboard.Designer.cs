namespace BloodBank_Video
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.คนหาBloodDonorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDonerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewDonorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allDonorDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bloodGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.เพมจำนวนเลอดToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ลดจำนวนเลอดToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allBloodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDonorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightPink;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dToolStripMenuItem,
            this.คนหาBloodDonorToolStripMenuItem,
            this.stockToolStripMenuItem,
            this.deleteDonerToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1223, 68);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewDonorToolStripMenuItem,
            this.updateDetailsToolStripMenuItem,
            this.allDonorDetailsToolStripMenuItem});
            this.dToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dToolStripMenuItem.Image")));
            this.dToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(146, 64);
            this.dToolStripMenuItem.Text = "Donor";
            this.dToolStripMenuItem.Click += new System.EventHandler(this.dToolStripMenuItem_Click);
            // 
            // คนหาBloodDonorToolStripMenuItem
            // 
            this.คนหาBloodDonorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locationToolStripMenuItem,
            this.bloodGroupToolStripMenuItem});
            this.คนหาBloodDonorToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.คนหาBloodDonorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("คนหาBloodDonorToolStripMenuItem.Image")));
            this.คนหาBloodDonorToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.คนหาBloodDonorToolStripMenuItem.Name = "คนหาBloodDonorToolStripMenuItem";
            this.คนหาBloodDonorToolStripMenuItem.Size = new System.Drawing.Size(225, 64);
            this.คนหาBloodDonorToolStripMenuItem.Text = "ค้นหา Blood Donor";
            // 
            // stockToolStripMenuItem
            // 
            this.stockToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.เพมจำนวนเลอดToolStripMenuItem,
            this.ลดจำนวนเลอดToolStripMenuItem,
            this.allBloodToolStripMenuItem});
            this.stockToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stockToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stockToolStripMenuItem.Image")));
            this.stockToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stockToolStripMenuItem.Name = "stockToolStripMenuItem";
            this.stockToolStripMenuItem.Size = new System.Drawing.Size(128, 64);
            this.stockToolStripMenuItem.Text = "Stock";
            // 
            // deleteDonerToolStripMenuItem
            // 
            this.deleteDonerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteDonorToolStripMenuItem});
            this.deleteDonerToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteDonerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteDonerToolStripMenuItem.Image")));
            this.deleteDonerToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.deleteDonerToolStripMenuItem.Name = "deleteDonerToolStripMenuItem";
            this.deleteDonerToolStripMenuItem.Size = new System.Drawing.Size(180, 64);
            this.deleteDonerToolStripMenuItem.Text = "Delete Doner";
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logOutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("logOutToolStripMenuItem.Image")));
            this.logOutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(149, 64);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // addNewDonorToolStripMenuItem
            // 
            this.addNewDonorToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addNewDonorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewDonorToolStripMenuItem.Image")));
            this.addNewDonorToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addNewDonorToolStripMenuItem.Name = "addNewDonorToolStripMenuItem";
            this.addNewDonorToolStripMenuItem.Size = new System.Drawing.Size(263, 32);
            this.addNewDonorToolStripMenuItem.Text = "Add New Donor";
            // 
            // updateDetailsToolStripMenuItem
            // 
            this.updateDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateDetailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("updateDetailsToolStripMenuItem.Image")));
            this.updateDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.updateDetailsToolStripMenuItem.Name = "updateDetailsToolStripMenuItem";
            this.updateDetailsToolStripMenuItem.Size = new System.Drawing.Size(263, 32);
            this.updateDetailsToolStripMenuItem.Text = "Update Details";
            this.updateDetailsToolStripMenuItem.Click += new System.EventHandler(this.updateDetailsToolStripMenuItem_Click);
            // 
            // allDonorDetailsToolStripMenuItem
            // 
            this.allDonorDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allDonorDetailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("allDonorDetailsToolStripMenuItem.Image")));
            this.allDonorDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.allDonorDetailsToolStripMenuItem.Name = "allDonorDetailsToolStripMenuItem";
            this.allDonorDetailsToolStripMenuItem.Size = new System.Drawing.Size(263, 32);
            this.allDonorDetailsToolStripMenuItem.Text = "All Donor Details";
            // 
            // locationToolStripMenuItem
            // 
            this.locationToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.locationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("locationToolStripMenuItem.Image")));
            this.locationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.locationToolStripMenuItem.Name = "locationToolStripMenuItem";
            this.locationToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.locationToolStripMenuItem.Text = "Location";
            // 
            // bloodGroupToolStripMenuItem
            // 
            this.bloodGroupToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bloodGroupToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bloodGroupToolStripMenuItem.Image")));
            this.bloodGroupToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bloodGroupToolStripMenuItem.Name = "bloodGroupToolStripMenuItem";
            this.bloodGroupToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.bloodGroupToolStripMenuItem.Text = "Blood Group";
            // 
            // เพมจำนวนเลอดToolStripMenuItem
            // 
            this.เพมจำนวนเลอดToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.เพมจำนวนเลอดToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("เพมจำนวนเลอดToolStripMenuItem.Image")));
            this.เพมจำนวนเลอดToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.เพมจำนวนเลอดToolStripMenuItem.Name = "เพมจำนวนเลอดToolStripMenuItem";
            this.เพมจำนวนเลอดToolStripMenuItem.Size = new System.Drawing.Size(237, 32);
            this.เพมจำนวนเลอดToolStripMenuItem.Text = "เพิ่มจำนวนเลือด";
            // 
            // ลดจำนวนเลอดToolStripMenuItem
            // 
            this.ลดจำนวนเลอดToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ลดจำนวนเลอดToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ลดจำนวนเลอดToolStripMenuItem.Image")));
            this.ลดจำนวนเลอดToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ลดจำนวนเลอดToolStripMenuItem.Name = "ลดจำนวนเลอดToolStripMenuItem";
            this.ลดจำนวนเลอดToolStripMenuItem.Size = new System.Drawing.Size(237, 32);
            this.ลดจำนวนเลอดToolStripMenuItem.Text = "ลดจำนวนเลือด";
            // 
            // allBloodToolStripMenuItem
            // 
            this.allBloodToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allBloodToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("allBloodToolStripMenuItem.Image")));
            this.allBloodToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.allBloodToolStripMenuItem.Name = "allBloodToolStripMenuItem";
            this.allBloodToolStripMenuItem.Size = new System.Drawing.Size(237, 32);
            this.allBloodToolStripMenuItem.Text = "All Blood ";
            // 
            // deleteDonorToolStripMenuItem
            // 
            this.deleteDonorToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteDonorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteDonorToolStripMenuItem.Image")));
            this.deleteDonorToolStripMenuItem.Name = "deleteDonorToolStripMenuItem";
            this.deleteDonorToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.deleteDonorToolStripMenuItem.Text = "Delete Donor";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1223, 525);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem คนหาBloodDonorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDonerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewDonorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allDonorDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bloodGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem เพมจำนวนเลอดToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ลดจำนวนเลอดToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allBloodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDonorToolStripMenuItem;
    }
}