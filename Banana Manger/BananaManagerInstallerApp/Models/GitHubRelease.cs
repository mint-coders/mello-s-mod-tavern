using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BananaManagerInstallerApp.Models
{
    /// <summary>
    /// Represents a GitHub release
    /// </summary>
    public class GitHubRelease
    {
        [JsonPropertyName("tag_name")]
        public string? TagName { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("draft")]
        public bool Draft { get; set; }

        [JsonPropertyName("prerelease")]
        public bool Prerelease { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("assets")]
        public List<GitHubAsset>? Assets { get; set; }

        public override string ToString()
        {
            return $"{Name ?? TagName} ({PublishedAt:yyyy-MM-dd})";
        }
    }

    /// <summary>
    /// Represents a GitHub release asset (file)
    /// </summary>
    public class GitHubAsset
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("browser_download_url")]
        public string? BrowserDownloadUrl { get; set; }

        [JsonPropertyName("size")]
        public long Size { get; set; }
    }
}
