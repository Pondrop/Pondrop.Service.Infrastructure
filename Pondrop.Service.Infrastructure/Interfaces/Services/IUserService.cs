using Pondrop.Service.Models.User;

namespace Pondrop.Service.Interfaces.Services;

public interface IUserService
{
    string GetMaterializedViewUserName();

    string CurrentUserId();

    string CurrentUserName();

    UserType CurrentUserType();

    bool SetCurrentUser(UserModel user);
}