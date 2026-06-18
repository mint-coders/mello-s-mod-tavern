namespace BananaManagerInstallerApp
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblReleases = new System.Windows.Forms.Label();
            this.lstReleases = new System.Windows.Forms.ListBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblDirectoryPrompt = new System.Windows.Forms.Label();
            this.txtTargetDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkDesktopShortcut = new System.Windows.Forms.CheckBox();
            this.btnInstall = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();

            // pnlSidebar
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(255, 223, 0);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(175, 520);
            this.pnlSidebar.TabIndex = 0;
            this.pnlSidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSidebar_Paint);

            // pnlContent
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.lblStatus);
            this.pnlContent.Controls.Add(this.progressBar);
            this.pnlContent.Controls.Add(this.chkDesktopShortcut);
            this.pnlContent.Controls.Add(this.btnInstall);
            this.pnlContent.Controls.Add(this.btnBrowse);
            this.pnlContent.Controls.Add(this.txtTargetDirectory);
            this.pnlContent.Controls.Add(this.lblDirectoryPrompt);
            this.pnlContent.Controls.Add(this.btnRefresh);
            this.pnlContent.Controls.Add(this.lstReleases);
            this.pnlContent.Controls.Add(this.lblReleases);
            this.pnlContent.Controls.Add(this.lblTitle);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(175, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContent.Size = new System.Drawing.Size(525, 520);
            this.pnlContent.TabIndex = 1;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(344, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Banana Manger Installer";

            // lblReleases
            this.lblReleases.AutoSize = true;
            this.lblReleases.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblReleases.Location = new System.Drawing.Point(20, 70);
            this.lblReleases.Name = "lblReleases";
            this.lblReleases.Size = new System.Drawing.Size(80, 23);
            this.lblReleases.TabIndex = 1;
            this.lblReleases.Text = "Releases:";

            // lstReleases
            this.lstReleases.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstReleases.FormattingEnabled = true;
            this.lstReleases.ItemHeight = 23;
            this.lstReleases.Location = new System.Drawing.Point(20, 100);
            this.lstReleases.Name = "lstReleases";
            this.lstReleases.Size = new System.Drawing.Size(400, 88);
            this.lstReleases.TabIndex = 2;
            this.lstReleases.SelectedIndexChanged += new System.EventHandler(this.lstReleases_SelectedIndexChanged);

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(255, 223, 0);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.Location = new System.Drawing.Point(430, 100);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 30);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // lblDirectoryPrompt
            this.lblDirectoryPrompt.AutoSize = true;
            this.lblDirectoryPrompt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDirectoryPrompt.Location = new System.Drawing.Point(20, 210);
            this.lblDirectoryPrompt.Name = "lblDirectoryPrompt";
            this.lblDirectoryPrompt.Size = new System.Drawing.Size(151, 23);
            this.lblDirectoryPrompt.TabIndex = 4;
            this.lblDirectoryPrompt.Text = "Target Directory:";

            // txtTargetDirectory
            this.txtTargetDirectory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTargetDirectory.Location = new System.Drawing.Point(20, 240);
            this.txtTargetDirectory.Name = "txtTargetDirectory";
            this.txtTargetDirectory.Size = new System.Drawing.Size(385, 30);
            this.txtTargetDirectory.TabIndex = 5;

            // btnBrowse
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(255, 223, 0);
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowse.Location = new System.Drawing.Point(410, 240);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(95, 30);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);

            // chkDesktopShortcut
            this.chkDesktopShortcut.AutoSize = true;
            this.chkDesktopShortcut.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkDesktopShortcut.Location = new System.Drawing.Point(20, 290);
            this.chkDesktopShortcut.Name = "chkDesktopShortcut";
            this.chkDesktopShortcut.Size = new System.Drawing.Size(211, 27);
            this.chkDesktopShortcut.TabIndex = 7;
            this.chkDesktopShortcut.Text = "Make a desktop shortcut";
            this.chkDesktopShortcut.UseVisualStyleBackColor = true;
            this.chkDesktopShortcut.Checked = true;

            // btnInstall
            this.btnInstall.BackColor = System.Drawing.Color.FromArgb(255, 223, 0);
            this.btnInstall.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnInstall.ForeColor = System.Drawing.Color.Black;
            this.btnInstall.Location = new System.Drawing.Point(20, 350);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(485, 60);
            this.btnInstall.TabIndex = 8;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = false;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);

            // progressBar
            this.progressBar.Location = new System.Drawing.Point(20, 420);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(485, 25);
            this.progressBar.TabIndex = 9;
            this.progressBar.Visible = false;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.Location = new System.Drawing.Point(20, 460);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 21);
            this.lblStatus.TabIndex = 10;

            // MainWindow
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 520);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Banana Manger Installer";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblReleases;
        private System.Windows.Forms.ListBox lstReleases;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblDirectoryPrompt;
        private System.Windows.Forms.TextBox txtTargetDirectory;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox chkDesktopShortcut;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
