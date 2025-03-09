using FocusPoint.DAL.Entities;

namespace FocusPoint.DAL.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
}