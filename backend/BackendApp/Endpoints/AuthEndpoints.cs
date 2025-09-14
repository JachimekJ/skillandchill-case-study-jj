using BackendApp.DTOs;
using BackendApp.Services;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BackendApp.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/auth");

            // POST /api/auth/login
            group.MapPost("/login", async (UserLoginDto loginDto, IAuthService authService) =>
            {
                var response = await authService.Login(loginDto);
                return response is null ? Results.Unauthorized() : Results.Ok(response);
            });

            // POST /api/auth/change-password  (wymaga JWT)
            group.MapPost("/change-password",
                [Authorize] async (ChangePasswordDto body, ClaimsPrincipal principal, IAuthService authService) =>
                {
                    var idClaim = principal.FindFirst("id")?.Value;
                    if (string.IsNullOrEmpty(idClaim)) return Results.Unauthorized();

                    if (!int.TryParse(idClaim, out var userId)) return Results.Unauthorized();

                    var ok = await authService.ChangePassword(userId, body.OldPassword, body.NewPassword);
                    return ok ? Results.Ok() : Results.BadRequest("Invalid old password or policy not met.");
                });

            // POST /api/auth/unlock/{userId}  (tylko Admin/SuperAdmin)
            group.MapPost("/unlock/{userId:int}",
                [Authorize(Roles = "Admin,SuperAdmin")] async (int userId, ClaimsPrincipal principal, IAuthService authService) =>
                {
                    var actorIdStr = principal.FindFirst("id")?.Value;
                    if (string.IsNullOrEmpty(actorIdStr) || !int.TryParse(actorIdStr, out var actorId))
                        return Results.Unauthorized();

                    var ok = await authService.UnlockAccount(userId, actorId);
                    return ok ? Results.Ok() : Results.Forbid();
                });

            // GET /api/auth/me (prosty test JWT)
            group.MapGet("/me",
                [Authorize] (ClaimsPrincipal principal) =>
                {
                    var username = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                    var id = principal.FindFirst("id")?.Value;
                    var role = principal.FindFirst(ClaimTypes.Role)?.Value;

                    return Results.Ok(new { id, username, role });
                });
        }
    }
}