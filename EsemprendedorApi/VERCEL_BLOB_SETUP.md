# Vercel Blob Storage Setup Guide

This guide explains how to configure and use Vercel Blob Storage for card image uploads in the Esemprendedor API dashboard.

## Overview

The application now supports uploading card images directly from the Cards dashboard (`/Dashboard/Cards`) to Vercel Blob Storage. Images are automatically managed with upload, update, and deletion capabilities.

## Prerequisites

1. A Vercel account (free or paid)
2. A Vercel Blob Store created in your project

## Getting Your Vercel Blob Credentials

### Step 1: Create a Vercel Blob Store

1. Go to [Vercel Dashboard](https://vercel.com/dashboard)
2. Select your project (or create a new one)
3. Navigate to **Storage** tab
4. Click **Create Database** → **Blob**
5. Give your store a name (e.g., `esemprendedor-images`)
6. Click **Create**

### Step 2: Get Your Credentials

After creating the store:

1. In the Blob store dashboard, click on **Settings** or **Connect**
2. Copy the following values:
   - **Token**: `vercel_blob_rw_...` (read-write token)
   - **Store ID**: Your blob store identifier

## Configuration

### Option 1: Using appsettings.Development.json (Recommended for Development)

1. Open `appsettings.Development.json`
2. Update the `VercelBlob` section with your credentials:

```json
{
  "VercelBlob": {
	"Token": "vercel_blob_rw_YOUR_TOKEN_HERE",
	"StoreId": "YOUR_STORE_ID_HERE",
	"BaseUrl": "https://blob.vercel-storage.com"
  }
}
```

### Option 2: Using Environment Variables (Recommended for Production)

Set the following environment variables:

```bash
# Windows (PowerShell)
$env:VercelBlob__Token = "vercel_blob_rw_YOUR_TOKEN_HERE"
$env:VercelBlob__StoreId = "YOUR_STORE_ID_HERE"

# Linux/macOS
export VercelBlob__Token="vercel_blob_rw_YOUR_TOKEN_HERE"
export VercelBlob__StoreId="YOUR_STORE_ID_HERE"
```

### Option 3: Using User Secrets (Recommended for Development)

```bash
dotnet user-secrets set "VercelBlob:Token" "vercel_blob_rw_YOUR_TOKEN_HERE"
dotnet user-secrets set "VercelBlob:StoreId" "YOUR_STORE_ID_HERE"
```

## Features

### Card Image Upload

The Cards dashboard now includes:

1. **Upload on Create**: When creating a new card, you can upload an image file
2. **Upload on Edit**: When editing a card, you can replace the existing image
3. **Image Preview**: See a preview of the selected image before submitting
4. **Thumbnail Display**: View uploaded images as thumbnails in the cards table
5. **Automatic Cleanup**: Old images are automatically deleted when replaced or when the card is deleted

### Supported Image Formats

- JPEG/JPG
- PNG
- GIF
- WebP
- SVG

### Image Management

#### Creating a Card with an Image

1. Navigate to `/Dashboard/Cards`
2. Fill in the card details
3. Click **Choose Image** to select an image file
4. Preview appears automatically
5. Click **Create** to upload and save

#### Editing a Card Image

1. Click **Edit** on any card row
2. Current image is displayed (if exists)
3. Click **Change Image** to select a new image
4. Or update the **BackgroundImage** URL field manually
5. Click **Save** to apply changes

#### Deleting a Card

When you delete a card, the associated image is automatically removed from Vercel Blob Storage.

## Architecture

### Components

1. **IImageStorageService**: Service interface for image operations
   - `UploadImageAsync()`: Uploads an image and returns the public URL
   - `DeleteImageAsync()`: Removes an image by URL
   - `GetImageUrl()`: Resolves blob keys to public URLs

2. **VercelBlobStorageService**: Implementation using Vercel Blob API
   - Sanitizes filenames
   - Generates unique blob keys with GUIDs
   - Handles HTTP multipart uploads
   - Manages deletion via Vercel API

3. **VercelBlobSettings**: Configuration model
   - Token: Authentication token
   - StoreId: Blob store identifier
   - BaseUrl: Vercel Blob API endpoint

### File Storage Structure

Images are stored with the following naming pattern:
```
cards/{guid}_{sanitized-filename}
```

Example:
```
cards/a1b2c3d4-e5f6-7890-abcd-ef1234567890_pizza-restaurant.jpg
```

## Troubleshooting

### Issue: "Vercel Blob Token is not configured"

**Solution**: Ensure you've set the `VercelBlob:Token` configuration value in one of:
- `appsettings.Development.json`
- Environment variables
- User secrets

### Issue: Image upload fails

**Causes**:
1. Invalid or expired token
2. Incorrect store ID
3. Network connectivity issues
4. File size too large

**Solution**:
1. Verify credentials in Vercel dashboard
2. Check application logs for detailed error messages
3. Ensure your Vercel Blob plan supports the file size

### Issue: Images don't appear in the table

**Solution**:
1. Check browser console for CORS errors
2. Verify the image URL is publicly accessible
3. Ensure the `BackgroundImage` field is populated in the database

## Testing Locally

1. Configure your Vercel Blob credentials (see Configuration above)
2. Start the application:
   ```bash
   dotnet run
   ```
3. Navigate to `https://localhost:7228/Dashboard/Cards`
4. Try uploading an image when creating a new card
5. Verify the image appears in the table thumbnail column
6. Edit the card and upload a different image
7. Delete the card and verify the image is removed from Vercel Blob

## Production Deployment

When deploying to production:

1. **Never commit credentials** to source control
2. Use environment variables or Azure Key Vault for secrets
3. Set the `VercelBlob:Token` and `VercelBlob:StoreId` in your hosting environment
4. For Vercel deployment, use Vercel Environment Variables
5. For Azure deployment, use App Service Application Settings

## Costs

Vercel Blob pricing (as of 2024):

- **Hobby Plan**: Free up to 500MB storage and 100GB bandwidth
- **Pro Plan**: $0.15/GB storage, $0.15/GB bandwidth after limits
- **Enterprise**: Custom pricing

Check the [Vercel Pricing Page](https://vercel.com/docs/storage/vercel-blob/usage-and-pricing) for current rates.

## API Reference

### Upload Endpoint
```
POST https://blob.vercel-storage.com/upload?token={token}
Headers:
  x-vercel-blob-store-id: {storeId}
  x-vercel-filename: {blobKey}
Body: multipart/form-data
```

### Delete Endpoint
```
POST https://blob.vercel-storage.com/delete?token={token}&url={imageUrl}
Headers:
  x-vercel-blob-store-id: {storeId}
```

## Security Notes

1. **Token Security**: The read-write token provides full access to your blob store. Keep it secure.
2. **File Validation**: The service accepts `image/*` MIME types. Consider adding size limits if needed.
3. **Public Access**: All uploaded images are publicly accessible via their URL.
4. **Cleanup**: Images are automatically deleted when cards are removed, preventing orphaned files.

## Next Steps

- Consider adding image size validation (e.g., max 5MB)
- Implement image optimization/compression before upload
- Add support for multiple images per card
- Create an admin view to manage all blob storage items

## Support

For Vercel Blob issues, consult:
- [Vercel Blob Documentation](https://vercel.com/docs/storage/vercel-blob)
- [Vercel Support](https://vercel.com/support)

For application-specific issues, check the application logs in `Program.cs` startup or the `VercelBlobStorageService` logger.
