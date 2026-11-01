using xpbro.DomainModels;

namespace xpbro_backend.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetPublishedPostsAsync();
    }
}
