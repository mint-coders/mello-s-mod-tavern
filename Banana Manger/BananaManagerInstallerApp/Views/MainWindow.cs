using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using BananaManagerInstallerApp.Models;
using BananaManagerInstallerApp.ViewModels;
using IWshRuntimeLibrary;   

namespace BananaManagerInstallerApp
{
    public partial class MainWindow : Form
    {
        // GitHub repository details
        private const string GitHubOwner = "mint-coders";
        private const string GitHubRepo = "mello-s-mod-tavern";

        private ReleaseManager _releaseManager;
        private List<GitHubRelease> _availableReleases = new();
        private GitHubRelease? _selectedRelease;

        public MainWindow()
        {
            InitializeComponent();
            _releaseManager = new ReleaseManager(GitHubOwner, GitHubRepo);
            
            // Load icon
            try
            {
                string? baseDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (!string.IsNullOrEmpty(baseDir))
                {
                    string iconPath = System.IO.Path.Combine(baseDir, "Views", "Assets", "Installer.ico");
                    if (System.IO.File.Exists(iconPath))
                    {
                        this.Icon = new System.Drawing.Icon(iconPath);
                    }
                }
            }
            catch { }
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            // Load releases on startup
            await LoadReleasesAsync();
        }

        /// <summary>
        /// Loads available releases from GitHub
        /// </summary>
        private async Task LoadReleasesAsync()
        {
            lblStatus.Text = "Checking for available releases...";
            btnInstall.Enabled = false;
            lstReleases.Items.Clear();

            try
            {
                _availableReleases = await _releaseManager.GetReleasesAsync();

                if (_availableReleases.Count == 0)
                {
                    lblStatus.Text = "No releases found.";
                    return;
                }

                // Populate the listbox with releases
                foreach (var release in _availableReleases)
                {
                    lstReleases.Items.Add(release);
                }

                // Auto-select the first (latest) release
                if (_availableReleases.Count > 0)
                {
                    lstReleases.SelectedIndex = 0;
                    _selectedRelease = _availableReleases[0];
                    lblStatus.Text = $"Latest release: {_selectedRelease}";
                    btnInstall.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Failed to load releases.";
                MessageBox.Show($"Error loading releases:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles release selection change
        /// </summary>
        private void lstReleases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstReleases.SelectedIndex >= 0 && lstReleases.SelectedIndex < _availableReleases.Count)
            {
                _selectedRelease = _availableReleases[lstReleases.SelectedIndex];
                lblStatus.Text = $"Selected: {_selectedRelease}";
            }
        }

        /// <summary>
        /// Triggered when the user clicks the "Install" button
        /// </summary>
        private async void btnInstall_Click(object sender, EventArgs e)
        {
            if (_selectedRelease == null)
            {
                MessageBox.Show("Please select a release.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string targetFolder = txtTargetDirectory.Text;

            // Validate that a directory was entered
            if (string.IsNullOrWhiteSpace(targetFolder))
            {
                MessageBox.Show("Please select or enter a target directory.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create desktop shortcut if checked
            if (chkDesktopShortcut.Checked)
            {
                try
                {
                    CreateDesktopShortcut(targetFolder);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Warning: Could not create desktop shortcut.\n{ex.Message}", "Shortcut Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            // Get the download URL from the selected release
            string? downloadUrl = _releaseManager.GetZipDownloadUrl(_selectedRelease);
            if (string.IsNullOrEmpty(downloadUrl))
            {
                MessageBox.Show("No ZIP file found in the selected release.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update UI to show downloading state
            lblStatus.Text = "Downloading components from GitHub...";
            btnInstall.Enabled = false;
            btnRefresh.Enabled = false;
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;

            try
            {
                // Run the download and extraction process asynchronously
                await DownloadAndExtractFilesAsync(downloadUrl, targetFolder);

                lblStatus.Text = "Installation complete!";
                progressBar.Visible = false;
                MessageBox.Show("Installation completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Installation failed.";
                progressBar.Visible = false;
                MessageBox.Show($"An error occurred during installation:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnInstall.Enabled = true;
                btnRefresh.Enabled = true;
            }
        }

        /// <summary>
        /// Browse for a target folder
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the target installation folder";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtTargetDirectory.Text = dialog.SelectedPath;
                }
            }
        }

        /// <summary>
        /// Refresh releases from GitHub
        /// </summary>
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadReleasesAsync();
        }

        /// <summary>
        /// Handles downloading the zip stream and extracting it to disk without freezing the UI
        /// </summary>
        private async Task DownloadAndExtractFilesAsync(string downloadUrl, string destinationFolder)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set a user-agent header (GitHub requires this for API/Release downloads)
                client.DefaultRequestHeaders.UserAgent.ParseAdd("BananaInstaller/1.0");

                // Download the file stream directly into memory
                using (Stream successStream = await client.GetStreamAsync(downloadUrl))
                {
                    // Open the stream as a zip archive
                    using (ZipArchive archive = new ZipArchive(successStream))
                    {
                        // Extract every file into your verified game folder
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            string fullPath = Path.Combine(destinationFolder, entry.FullName);

                            // Safeguard to ensure directories inside the zip are created correctly
                            string? directoryName = Path.GetDirectoryName(fullPath);
                            if (!string.IsNullOrEmpty(directoryName))
                            {
                                Directory.CreateDirectory(directoryName);
                            }

                            // If it's an actual file (not just a directory entry), extract it
                            if (!string.IsNullOrEmpty(entry.Name))
                            {
                                entry.ExtractToFile(fullPath, overwrite: true);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draws banana icon on the yellow sidebar
        /// </summary>
        private void pnlSidebar_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.Clear(System.Drawing.Color.FromArgb(255, 223, 0));
            
            try
            {
                string? baseDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (!string.IsNullOrEmpty(baseDir))
                {
                    string bananaPath = System.IO.Path.Combine(baseDir, "Views", "Assets", "banana.png");
                    if (System.IO.File.Exists(bananaPath))
                    {
                        using (System.Drawing.Image bananaImage = System.Drawing.Image.FromFile(bananaPath))
                        {
                            // Scale banana to fit the sidebar width with some padding
                            int imageWidth = pnlSidebar.Width - 10;
                            int imageHeight = (int)((bananaImage.Height / (float)bananaImage.Width) * imageWidth);
                            int x = 5;
                            int y = 30;
                            
                            e.Graphics.DrawImage(bananaImage, x, y, imageWidth, imageHeight);
                        }
                        return;
                    }
                }
            }
            catch { }
            
            // If image fails to load, draw fallback banana square
            int size = 30;
            int fallbackX = (pnlSidebar.Width - size) / 2;
            int fallbackY = 40;
            e.Graphics.FillRectangle(System.Drawing.Brushes.Black, new System.Drawing.Rectangle(fallbackX, fallbackY, size, size));
        }

        /// <summary>
        /// Creates a desktop shortcut for the installed application
        /// </summary>
        private void CreateDesktopShortcut(string targetFolder)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string shortcutPath = Path.Combine(desktopPath, "Banana Manger.lnk");
                
                // Try to find an executable in the target folder
                var exeFiles = Directory.GetFiles(targetFolder, "*.exe", SearchOption.AllDirectories);
                if (exeFiles.Length == 0)
                {
                    return; // No executable found, skip shortcut creation
                }

                string exePath = exeFiles[0]; // Use the first executable found
                
    // Create shortcut using Windows Shell
                var shell = new IWshRuntimeLibrary.WshShell();
                var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Banana Manager.lnk"));
                shortcut.TargetPath = exePath;
                shortcut.WorkingDirectory = System.IO.Path.GetDirectoryName(exePath);
                shortcut.Save();
            }
            catch
            {
                // Silently fail if shortcut creation doesn't work
            }
        }
    }
}
