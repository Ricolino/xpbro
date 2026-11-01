using xpbro.DataAccess.Interfaces;
using xpbro.DataAccess.Supabase.Posts;
using xpbro_backend.Interfaces;
using xpbro_backend.Middleware;
using xpbro_backend.Services;

//TODO: Add Serilog for logging
//TODO: Add Health Checks
//TODO: Add Seq sink maybe?

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddSingleton<SupabaseAuthService>();
builder.Services.AddScoped<IPostService, PostsService>();

builder.Services.AddScoped<IPostsRepository, SupabasePostsRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<SupabaseJwtMiddleware>();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
