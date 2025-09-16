using BackendApp.Data;
using BackendApp.DTOs;
using BackendApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Endpoints
{
    public static class PurchaseEndpoints
    {
        public static void MapPurchaseEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/purchase");

            // POST /api/purchase
            group.MapPost("/", [Authorize(Roles = "ExportManager,Admin,SuperAdmin")] async (
                PurchaseReportDto dto, AppDbContext db) =>
            {
                // Pobierz Total ze SalesReports dla danego dystrybutora i kwartału
                var sales = await db.SalesReports
                    .FirstOrDefaultAsync(s => s.DistributorId == dto.DistributorId && s.Quarter == dto.Quarter);

                var actual = sales?.Total ?? 0m;

                var report = new PurchaseReports
                {
                    DistributorId = dto.DistributorId,
                    Quarter = dto.Quarter,
                    LastYearSales = dto.LastYearSales,
                    Purchases = dto.Purchases,
                    Budget = dto.Budget,

                    ActualSales = actual,
                    YearVsLastYear = actual - dto.LastYearSales,
                    YearVsBudget = actual - dto.Budget,

                    TotalPOS = dto.TotalPOS,
                    NewOpenings = dto.NewOpenings,
                    OpeningsTarget = dto.OpeningsTarget
                };

                db.PurchaseReports.Add(report);
                await db.SaveChangesAsync();

                return Results.Created($"/api/purchase/{report.Id}", new { message = "Purchase report saved" });
            });

            // GET /api/purchase?distributorId=1
            group.MapGet("/", [Authorize(Roles = "ExportManager,Admin,SuperAdmin")] async (
                int distributorId, AppDbContext db) =>
            {
                var reports = await db.PurchaseReports
                    .Where(r => r.DistributorId == distributorId)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

                return Results.Ok(reports);
            });
        }
    }
}
