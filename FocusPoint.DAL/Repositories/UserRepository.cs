using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Entities;
using FocusPoint.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FocusPoint.DAL.Repositories;

public class UserRepository: IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var userByUsername = await _context.Users.FirstOrDefaultAsync(user => string.Equals(user.Username, username));

        return userByUsername;
    }




}