public sealed class PostsRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task CreateAsync() { }
}
