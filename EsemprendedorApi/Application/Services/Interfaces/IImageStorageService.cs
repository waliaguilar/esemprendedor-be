namespace EsemprendedorApi.Application.Services.Interfaces;

public interface IImageStorageService
{
    /// <summary>
    /// Upload an image to Vercel Blob Storage
    /// </summary>
    /// <param name="file">The image file stream</param>
    /// <param name="fileName">Original file name</param>
    /// <param name="contentType">Content type (e.g., image/jpeg)</param>
    /// <returns>Public URL of the uploaded image</returns>
    Task<string> UploadImageAsync(Stream file, string fileName, string contentType);

    /// <summary>
    /// Delete an image from Vercel Blob Storage
    /// </summary>
    /// <param name="imageUrl">The public URL of the image to delete</param>
    Task DeleteImageAsync(string imageUrl);

    /// <summary>
    /// Get the public URL for an image by its blob key
    /// </summary>
    /// <param name="blobKey">The blob storage key</param>
    /// <returns>Public URL</returns>
    string GetImageUrl(string blobKey);
}
