using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtSecret);

                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                }, out var validatedToken);

                context.Items["SupabaseUser"] = validatedToken;
            }
            catch
            {
                // token invalid → ignorăm, nu blocăm request-ul
            }
        }

        await _next(context);
    }
}
