using ship_convenient.Constants.ConfigConstant;
using ship_convenient.Core.CoreModel;
using ship_convenient.Core.UnitOfWork;
using ship_convenient.Entities;
using ship_convenient.Model.ConfigModel;
using ship_convenient.Services.GenericService;
using unitofwork_core.Constant.ConfigConstant;

namespace ship_convenient.Services.ConfigService
{
    public class ConfigService : GenericService<ConfigService>, IConfigService
    {
        public ConfigService(ILogger<ConfigService> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
        {
        }

        public async Task<ApiResponse<List<ResponseConfigModel>>> GetAll()
        {
            ApiResponse<List<ResponseConfigModel>> response = new();
            List<ConfigApp> configDefault = await _configRepo.GetAllAsync(predicate: (c) => c.Type == TypeOfConfig.DEFAULT);
            List<ConfigApp> configPrice = await _configRepo.GetAllAsync(predicate: (c) => c.Type == TypeOfConfig.PRICE_DISTANCE);

            List<ResponseConfigModel> responseConfigDefault = configDefault.Select((c) => c.ToResponseModel()).ToList();
            ResponseConfigModel? configCalculateWith = responseConfigDefault.FirstOrDefault(predicate: (c) => c.Name == ConfigConstant.CALCULATE_PRICE_WITH);
            if (configCalculateWith != null)
            {
                configCalculateWith.configsChildren = configPrice.Select((c) => c.ToResponseModel()).ToList();
            }
            response.ToSuccessResponse(responseConfigDefault, "Lấy thông tin thành công");
            return response;
        }



        public async Task<ApiResponse<ConfigApp>> Update(UpdateConfigModel model)
        {
            ApiResponse<ConfigApp> response = new();
            ConfigApp? config = await _configRepo.FirstOrDefaultAsync((c) => c.Name == model.Name, disableTracking: false);
            if (config == null) {
                response.ToFailedResponse("Không tìm thấy thông tin cấu hình");
                return response;
            }
            config.Note = model.Note;
            config.ModifiedBy = model.ModifiedBy;
            int result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                response.ToSuccessResponse(config, "Cập nhật thông tin thành công");
            }
            else {
                response.ToFailedResponse("Cập nhật thông tin thất bại");
            }
            return response;
        }
    }
}
