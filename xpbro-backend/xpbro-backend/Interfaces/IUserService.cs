using xpbro.DomainModels;

namespace xpbro_backend.Interfaces
{
    public interface IUserService
    {
        Task<UserProfile> GetUserProfileAsync(Guid userId);
    }
}
