//using Supabase;
//using Supabase.Gotrue;
//using Microsoft.Extensions.Configuration;

//public class SupabaseAuthService
//{
//    private readonly Supabase.Client _client;
//    private readonly Supabase.Client _adminClient;

//    public SupabaseAuthService(IConfiguration config)
//    {
//        string? url = config["Supabase:Url"];
//        string? anon = config["Supabase:AnonKey"];
//        string? service = config["Supabase:ServiceRoleKey"];

//        var options = new SupabaseOptions
//        {
//            AutoRefreshToken = true,
//            AutoConnectRealtime = false
//        };

//        _client = new Supabase.Client(url, anon, options);
//        _adminClient = new Supabase.Client(url, service, options);
//    }

//    public async Task InitializeAsync()
//    {
//        await _client.InitializeAsync();
//        await _adminClient.InitializeAsync();
//    }

//    // -------------------------
//    // AUTH PUBLIC (anon key)
//    // -------------------------

//    public async Task<Session> LoginAsync(string email, string password)
//    {
//        await InitializeAsync();
//        return await _client.Auth.SignIn(email, password);
//    }

//    public async Task<User> RegisterAsync(string email, string password)
//    {
//        await InitializeAsync();
//        Session? session = await _client.Auth.SignUp(email, password);
//        return session.User;
//    }

//    public User GetCurrentUser()
//    {
//        return _client.Auth.CurrentUser;
//    }

//    public async Task<Session> RefreshTokenAsync(string refreshToken)
//    {
//        await InitializeAsync();
//        return await _client.Auth.RefreshSession(refreshToken);
//    }

//    // -------------------------
//    // ADMIN (service_role key)
//    // -------------------------

//    public async Task<List<User>> GetAllUsersAsync()
//    {
//        await InitializeAsync();
//        var result = await _adminClient.Auth.Admin.ListUsers();
//        return result;
//    }

//    public async Task<User> GetUserByIdAsync(string id)
//    {
//        await InitializeAsync();
//        return await _adminClient.Auth.Admin.GetUserById(id);
//    }
//}
