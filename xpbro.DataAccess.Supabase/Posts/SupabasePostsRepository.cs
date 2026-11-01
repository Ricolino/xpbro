using Microsoft.Extensions.Configuration;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using xpbro.DataAccess.Interfaces;
using xpbro.DomainModels;
using static Supabase.Postgrest.Constants;

namespace xpbro.DataAccess.Supabase.Posts;

public class SupabasePostsRepository : IPostsRepository
{
    private readonly Client _client;
    private bool _initialized = false;

    public SupabasePostsRepository(IConfiguration config)
    {
        string? url = config["Supabase:Url"];
        string? anonKey = config["Supabase:AnonKey"];

        var options = new SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = false
        };

        _client = new Client(url, anonKey, options);
    }

    private async Task EnsureInit()
    {
        if (!_initialized)
        {
            await _client.InitializeAsync();
            _initialized = true;
        }
    }

    public async Task<List<Post>> GetPublishedPostsAsync()
    {
        await EnsureInit();

        var result = await _client
            .From<SupabasePost>()
            .Select("title, body, published_at")
            .Filter("published", Operator.Equals, true)
            .Order("published_at", Ordering.Descending)
            .Get();

        return [.. result.Models
            .Select(x => new Post
            {
                Id = x.Id,
                Title = x.Title,
                Body = x.Body,
                Published = x.Published,
                PublishedAt = x.PublishedAt
            })];
    }
}

[Table("posts")]
public class SupabasePost : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("body")]
    public string Body { get; set; }

    [Column("published")]
    public bool Published { get; set; }

    [Column("published_at")]
    public DateTime? PublishedAt { get; set; }
}
