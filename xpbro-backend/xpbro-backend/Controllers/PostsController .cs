using Microsoft.AspNetCore.Mvc;
using xpbro.DomainModels;
using xpbro_backend.Interfaces;

namespace xpbro_backend.Controllers;

[ApiController]
[Route("posts")]
public class PostsController(IPostService postService) : ControllerBase
{
    private readonly IPostService _postService = postService;

    [HttpGet("published")]
    public async Task<IActionResult> GetPublished()
    {
        List<Post> posts = await _postService.GetPublishedPostsAsync();
        
        return Ok(posts);
    }
}
