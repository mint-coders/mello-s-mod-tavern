namespace Banana_Manage;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private Panel mainPanel;
    private Label titleLabel;
    private Label loaderLabel;
    private ComboBox loaderComboBox;
    private DataGridView modsGrid;
    private TextBox targetModsPathTextBox;
    private Button browseTargetModsPathButton;
    private Button addModButton;
    private Button removeModButton;
    private Button toggleModButton;
    private Button saveButton;
    private Button installBepInExButton;
    private Button installMelonLoaderButton;
    private Button openModFolderButton;
    private Label saveStatusLabel;
    private System.Windows.Forms.Timer saveStatusTimer;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
            privateFonts.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        mainPanel = new Panel();
        titleLabel = new Label();
        loaderLabel = new Label();
        loaderComboBox = new ComboBox();
        modsGrid = new DataGridView();
        targetModsPathTextBox = new TextBox();
        browseTargetModsPathButton = new Button();
        addModButton = new Button();
        removeModButton = new Button();
        toggleModButton = new Button();
        saveButton = new Button();
        installBepInExButton = new Button();
        installMelonLoaderButton = new Button();
        openModFolderButton = new Button();
        saveStatusLabel = new Label();
        saveStatusTimer = new System.Windows.Forms.Timer(components);
        mainPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)modsGrid).BeginInit();
        SuspendLayout();
        // 
        // mainPanel
        // 
        mainPanel.BackColor = Color.FromArgb(64, 64, 64);
        mainPanel.BorderStyle = BorderStyle.FixedSingle;
        mainPanel.Controls.Add(toggleModButton);
        mainPanel.Controls.Add(saveButton);
        mainPanel.Controls.Add(installBepInExButton);
        mainPanel.Controls.Add(installMelonLoaderButton);
        mainPanel.Controls.Add(openModFolderButton);
        mainPanel.Controls.Add(saveStatusLabel);
        mainPanel.Controls.Add(removeModButton);
        mainPanel.Controls.Add(addModButton);
        mainPanel.Controls.Add(modsGrid);
        mainPanel.Controls.Add(browseTargetModsPathButton);
        mainPanel.Controls.Add(targetModsPathTextBox);
        mainPanel.Controls.Add(loaderComboBox);
        mainPanel.Controls.Add(loaderLabel);
        mainPanel.Controls.Add(titleLabel);
        mainPanel.Dock = DockStyle.Fill;
        mainPanel.Location = new Point(24, 24);
        mainPanel.Name = "mainPanel";
        mainPanel.Size = new Size(896, 393);
        mainPanel.TabIndex = 0;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Font = new Font("Cherry Bomb One", 24F, FontStyle.Regular, GraphicsUnit.Point);
        titleLabel.ForeColor = Color.FromArgb(24, 24, 24);
        titleLabel.Location = new Point(15, 19);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(103, 45);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Mods";
        // 
        // loaderLabel
        // 
        loaderLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        loaderLabel.AutoSize = true;
        loaderLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        loaderLabel.ForeColor = Color.White;
        loaderLabel.Location = new Point(687, 151);
        loaderLabel.Name = "loaderLabel";
        loaderLabel.Size = new Size(45, 15);
        loaderLabel.TabIndex = 1;
        loaderLabel.Text = "Loader";
        // 
        // loaderComboBox
        // 
        loaderComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        loaderComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        loaderComboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        loaderComboBox.FormattingEnabled = true;
        loaderComboBox.Items.AddRange(new object[] { "MelonLoader", "BepInEx" });
        loaderComboBox.Location = new Point(687, 171);
        loaderComboBox.Name = "loaderComboBox";
        loaderComboBox.Size = new Size(147, 23);
        loaderComboBox.TabIndex = 2;
        loaderComboBox.SelectedIndexChanged += LoaderComboBox_SelectedIndexChanged;
        // 
        // modsGrid
        // 
        modsGrid.AllowUserToAddRows = false;
        modsGrid.AllowUserToDeleteRows = false;
        modsGrid.AllowUserToResizeRows = false;
        modsGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        modsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        modsGrid.BackgroundColor = Color.FromArgb(42, 42, 42);
        modsGrid.BorderStyle = BorderStyle.Fixed3D;
        modsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
        modsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        modsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        modsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        modsGrid.DefaultCellStyle.BackColor = Color.FromArgb(54, 54, 54);
        modsGrid.DefaultCellStyle.ForeColor = Color.White;
        modsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(95, 95, 95);
        modsGrid.DefaultCellStyle.SelectionForeColor = Color.White;
        modsGrid.EnableHeadersVisualStyles = false;
        modsGrid.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        modsGrid.Location = new Point(15, 72);
        modsGrid.MultiSelect = false;
        modsGrid.Name = "modsGrid";
        modsGrid.ReadOnly = true;
        modsGrid.RowTemplate.Height = 32;
        modsGrid.RowHeadersVisible = false;
        modsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        modsGrid.Size = new Size(658, 211);
        modsGrid.TabIndex = 3;
        // 
        // targetModsPathTextBox
        // 
        targetModsPathTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        targetModsPathTextBox.BackColor = Color.FromArgb(230, 230, 230);
        targetModsPathTextBox.BorderStyle = BorderStyle.FixedSingle;
        targetModsPathTextBox.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
        targetModsPathTextBox.ForeColor = Color.FromArgb(20, 20, 20);
        targetModsPathTextBox.Location = new Point(15, 288);
        targetModsPathTextBox.Name = "targetModsPathTextBox";
        targetModsPathTextBox.PlaceholderText = "Game and mod folder path";
        targetModsPathTextBox.ReadOnly = true;
        targetModsPathTextBox.Size = new Size(536, 29);
        targetModsPathTextBox.TabIndex = 4;
        // 
        // browseTargetModsPathButton
        // 
        browseTargetModsPathButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        browseTargetModsPathButton.BackColor = Color.FromArgb(28, 28, 28);
        browseTargetModsPathButton.FlatAppearance.BorderSize = 0;
        browseTargetModsPathButton.FlatStyle = FlatStyle.Flat;
        browseTargetModsPathButton.ForeColor = Color.White;
        browseTargetModsPathButton.ImageAlign = ContentAlignment.MiddleCenter;
        browseTargetModsPathButton.Location = new Point(557, 288);
        browseTargetModsPathButton.Name = "browseTargetModsPathButton";
        browseTargetModsPathButton.Size = new Size(116, 29);
        browseTargetModsPathButton.TabIndex = 5;
        browseTargetModsPathButton.Text = "";
        browseTargetModsPathButton.UseVisualStyleBackColor = false;
        browseTargetModsPathButton.Click += BrowseTargetModsPathButton_Click;
        // 
        // addModButton
        // 
        addModButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        addModButton.BackColor = Color.FromArgb(28, 28, 28);
        addModButton.FlatAppearance.BorderSize = 0;
        addModButton.FlatStyle = FlatStyle.Flat;
        addModButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        addModButton.ForeColor = Color.White;
        addModButton.Location = new Point(687, 0);
        addModButton.Name = "addModButton";
        addModButton.Size = new Size(147, 33);
        addModButton.TabIndex = 6;
        addModButton.Text = "Add mod";
        addModButton.UseVisualStyleBackColor = false;
        addModButton.Click += AddModButton_Click;
        // 
        // removeModButton
        // 
        removeModButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        removeModButton.BackColor = Color.FromArgb(28, 28, 28);
        removeModButton.FlatAppearance.BorderSize = 0;
        removeModButton.FlatStyle = FlatStyle.Flat;
        removeModButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        removeModButton.ForeColor = Color.White;
        removeModButton.Location = new Point(687, 57);
        removeModButton.Name = "removeModButton";
        removeModButton.Size = new Size(147, 33);
        removeModButton.TabIndex = 7;
        removeModButton.Text = "Remove mod";
        removeModButton.UseVisualStyleBackColor = false;
        removeModButton.Click += RemoveModButton_Click;
        // 
        // toggleModButton
        // 
        toggleModButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        toggleModButton.BackColor = Color.FromArgb(43, 179, 72);
        toggleModButton.FlatAppearance.BorderSize = 0;
        toggleModButton.FlatStyle = FlatStyle.Flat;
        toggleModButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        toggleModButton.ForeColor = Color.White;
        toggleModButton.Location = new Point(687, 101);
        toggleModButton.Name = "toggleModButton";
        toggleModButton.Size = new Size(147, 33);
        toggleModButton.TabIndex = 8;
        toggleModButton.Text = "Enable / Disable";
        toggleModButton.UseVisualStyleBackColor = false;
        toggleModButton.Click += ToggleModButton_Click;
        // 
        // saveButton
        // 
        saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        saveButton.BackColor = Color.FromArgb(28, 28, 28);
        saveButton.FlatAppearance.BorderSize = 0;
        saveButton.FlatStyle = FlatStyle.Flat;
        saveButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        saveButton.ForeColor = Color.White;
        saveButton.Location = new Point(687, 215);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(147, 33);
        saveButton.TabIndex = 9;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = false;
        saveButton.Click += SaveButton_Click;
        // 
        // installBepInExButton
        // 
        installBepInExButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        installBepInExButton.BackColor = Color.FromArgb(28, 28, 28);
        installBepInExButton.FlatAppearance.BorderSize = 0;
        installBepInExButton.FlatStyle = FlatStyle.Flat;
        installBepInExButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        installBepInExButton.ForeColor = Color.White;
        installBepInExButton.Location = new Point(687, 286);
        installBepInExButton.Name = "installBepInExButton";
        installBepInExButton.Size = new Size(147, 48);
        installBepInExButton.TabIndex = 10;
        installBepInExButton.Text = "Install\r\nBepInEx";
        installBepInExButton.TextAlign = ContentAlignment.MiddleCenter;
        installBepInExButton.UseVisualStyleBackColor = false;
        installBepInExButton.UseCompatibleTextRendering = true;
        installBepInExButton.Click += InstallBepInExButton_Click;
        // 
        // installMelonLoaderButton
        // 
        installMelonLoaderButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        installMelonLoaderButton.BackColor = Color.FromArgb(28, 28, 28);
        installMelonLoaderButton.FlatAppearance.BorderSize = 0;
        installMelonLoaderButton.FlatStyle = FlatStyle.Flat;
        installMelonLoaderButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        installMelonLoaderButton.ForeColor = Color.White;
        installMelonLoaderButton.Location = new Point(687, 339);
        installMelonLoaderButton.Name = "installMelonLoaderButton";
        installMelonLoaderButton.Size = new Size(147, 48);
        installMelonLoaderButton.TabIndex = 11;
        installMelonLoaderButton.Text = "Install\r\nMelonLoader";
        installMelonLoaderButton.TextAlign = ContentAlignment.MiddleCenter;
        installMelonLoaderButton.UseVisualStyleBackColor = false;
        installMelonLoaderButton.UseCompatibleTextRendering = true;
        installMelonLoaderButton.Click += InstallMelonLoaderButton_Click;
        // 
        // openModFolderButton
        // 
        openModFolderButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        openModFolderButton.BackColor = Color.FromArgb(28, 28, 28);
        openModFolderButton.FlatAppearance.BorderSize = 0;
        openModFolderButton.FlatStyle = FlatStyle.Flat;
        openModFolderButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        openModFolderButton.ForeColor = Color.White;
        openModFolderButton.Location = new Point(687, 253);
        openModFolderButton.Name = "openModFolderButton";
        openModFolderButton.Size = new Size(147, 33);
        openModFolderButton.TabIndex = 12;
        openModFolderButton.Text = "Open mod folder";
        openModFolderButton.UseVisualStyleBackColor = false;
        openModFolderButton.Click += OpenModFolderButton_Click;
        // 
        // saveStatusLabel
        // 
        saveStatusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        saveStatusLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        saveStatusLabel.ForeColor = Color.White;
        saveStatusLabel.Location = new Point(687, 247);
        saveStatusLabel.Name = "saveStatusLabel";
        saveStatusLabel.Size = new Size(147, 18);
        saveStatusLabel.TabIndex = 13;
        saveStatusLabel.TextAlign = ContentAlignment.TopCenter;
        // 
        // saveStatusTimer
        // 
        saveStatusTimer.Interval = 2000;
        saveStatusTimer.Tick += SaveStatusTimer_Tick;
        // 
        // Form1
        // 
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(944, 441);
        Controls.Add(mainPanel);
        MinimumSize = new Size(760, 360);
        Name = "Form1";
        Padding = new Padding(24);
        Text = "Banana Manger";
        Load += Form1_Load;
        mainPanel.ResumeLayout(false);
        mainPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)modsGrid).EndInit();
        ResumeLayout(false);
    }

    #endregion
}
