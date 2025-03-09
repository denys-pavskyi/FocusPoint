using FocusPoint.DAL.Entities;

namespace FocusPoint.BLL.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByUsernameAsync(string username);
}