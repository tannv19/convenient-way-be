using ship_convenient.Core.CoreModel;
using ship_convenient.Entities;
using ship_convenient.Model.ConfigModel;

namespace ship_convenient.Services.ConfigService
{
    public interface IConfigService
    {
        Task<ApiResponse<List<ResponseConfigModel>>> GetAll();
        Task<ApiResponse<ConfigApp>> Update(UpdateConfigModel model);
        Task<ApiResponse<List<ConfigPrice>>> GetAllConfigPrice();
        Task<ApiResponse<ConfigPrice>> UpdateConfigPrice(UpdateConfigPriceModel model);
        Task<ApiResponse<ConfigPrice>> CreateConfigPrice(CreateConfigPriceModel model);
    }
}
