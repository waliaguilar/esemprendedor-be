using System.Net.Http.Headers;
using System.Text.Json;
using EsemprendedorApi.Application.Services.Interfaces;
using EsemprendedorApi.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace EsemprendedorApi.Application.Services;

public class VercelBlobStorageService : IImageStorageService
{
    private readonly HttpClient _httpClient;
    private readonly VercelBlobSettings _settings;
    private readonly ILogger<VercelBlobStorageService> _logger;

    public VercelBlobStorageService(
        HttpClient httpClient,
        IOptions<VercelBlobSettings> settings,
        ILogger<VercelBlobStorageService> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;

        if (string.IsNullOrWhiteSpace(_settings.Token))
            throw new InvalidOperationException("Vercel Blob Token is not configured.");
    }

    public async Task<string> UploadImageAsync(Stream file, string fileName, string contentType)
    {
        try
        {
            // Sanitize filename
            var sanitizedFileName = SanitizeFileName(fileName);
            var blobKey = $"cards/{Guid.NewGuid()}_{sanitizedFileName}";

            // Create multipart content
            using var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(file);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            content.Add(streamContent, "file", sanitizedFileName);

            // Build upload URL with token
            var uploadUrl = $"{_settings.BaseUrl}/upload?token={_settings.Token}";

            // Send upload request
            var request = new HttpRequestMessage(HttpMethod.Post, uploadUrl);
            request.Headers.Add("x-vercel-blob-store-id", _settings.StoreId);
            request.Headers.Add("x-vercel-filename", blobKey);
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var uploadResult = JsonSerializer.Deserialize<VercelUploadResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (uploadResult?.Url == null)
                throw new InvalidOperationException("Failed to get upload URL from Vercel Blob response.");

            _logger.LogInformation("Image uploaded successfully: {Url}", uploadResult.Url);
            return uploadResult.Url;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload image to Vercel Blob: {FileName}", fileName);
            throw;
        }
    }

    public async Task DeleteImageAsync(string imageUrl)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return;

            // Extract blob key from URL if needed
            var deleteUrl = $"{_settings.BaseUrl}/delete?token={_settings.Token}&url={Uri.EscapeDataString(imageUrl)}";

            var request = new HttpRequestMessage(HttpMethod.Post, deleteUrl);
            request.Headers.Add("x-vercel-blob-store-id", _settings.StoreId);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            _logger.LogInformation("Image deleted successfully: {Url}", imageUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete image from Vercel Blob: {Url}", imageUrl);
            throw;
        }
    }

    public string GetImageUrl(string blobKey)
    {
        if (string.IsNullOrWhiteSpace(blobKey))
            return string.Empty;

        // If already a full URL, return as-is
        if (blobKey.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            return blobKey;

        // Construct public URL
        return $"{_settings.BaseUrl}/{_settings.StoreId}/{blobKey}";
    }

    private static string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var sanitized = string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
        return sanitized.ToLowerInvariant();
    }

    private class VercelUploadResponse
    {
        public string? Url { get; set; }
        public string? Pathname { get; set; }
        public string? ContentType { get; set; }
        public string? ContentDisposition { get; set; }
    }
}
