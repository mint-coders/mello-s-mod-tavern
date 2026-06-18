using System.Diagnostics;
using System.Drawing.Text;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Banana_Manage;

public partial class Form1 : Form
{
    private const string DefaultLoader = "MelonLoader";
    private const string BepInExLoader = "BepInEx";
    private const string BepInExLatestReleaseUrl = "https://api.github.com/repos/BepInEx/BepInEx/releases/latest";
    private const string MelonLoaderLatestReleaseUrl = "https://api.github.com/repos/LavaGang/MelonLoader/releases/latest";
    private readonly string pathsFile = Path.Combine(Environment.CurrentDirectory, "Paths.json");
    private readonly List<ModEntry> mods = new();
    private readonly List<LoaderEntry> loaders = new();
    private readonly PrivateFontCollection privateFonts = new();
    private static readonly HttpClient httpClient = new();

    public Form1()
    {
        InitializeComponent();
        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath) ?? Icon;
        LoadLocalAssets();
        ApplyLocalFont();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        loaderComboBox.SelectedItem = DefaultLoader;
        ConfigureGrid();
        LoadMods();
        RefreshGrid();
    }

    private void ConfigureGrid()
    {
        modsGrid.Columns.Clear();
        modsGrid.Columns.Add("Name", "Name");
        modsGrid.Columns.Add("Path", "Path");
        modsGrid.Columns.Add("Status", "Status");

        modsGrid.Columns["Name"]!.FillWeight = 24;
        modsGrid.Columns["Path"]!.FillWeight = 56;
        modsGrid.Columns["Status"]!.FillWeight = 20;
    }

    private void LoadLocalAssets()
    {
        string folderIconPath = FindAppAsset("folder_data_white.png");
        string fontPath = FindAppAsset("CherryBombOne-Regular.ttf");

        if (File.Exists(folderIconPath))
        {
            browseTargetModsPathButton.Image = Image.FromFile(folderIconPath);
        }

        if (File.Exists(fontPath))
        {
            privateFonts.AddFontFile(fontPath);
        }
    }

    private void ApplyLocalFont()
    {
        if (privateFonts.Families.Length == 0)
        {
            return;
        }

        FontFamily fontFamily = privateFonts.Families[0];
        titleLabel.Font = new Font(fontFamily, 24F, FontStyle.Regular);
    }

    private static string FindAppAsset(string fileName)
    {
        string outputPath = Path.Combine(AppContext.BaseDirectory, fileName);

        if (File.Exists(outputPath))
        {
            return outputPath;
        }

        return Path.Combine(Environment.CurrentDirectory, fileName);
    }

    private void LoadMods()
    {
        mods.Clear();

        if (!File.Exists(pathsFile))
        {
            EnsureDefaultLoaders();
            SaveMods();
            UpdateTargetModsPathTextBox();
            return;
        }

        try
        {
            string json = File.ReadAllText(pathsFile);
            PathsConfig? config = JsonSerializer.Deserialize<PathsConfig>(json);

            if (config?.Mods is not null)
            {
                mods.AddRange(config.Mods);

                foreach (ModEntry mod in mods.Where(mod => string.IsNullOrWhiteSpace(mod.Loader)))
                {
                    mod.Loader = DefaultLoader;
                }
            }

            if (config?.Loaders is not null)
            {
                loaders.AddRange(config.Loaders);
            }

            EnsureDefaultLoaders();
        }
        catch (JsonException)
        {
            MessageBox.Show(
                "Paths.json could not be read. Banana Manger will start with an empty mod list.",
                "Bad Paths.json",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        UpdateTargetModsPathTextBox();
    }

    private void EnsureDefaultLoaders()
    {
        EnsureLoaderExists("MelonLoader");
        EnsureLoaderExists("BepInEx");
    }

    private void SaveMods()
    {
        PathsConfig config = new()
        {
            Loaders = loaders,
            Mods = mods
        };
        JsonSerializerOptions options = new() { WriteIndented = true };
        string json = JsonSerializer.Serialize(config, options);
        string? folder = Path.GetDirectoryName(pathsFile);

        if (!string.IsNullOrWhiteSpace(folder))
        {
            Directory.CreateDirectory(folder);
        }

        File.WriteAllText(pathsFile, json);
    }

    private void RefreshGrid()
    {
        titleLabel.Text = $"{CurrentLoader} Mods";
        UpdateTargetModsPathTextBox();
        modsGrid.Rows.Clear();

        foreach (ModEntry mod in mods.Where(mod => mod.Loader == CurrentLoader))
        {
            string status = mod.Enabled ? "Enabled" : "Disabled";
            int rowIndex = modsGrid.Rows.Add(mod.Name, mod.Path, status);
            DataGridViewRow row = modsGrid.Rows[rowIndex];
            row.Tag = mod;

            if (!File.Exists(mod.Path))
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(120, 48, 48);
                row.Cells["Status"].Value = "Missing";
            }
            else if (mod.Enabled)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(42, 110, 55);
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(54, 54, 54);
            }

            row.DefaultCellStyle.ForeColor = Color.White;
        }
    }

    private void AddModButton_Click(object sender, EventArgs e)
    {
        using OpenFileDialog dialog = new()
        {
            Filter = "DLL mod files (*.dll)|*.dll",
            Title = "Choose a mod file"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        if (!string.Equals(Path.GetExtension(dialog.FileName), ".dll", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show(
                "Banana Manger only accepts .dll mod files.",
                "Wrong file type",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        string defaultName = Path.GetFileNameWithoutExtension(dialog.FileName);
        string? modName = PromptForText("Mod name", "Name this mod:", defaultName);

        if (string.IsNullOrWhiteSpace(modName))
        {
            return;
        }

        mods.Add(new ModEntry
        {
            Name = modName.Trim(),
            Path = dialog.FileName,
            Enabled = false,
            Loader = CurrentLoader
        });

        SaveMods();
        saveStatusLabel.Text = "";
        RefreshGrid();
    }

    private void LoaderComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateTargetModsPathTextBox();
        RefreshGrid();
    }

    private void BrowseTargetModsPathButton_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new()
        {
            Description = $"Choose the {CurrentLoader} game mod folder"
        };

        LoaderEntry loader = GetCurrentLoaderEntry();

        if (Directory.Exists(loader.TargetModsPath))
        {
            dialog.InitialDirectory = loader.TargetModsPath;
        }

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        loader.TargetModsPath = dialog.SelectedPath;
        SaveMods();
        saveStatusLabel.Text = "";
        UpdateTargetModsPathTextBox();
    }

    private void RemoveModButton_Click(object sender, EventArgs e)
    {
        ModEntry? selectedMod = GetSelectedMod();

        if (selectedMod is null)
        {
            MessageBox.Show("Pick a mod first.", "No mod selected");
            return;
        }

        DialogResult result = MessageBox.Show(
            $"Remove \"{selectedMod.Name}\" from Banana Manger?",
            "Remove mod",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result != DialogResult.Yes)
        {
            return;
        }

        mods.Remove(selectedMod);
        SaveMods();
        saveStatusLabel.Text = "";
        RefreshGrid();
    }

    private void ToggleModButton_Click(object sender, EventArgs e)
    {
        ModEntry? selectedMod = GetSelectedMod();

        if (selectedMod is null)
        {
            MessageBox.Show("Pick a mod first.", "No mod selected");
            return;
        }

        if (selectedMod.Enabled)
        {
            DisableMod(selectedMod);
        }
        else if (!EnableMod(selectedMod))
        {
            return;
        }

        SaveMods();
        saveStatusLabel.Text = "";
        RefreshGrid();
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        SaveMods();
        saveStatusLabel.Text = "Saved";
        saveStatusTimer.Stop();
        saveStatusTimer.Start();
    }

    private void SaveStatusTimer_Tick(object sender, EventArgs e)
    {
        saveStatusTimer.Stop();
        saveStatusLabel.Text = "";
    }

    private async void InstallBepInExButton_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new()
        {
            Description = "Choose the game folder where BepInEx should be installed"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            SetBusy(true, "Installing...");

            string zipPath = await DownloadLatestReleaseAssetAsync(
                BepInExLatestReleaseUrl,
                assetName => assetName.Contains("win_x64", StringComparison.OrdinalIgnoreCase)
                    && assetName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase),
                "Could not find a Windows x64 BepInEx release zip.");

            ZipFile.ExtractToDirectory(zipPath, dialog.SelectedPath, overwriteFiles: true);
            File.Delete(zipPath);

            loaderComboBox.SelectedItem = BepInExLoader;
            LoaderEntry loader = GetCurrentLoaderEntry();
            loader.TargetModsPath = Path.Combine(dialog.SelectedPath, "BepInEx", "plugins");
            Directory.CreateDirectory(loader.TargetModsPath);

            SaveMods();
            UpdateTargetModsPathTextBox();
            saveStatusLabel.Text = "Installed";
            saveStatusTimer.Stop();
            saveStatusTimer.Start();
        }
        catch (Exception ex) when (ex is HttpRequestException or IOException or JsonException or InvalidOperationException)
        {
            MessageBox.Show(
                $"BepInEx could not be installed.\n\n{ex.Message}",
                "Install failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void InstallMelonLoaderButton_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new()
        {
            Description = "Choose the game folder where MelonLoader should be installed"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            SetBusy(true, "Installing...");

            string zipPath = await DownloadLatestReleaseAssetAsync(
                MelonLoaderLatestReleaseUrl,
                assetName => assetName.Equals("MelonLoader.x64.zip", StringComparison.OrdinalIgnoreCase)
                    || assetName.Contains("MelonLoader.x64", StringComparison.OrdinalIgnoreCase)
                    && assetName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase),
                "Could not find a Windows x64 MelonLoader release zip.");

            ZipFile.ExtractToDirectory(zipPath, dialog.SelectedPath, overwriteFiles: true);
            File.Delete(zipPath);

            loaderComboBox.SelectedItem = DefaultLoader;
            LoaderEntry loader = GetCurrentLoaderEntry();
            loader.TargetModsPath = Path.Combine(dialog.SelectedPath, "Mods");
            Directory.CreateDirectory(loader.TargetModsPath);

            SaveMods();
            UpdateTargetModsPathTextBox();
            saveStatusLabel.Text = "Installed";
            saveStatusTimer.Stop();
            saveStatusTimer.Start();
        }
        catch (Exception ex) when (ex is HttpRequestException or IOException or JsonException or InvalidOperationException)
        {
            MessageBox.Show(
                $"MelonLoader could not be installed.\n\n{ex.Message}",
                "Install failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void OpenModFolderButton_Click(object sender, EventArgs e)
    {
        string targetModsPath = GetCurrentLoaderEntry().TargetModsPath;

        if (string.IsNullOrWhiteSpace(targetModsPath))
        {
            MessageBox.Show(
                $"Set the {CurrentLoader} game mod folder path first.",
                "Missing mod folder",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        Directory.CreateDirectory(targetModsPath);
        Process.Start(new ProcessStartInfo
        {
            FileName = targetModsPath,
            UseShellExecute = true
        });
    }

    private ModEntry? GetSelectedMod()
    {
        if (modsGrid.SelectedRows.Count == 0)
        {
            return null;
        }

        return modsGrid.SelectedRows[0].Tag as ModEntry;
    }

    private string CurrentLoader => loaderComboBox.SelectedItem?.ToString() ?? DefaultLoader;

    private static async Task<string> DownloadLatestReleaseAssetAsync(
        string latestReleaseUrl,
        Func<string, bool> assetNameMatches,
        string missingAssetMessage)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, latestReleaseUrl);
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue("BananaManger", "1.0"));

        using HttpResponseMessage response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        using Stream stream = await response.Content.ReadAsStreamAsync();
        using JsonDocument releaseJson = await JsonDocument.ParseAsync(stream);
        JsonElement assets = releaseJson.RootElement.GetProperty("assets");
        string? downloadUrl = null;

        foreach (JsonElement asset in assets.EnumerateArray())
        {
            string name = asset.GetProperty("name").GetString() ?? "";

            if (assetNameMatches(name))
            {
                downloadUrl = asset.GetProperty("browser_download_url").GetString();
                break;
            }
        }

        if (string.IsNullOrWhiteSpace(downloadUrl))
        {
            throw new InvalidOperationException(missingAssetMessage);
        }

        string zipPath = Path.Combine(Path.GetTempPath(), $"BananaManger-{Guid.NewGuid():N}.zip");
        using Stream zipStream = await httpClient.GetStreamAsync(downloadUrl);
        using FileStream fileStream = File.Create(zipPath);
        await zipStream.CopyToAsync(fileStream);
        return zipPath;
    }

    private void SetBusy(bool busy, string statusText = "")
    {
        addModButton.Enabled = !busy;
        removeModButton.Enabled = !busy;
        toggleModButton.Enabled = !busy;
        saveButton.Enabled = !busy;
        installBepInExButton.Enabled = !busy;
        installMelonLoaderButton.Enabled = !busy;
        openModFolderButton.Enabled = !busy;
        browseTargetModsPathButton.Enabled = !busy;
        loaderComboBox.Enabled = !busy;
        saveStatusLabel.Text = statusText;
    }

    private LoaderEntry GetCurrentLoaderEntry()
    {
        EnsureLoaderExists(CurrentLoader);
        return loaders.First(loader => loader.Name == CurrentLoader);
    }

    private void EnsureLoaderExists(string name)
    {
        if (loaders.Any(loader => loader.Name == name))
        {
            return;
        }

        loaders.Add(new LoaderEntry { Name = name });
    }

    private void UpdateTargetModsPathTextBox()
    {
        targetModsPathTextBox.Text = GetCurrentLoaderEntry().TargetModsPath;
    }

    private bool EnableMod(ModEntry mod)
    {
        LoaderEntry loader = GetCurrentLoaderEntry();

        if (string.IsNullOrWhiteSpace(loader.TargetModsPath))
        {
            MessageBox.Show(
                $"Set the {CurrentLoader} game mod folder path first.",
                "Missing mod folder",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        if (!File.Exists(mod.Path))
        {
            MessageBox.Show(
                $"Banana Manger could not find {mod.Name}.",
                "Missing mod file",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        Directory.CreateDirectory(loader.TargetModsPath);

        string destinationPath = Path.Combine(loader.TargetModsPath, Path.GetFileName(mod.Path));
        File.Copy(mod.Path, destinationPath, overwrite: true);
        mod.Enabled = true;
        mod.DeployedPath = destinationPath;
        return true;
    }

    private void DisableMod(ModEntry mod)
    {
        string deployedPath = mod.DeployedPath;

        if (string.IsNullOrWhiteSpace(deployedPath))
        {
            deployedPath = Path.Combine(GetCurrentLoaderEntry().TargetModsPath, Path.GetFileName(mod.Path));
        }

        if (File.Exists(deployedPath))
        {
            File.Delete(deployedPath);
        }

        mod.Enabled = false;
        mod.DeployedPath = "";
    }

    private static string? PromptForText(string title, string message, string defaultValue)
    {
        using Form prompt = new()
        {
            Text = title,
            Width = 380,
            Height = 160,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false
        };

        Label label = new()
        {
            Text = message,
            Left = 12,
            Top = 14,
            Width = 340
        };

        TextBox textBox = new()
        {
            Left = 12,
            Top = 42,
            Width = 340,
            Text = defaultValue
        };

        Button okButton = new()
        {
            Text = "OK",
            Left = 196,
            Width = 75,
            Top = 78,
            DialogResult = DialogResult.OK
        };

        Button cancelButton = new()
        {
            Text = "Cancel",
            Left = 277,
            Width = 75,
            Top = 78,
            DialogResult = DialogResult.Cancel
        };

        prompt.Controls.Add(label);
        prompt.Controls.Add(textBox);
        prompt.Controls.Add(okButton);
        prompt.Controls.Add(cancelButton);
        prompt.AcceptButton = okButton;
        prompt.CancelButton = cancelButton;

        return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
    }
}

public sealed class PathsConfig
{
    [JsonPropertyName("loaders")]
    public List<LoaderEntry> Loaders { get; set; } = new();

    [JsonPropertyName("mods")]
    public List<ModEntry> Mods { get; set; } = new();
}

public sealed class LoaderEntry
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("targetModsPath")]
    public string TargetModsPath { get; set; } = "";
}

public sealed class ModEntry
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("path")]
    public string Path { get; set; } = "";

    [JsonPropertyName("loader")]
    public string Loader { get; set; } = "MelonLoader";

    [JsonPropertyName("deployedPath")]
    public string DeployedPath { get; set; } = "";

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
}
