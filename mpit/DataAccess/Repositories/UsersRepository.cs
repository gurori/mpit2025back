using AutoMapper;
using Microsoft.EntityFrameworkCore;
using mpit.Core.Models;
using mpit.DataAccess.Entities;

namespace mpit.DataAccess.Repositories;

public sealed class UsersRepository(ApplicationDbContext context, IMapper mapper)
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> TryCreateAsync(string login, string passwordHash)
    {
        var user = new UserEntity();

        if (login.Contains('@'))
        {
            if (await _context.Users.AnyAsync(x => x.Email == login))
            {
                return true;
            }
            user.Email = login;
        }
        else
        {
            if (await _context.Users.AnyAsync(x => x.PhoneNumber == login))
            {
                return true;
            }
            user.PhoneNumber = login;
        }
        user.PasswordHash = passwordHash;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return false;
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        UserEntity? userEntity;
        if (login.Contains('@'))
        {
            userEntity = await _context
                .Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == login);
        }
        else
        {
            userEntity = await _context
                .Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.PhoneNumber == login);
        }
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
}
