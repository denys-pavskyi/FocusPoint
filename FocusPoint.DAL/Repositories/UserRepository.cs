using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Repositories.Interfaces;

namespace FocusPoint.DAL.Repositories;

public class UserRepository: IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
}