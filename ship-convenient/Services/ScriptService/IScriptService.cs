using ship_convenient.Core.CoreModel;

namespace ship_convenient.Services.ScriptService
{
    public interface IScriptService
    {
        Task<ApiResponse> CreateActiveAccount();
        Task<ApiResponse> CreatePackages();
        Task<ApiResponse> ApprovedPackages();
        Task<ApiResponse> SelectedPackages();
        Task<ApiResponse> PickupSuccessPackages();
        Task<ApiResponse> DeliveredSuccessPackages();
        Task<ApiResponse> RemoveScriptData();
    }
}
