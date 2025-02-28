using Microsoft.EntityFrameworkCore;

public sealed class PermissionsRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HashSet<string>> GetPermissionsAsync(string roleName)
    {
        var permissions = await _context
            .Roles.Include(r => r.Permissions)
            .Where(r => r.Name.ToLower() == roleName)
            .Select(r => r.Permissions)
            .ToArrayAsync();

        return permissions.SelectMany(p => p).Select(p => p.Name).ToHashSet();
    }
}
