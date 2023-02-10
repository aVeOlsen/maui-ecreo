using MauiEcreoLib;
using MauiTemplateEcreo.Model;
using MvvmHelpers;

namespace MauiTemplateEcreo.Services
{
    public interface IUserDbService
    {
        Task AddUser(string firstname, string lastname, bool admin, Role role, string email, string phone, string address, string password);
        Task<User> GetItemAsync(string id);
        Task<UserModel> GetUserAsync(string id);
        Task<UserGetModel> GetUserModelAsync(string id);
        Task<IEnumerable<UserGetModel>> GetUsersAsync();
        Task RemoveUser(string id);
        Task<UserGetModel> UpdateUserAbsenceAsync(UserGetModel user);
        Task<User> UpdateUserAsync(User user);
        Task<UserGetModel> UpdateUserModelAsync(UserGetModel user);
        Task<UserModel> UpdateUserModelAsync(UserModel user);
    }
}