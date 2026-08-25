namespace EsemprendedorApi.Infrastructure.Configuration;

public class VercelBlobSettings
{
    public string Token { get; set; } = string.Empty;
    public string StoreId { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://blob.vercel-storage.com";
}
