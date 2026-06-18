# Mello's Mod Tavern Installer - Copilot Instructions

This is a C# Windows Forms desktop application that checks GitHub for releases and allows users to download and install them.

## Project Overview

- **Type**: Windows Desktop Application (Windows Forms)
- **Language**: C# (.NET 8.0)
- **Architecture**: MVVM-inspired with Models, Views, and ViewModels folders
- **GitHub Integration**: Fetches releases via GitHub REST API

## Key Files

- `BananaManagerInstallerApp.csproj` - Project configuration
- `Program.cs` - Application entry point
- `Views/MainWindow.cs` - Main installer form with release checking logic
- `Views/MainWindow.Designer.cs` - Form design and controls
- `Models/GitHubRelease.cs` - Data models for GitHub releases and assets
- `ViewModels/ReleaseManager.cs` - GitHub API interaction class
- `Models/` - Data models folder
- `ViewModels/` - ViewModel classes folder

## Build and Run

```bash
# Build the project
dotnet build

# Run the application
dotnet run
```

## Main Features

- **Release Discovery**: Automatically fetches releases from GitHub on startup
- **Release List**: Displays all releases with dates and allows selection
- **Latest First**: Auto-selects the latest release by default
- **Download**: Async download of ZIP files from release assets
- **Extract**: Extracts to user-selected directory without blocking UI
- **Progress**: Visual progress indicator with status messages
- **Folder Browser**: Easy directory selection with browse button
- **Refresh**: Manual refresh button to reload releases from GitHub
- **Error Handling**: Comprehensive error messages and recovery

## Configuration

GitHub repository details in `Views/MainWindow.cs` (lines 15-16):

```csharp
private const string GitHubOwner = "mint-coders";
private const string GitHubRepo = "mello-s-mod-tavern";
```

## API Details

- Uses GitHub REST API (no authentication required for public repos)
- Endpoint: `https://api.github.com/repos/{owner}/{repo}/releases`
- Looks for ZIP files in release assets
- Handles release metadata (name, date, draft status, prerelease status)

## Expanding the Application

To add new features:

1. **New UI screens** → Add new forms in `Views/` folder
2. **Data models** → Add classes in `Models/` folder
3. **Business logic** → Add classes in `ViewModels/` folder
4. **GitHub features** → Extend `ReleaseManager` class

## Dependencies

- .NET 8.0 SDK
- System.Net.Http (built-in for HTTP requests)
- System.Text.Json (built-in for JSON deserialization)
- System.IO.Compression (built-in for ZIP extraction)
