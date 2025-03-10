using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Repositories.Interfaces;

namespace FocusPoint.DAL.Repositories;

public class UserSettingRepository: IUserSettingRepository
{
    private readonly AppDbContext _context;

    public UserSettingRepository(AppDbContext context)
    {
        _context = context;
    }
}