using BackendApp.DTOs;
using BackendApp.Services;

namespace BackendApp.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/auth");

            group.MapPost("/login", async (UserLoginDto loginDto, IAuthService authService) =>
            {
                var user = await authService.Login(loginDto);

                if (user == null)
                {
                    return Results.Unauthorized();
                }

                // Na razie zwracamy DTO — TODO: dodać JWT token
                return Results.Ok(user);
            });
        }
    }
}
