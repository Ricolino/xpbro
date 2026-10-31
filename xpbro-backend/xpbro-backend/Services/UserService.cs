using Supabase;
using Supabase.Gotrue;
using xpbro.DomainModels;
using xpbro_backend.Interfaces;

namespace xpbro_backend.Services
{
    public class UserService : IUserService
    {
        public SupabaseOptions options = new()
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = false
        };
        public async Task<UserProfile> GetUserProfileAsync(Guid userId)
        {
            var client = new Supabase.Client(
    "https://YOUR_PROJECT.supabase.co",
    "YOUR_SUPABASE_ANON_KEY",
    options
);

            await client.InitializeAsync();

            var session = await client.Auth.SignIn("minghir@gmail.com", "Xz29htGrS4a6Gu#");

            var user = client.Auth.CurrentUser;

            Console.WriteLine(user.Id);
            Console.WriteLine(user.Email);
            Console.WriteLine(user.UserMetadata);
            Console.WriteLine(user.AppMetadata);

            var fullName = user.UserMetadata["full_name"]?.ToString();
            var role = user.UserMetadata["role"]?.ToString();

            Console.WriteLine(fullName);
            Console.WriteLine(role);

            //var users = await adminClient.Auth.Admin.ListUsers();

            //foreach (var u in users)
            //{
            //    Console.WriteLine($"{u.Id} - {u.Email}");
            //}

            // Return a dummy user profile for demonstration purposes
            return new UserProfile();
        }
    }
}
