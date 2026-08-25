# Quick Start: Card Image Uploads

## 1. Get Vercel Blob Credentials

Visit: https://vercel.com/dashboard
1. Create or select a project
2. Go to **Storage** → **Create Database** → **Blob**
3. Copy your **Token** and **Store ID**

## 2. Configure Locally

Add to `appsettings.Development.json`:

```json
{
  "VercelBlob": {
	"Token": "vercel_blob_rw_PASTE_YOUR_TOKEN_HERE",
	"StoreId": "PASTE_YOUR_STORE_ID_HERE",
	"BaseUrl": "https://blob.vercel-storage.com"
  }
}
```

## 3. Run the App

```bash
dotnet run
```

## 4. Test Image Upload

1. Open: `https://localhost:7228/Dashboard/Cards`
2. Fill in card details
3. Click **Choose Image** button
4. Select an image file (JPG, PNG, GIF, WebP, SVG)
5. See preview appear
6. Click **Create**
7. Image uploads to Vercel Blob and thumbnail appears in table

## Features

✅ **Create with Image**: Upload image when creating a card  
✅ **Edit Image**: Replace existing card image  
✅ **Auto-Delete**: Old images deleted when replaced or card is deleted  
✅ **Preview**: See image before submitting  
✅ **Thumbnail**: View uploaded images in the cards table  
✅ **URL Fallback**: Can still paste image URLs manually  

## Without Vercel Blob

If you don't configure Vercel Blob:
- ✅ App still works normally
- ✅ Can use image URLs in the BackgroundImage field
- ❌ File upload will fail (shows error message)

## Troubleshooting

**Upload fails?**
1. Check token in `appsettings.Development.json`
2. Verify token starts with `vercel_blob_rw_`
3. Check app logs for detailed error

**Image doesn't appear?**
1. Refresh the page
2. Check browser console for errors
3. Verify image URL is accessible

## Next Steps

See [VERCEL_BLOB_SETUP.md](VERCEL_BLOB_SETUP.md) for:
- Production deployment
- Environment variables
- Security best practices
- Detailed troubleshooting
