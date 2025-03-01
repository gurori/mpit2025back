using AutoMapper;
using Microsoft.EntityFrameworkCore;
using mpit.Core.Models;
using mpit.DataAccess.Entities;

namespace mpit.DataAccess.Repositories;

public sealed class UsersRepository(ApplicationDbContext context, IMapper mapper)
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> TryCreateAsync(
        string email,
        string passwordHash,
        string firstName,
        string role
    )
    {
        System.Console.WriteLine("start");
        if (await _context.Users.AsNoTracking().AnyAsync(u => u.Login == email))
        {
            System.Console.WriteLine("already");
            return true;
        }
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            PasswordHash = passwordHash,
            FirstName = firstName,
            Role = role,
            Login = email,
        };
        System.Console.WriteLine("add");
        await _context.Users.AddAsync(user);
        System.Console.WriteLine("added");
        await _context.SaveChangesAsync();
        return false;
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        UserEntity? userEntity = await _context
            .Users.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == login);
        if (userEntity is null)
            return null;
        return _mapper.Map<User>(userEntity);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (userEntity is null)
            return null;

        return _mapper.Map<User>(userEntity);
    }

    public async Task<string?> GetRoleByIdAsync(Guid id)
    {
        var role = await _context
            .Users.AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => x.Role)
            .FirstOrDefaultAsync();
        return role;
    }
}
