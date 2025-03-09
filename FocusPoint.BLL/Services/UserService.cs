using FocusPoint.BLL.Interfaces;
using FocusPoint.DAL.Entities;
using FocusPoint.DAL.Repositories.Interfaces;

namespace FocusPoint.BLL.Services;

public class UserService: IUserService
{

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userService)
    {
        _userRepository = userService;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        return user;
    }


}