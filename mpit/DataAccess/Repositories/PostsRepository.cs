using Microsoft.EntityFrameworkCore;

public sealed class PostsRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task CreateAsync(Guid userId, PostRequest request)
    {
        var author = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        author.Coins += 40;

        var post = new Post
        {
            Id = Guid.NewGuid(),
            Author = author,
            AuthorId = userId,
            Description = request.Description,
            Name = request.Name,
        };

        await _context.Posts.AddAsync(post);
        _context.Users.Update(author);
        await _context.SaveChangesAsync();
    }

    public async Task<Post[]> GetAll()
    {
        return await _context.Posts.AsNoTracking().ToArrayAsync();
    }
}
