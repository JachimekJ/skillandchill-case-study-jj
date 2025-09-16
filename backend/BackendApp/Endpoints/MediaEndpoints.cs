using BackendApp.Data;
using BackendApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace BackendApp.Endpoints
{
    public static class MediaEndpoints
    {
        public static void MapMediaEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/media").RequireAuthorization();

            // GET /api/media -> lista wszystkich plików
            group.MapGet("/", async (AppDbContext db) =>
            {
                var files = await Task.FromResult(db.MediaFiles.ToList());
                return Results.Ok(files);
            });

            // GET /api/media/search?sku=SKU123 -> wyszukiwarka po SKU
            group.MapGet("/search", async (string sku, AppDbContext db) =>
            {
                var files = db.MediaFiles
                    .Where(f => f.Name.Contains(sku))
                    .ToList();
                return Results.Ok(files);
            });
        }
    }
}
