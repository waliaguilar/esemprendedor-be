# Esemprendedor API

ASP.NET Core 9.0 API with Razor Pages dashboard for managing local business listings.

## Features

- **REST API**: CRUD operations for Sections, Cards, and SimpleCards
- **Razor Pages Dashboard**: Admin UI for managing content at `/Dashboard`
- **PostgreSQL Database**: Entity Framework Core with automatic migrations
- **Image Upload**: Vercel Blob Storage integration for card images
- **CORS Support**: Configured for Angular frontend integration
- **Swagger/OpenAPI**: Interactive API documentation at `/swagger`

## Tech Stack

- .NET 9.0
- ASP.NET Core (Web API + Razor Pages)
- Entity Framework Core
- PostgreSQL (Npgsql)
- Vercel Blob Storage
- Swagger/OpenAPI

## Prerequisites

1. [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
2. [PostgreSQL](https://www.postgresql.org/download/) (version 12 or higher)
3. [Vercel Account](https://vercel.com) (for image uploads - optional)

## Local Setup

### 1. Clone the Repository

```bash
git clone https://github.com/waliaguilar/esemprendedor-be.git
cd esemprendedor-be/EsemprendedorApi
```

### 2. Configure Database

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=esemprendedor;Username=postgres;Password=YOUR_PASSWORD;"
  }
}
```

**Default credentials** (if using default PostgreSQL installation):
- Host: `localhost`
- Port: `5432`
- Database: `esemprendedor` (will be created automatically)
- Username: `postgres`
- Password: Your PostgreSQL password

### 3. Configure Vercel Blob Storage (Optional)

For image upload functionality, configure Vercel Blob credentials:

```json
{
  "VercelBlob": {
    "Token": "vercel_blob_rw_YOUR_TOKEN_HERE",
    "StoreId": "YOUR_STORE_ID_HERE",
    "BaseUrl": "https://blob.vercel-storage.com"
  }
}
```

**See [VERCEL_BLOB_SETUP.md](VERCEL_BLOB_SETUP.md) for detailed instructions.**

> Note: The app will work without Vercel Blob configured, but image uploads will fail. You can still use image URLs manually.

### 4. Restore Dependencies

```bash
dotnet restore
```

### 5. Run the Application

```bash
dotnet run
```

The application will:
1. Automatically create the database if it doesn't exist
2. Apply all pending migrations
3. Seed initial data (Sections with sample cards)
4. Start the web server

### 6. Access the Application

- **API**: `https://localhost:7228/api` or `http://localhost:5023/api`
- **Swagger UI**: `https://localhost:7228/swagger`
- **Dashboard**: `https://localhost:7228/Dashboard/Sections`
  - Sections: `https://localhost:7228/Dashboard/Sections`
  - Cards: `https://localhost:7228/Dashboard/Cards`
  - SimpleCards: `https://localhost:7228/Dashboard/SimpleCards`

## Project Structure

```
EsemprendedorApi/
├── Controllers/          # API Controllers (REST endpoints)
├── Domain/
│   ├── Entities/        # Domain models (Section, Card, SimpleCard)
│   └── Interfaces/      # Repository interfaces
├── Application/
│   └── Services/        # Business logic services
│       ├── Interfaces/  # Service interfaces
│       └── VercelBlobStorageService.cs
├── Infrastructure/
│   ├── Configuration/   # Settings classes (VercelBlobSettings)
│   ├── Persistence/     # EF Core DbContext
│   └── Repositories/    # Data access implementations
├── Pages/
│   └── Dashboard/       # Razor Pages admin UI
│       ├── Sections/
│       ├── Cards/
│       └── SimpleCards/
├── wwwroot/
│   ├── css/            # Dashboard styles
│   └── mock/           # Mock data (cards.json)
├── Migrations/         # EF Core migrations
└── Program.cs          # Application startup
```

## Database Schema

### Sections
- Id (PK)
- Title
- Description
- CreatedAt
- UpdatedAt

### Cards
- Id (PK)
- SectionId (FK)
- Icon
- Chip
- Name
- Service
- Contact
- Featured
- BackgroundImage (Vercel Blob URL)
- Keywords
- CreatedAt
- UpdatedAt

### SimpleCards
- Id (PK)
- SectionId (FK)
- Name
- Service
- Contact
- CreatedAt
- UpdatedAt

## API Endpoints

### Sections
- `GET /api/sections` - List all sections
- `GET /api/sections/{id}` - Get section by ID
- `POST /api/sections` - Create section
- `PUT /api/sections/{id}` - Update section
- `DELETE /api/sections/{id}` - Delete section

### Cards
- `GET /api/cards` - List all cards
- `GET /api/cards/{id}` - Get card by ID
- `POST /api/cards` - Create card
- `PUT /api/cards/{id}` - Update card
- `DELETE /api/cards/{id}` - Delete card

### SimpleCards
- `GET /api/simplecards` - List all simple cards
- `GET /api/simplecards/{id}` - Get simple card by ID
- `POST /api/simplecards` - Create simple card
- `PUT /api/simplecards/{id}` - Update simple card
- `DELETE /api/simplecards/{id}` - Delete simple card

## Dashboard Features

The Razor Pages dashboard (`/Dashboard`) provides a full admin interface:

### Sections Dashboard
- Create/Edit/Delete sections
- View card counts per section
- Visual chart showing distribution
- Inline editing

### Cards Dashboard
- Create/Edit/Delete cards
- **Image Upload**: Upload card images to Vercel Blob
- **Image Preview**: Preview images before upload
- **Thumbnail View**: See uploaded images in table
- Mock data fallback when DB is empty
- Section filtering
- Featured card marking

### SimpleCards Dashboard
- Create/Edit/Delete simple cards
- Section assignment
- Table-based CRUD interface

## Development Commands

```bash
# Run the application
dotnet run

# Build the application
dotnet build

# Run tests (if available)
dotnet test

# Create a new migration
dotnet ef migrations add MigrationName

# Apply migrations manually
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## CORS Configuration

The API is configured to accept requests from:
- `http://localhost:4200` (Angular dev server)
- `https://localhost:4200`

Update `appsettings.json` to add more allowed origins:

```json
{
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:4200",
      "https://your-frontend-domain.com"
    ]
  }
}
```

## Environment Variables

For production deployment, use environment variables instead of `appsettings.json`:

```bash
# Database
ConnectionStrings__DefaultConnection="Host=...;Database=...;Username=...;Password=..."

# Vercel Blob
VercelBlob__Token="vercel_blob_rw_..."
VercelBlob__StoreId="your-store-id"
```

## Troubleshooting

### Database Connection Issues
1. Ensure PostgreSQL is running
2. Verify connection string credentials
3. Check PostgreSQL logs for authentication errors

### Migration Issues
```bash
# Reset database (WARNING: deletes all data)
dotnet ef database drop
dotnet ef database update
```

### Image Upload Issues
1. Check Vercel Blob credentials in `appsettings.Development.json`
2. Verify token has read-write permissions
3. Check application logs for detailed error messages
4. See [VERCEL_BLOB_SETUP.md](VERCEL_BLOB_SETUP.md) for detailed troubleshooting

### Port Already in Use
Update `Properties/launchSettings.json` to use different ports.

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License.

## Additional Documentation

- [Vercel Blob Setup Guide](VERCEL_BLOB_SETUP.md) - Detailed image upload configuration
- [API Documentation](https://localhost:7228/swagger) - Interactive Swagger UI (when running)

## Support

For issues or questions:
1. Check the [Troubleshooting](#troubleshooting) section
2. Review application logs
3. Open an issue on GitHub
