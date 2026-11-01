using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace xpbro_backend.Middleware;

public class SupabaseJwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _jwtSecret;

    public SupabaseJwtMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _jwtSecret = config["Supabase:AnonKey"];
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? authHeader = context.Request.Headers.Authorization.FirstOrDefault();

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader["Bearer ".Length..];

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtSecret);

                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };

                var principal = handler.ValidateToken(token, parameters, out var validatedToken);

                var jwt = (JwtSecurityToken)validatedToken;

                var userId = jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                var role = jwt.Claims.FirstOrDefault(c => c.Type == "user_role")?.Value;

                context.Items["UserId"] = userId;
                context.Items["UserRole"] = role;
            }
            catch (Exception)
            {
                // Token invalid → nu blocăm request-ul, doar nu setăm user-ul
            }
        }

        await _next(context);
    }
}
