using MauiEcreoLib;
using MauiTemplateEcreo.Model;

namespace MauiTemplateEcreo.Services
{
    public interface IAdminstratorService
    {
        Task<UserPersonalInfoModel> GetUserInfoAsync(string id);
        Task<UserPersonalInfoModel> GetUserPasswordAsync(string id);
        Task<UserGetModel> GetUserRoleAsync(string id);
        Task<Role> UpdateRoleAsync(Role role, string id);

    }
}