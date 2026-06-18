using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BananaManagerInstallerApp.Models;

namespace BananaManagerInstallerApp.ViewModels
{
    /// <summary>
    /// ViewModel for handling GitHub release operations
    /// </summary>
    public class ReleaseManager
    {
        private const string GitHubApiBaseUrl = "https://api.github.com/repos";
        private readonly string _owner;
        private readonly string _repo;
        private readonly HttpClient _httpClient;

        public ReleaseManager(string owner, string repo)
        {
            _owner = owner;
            _repo = repo;
            _httpClient = new HttpClient();
            // GitHub API requires a User-Agent header
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("BananaInstaller/1.0");
        }

        /// <summary>
        /// Fetches all releases from GitHub and filters for "banana manger"
        /// </summary>
        public async Task<List<GitHubRelease>> GetReleasesAsync()
        {
            try
            {
                string url = $"{GitHubApiBaseUrl}/{_owner}/{_repo}/releases";
                var releases = await _httpClient.GetFromJsonAsync<List<GitHubRelease>>(url);
                
                if (releases == null)
                    return new List<GitHubRelease>();
                
                // Filter for releases containing "banana manger"
                return releases
                    .Where(r => !string.IsNullOrEmpty(r.Name) && r.Name.Contains("banana manger", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch releases from GitHub: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets the latest release
        /// </summary>
        public async Task<GitHubRelease?> GetLatestReleaseAsync()
        {
            try
            {
                string url = $"{GitHubApiBaseUrl}/{_owner}/{_repo}/releases/latest";
                return await _httpClient.GetFromJsonAsync<GitHubRelease>(url);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch latest release: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets the download URL for a ZIP file in a release (if it exists)
        /// </summary>
        public string? GetZipDownloadUrl(GitHubRelease release)
        {
            if (release.Assets == null || release.Assets.Count == 0)
                return null;

            // Look for a ZIP file
            var zipAsset = release.Assets.FirstOrDefault(a => 
                a.Name?.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) == true);

            return zipAsset?.BrowserDownloadUrl;
        }
    }
}
