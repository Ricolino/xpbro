using xpbro.DataAccess.Interfaces;
using xpbro.DomainModels;
using xpbro_backend.Interfaces;

namespace xpbro_backend.Services
{
    public class PostsService(IPostsRepository repo) : IPostService
    {
        public async Task<List<Post>> GetPublishedPostsAsync()
        {
            return await repo.GetPublishedPostsAsync();
        }
    }
}
