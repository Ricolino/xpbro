using xpbro.DomainModels;

namespace xpbro.DataAccess.Interfaces;

public interface IPostsRepository
{
    Task<List<Post>> GetPublishedPostsAsync();
}
