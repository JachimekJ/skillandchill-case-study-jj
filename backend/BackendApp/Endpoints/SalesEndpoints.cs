using BackendApp.DTOs;
using BackendApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace BackendApp.Endpoints
{
    public static class SalesEndpoints
    {
        public static void MapSalesEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/sales").RequireAuthorization();

            // GET /api/sales?distributorId=1
            group.MapGet("/", async (int distributorId, ISalesService salesService) =>
            {
                var sales = await salesService.GetSalesAsync(distributorId);
                return Results.Ok(sales);
            });

            // POST /api/sales
            group.MapPost("/", async (SalesDto dto, ISalesService salesService) =>
            {
                await salesService.AddSalesAsync(dto);
                return Results.Created($"/api/sales", new { message = "Sales data saved" });
            });
        }
    }
}
