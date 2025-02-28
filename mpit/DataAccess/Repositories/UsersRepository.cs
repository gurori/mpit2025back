public sealed class UsersRepository(
    ApplicationDbContext context
) {

    private readonly ApplicationDbContext _context = context;

    public async Task CreateAsync(string login, string passwordHash) {

        var user = new UserEntity {
            PasswordHash = passwordHash
        };
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}