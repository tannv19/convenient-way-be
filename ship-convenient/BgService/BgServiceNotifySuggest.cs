using ship_convenient.Core.IRepository;
using ship_convenient.Core.UnitOfWork;
using ship_convenient.Entities;
using ship_convenient.Services.FirebaseCloudMsgService;
using ship_convenient.Services.PackageService;
using unitofwork_core.Constant.Package;

namespace ship_convenient.BgService
{
    public class BgServiceNotifySuggest : BackgroundService
    {
        private readonly ILogger<BgServiceNotifySuggest> _logger;
        private readonly IServiceScopeFactory _serviceProvider;

        public BgServiceNotifySuggest(ILogger<BgServiceNotifySuggest> logger, IServiceScopeFactory serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    // Inject service
                    IUnitOfWork _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    IPackageService _packageService = scope.ServiceProvider.GetRequiredService<IPackageService>();
                    PackageUtils _packageUtils = scope.ServiceProvider.GetRequiredService<PackageUtils>();
                    IFirebaseCloudMsgService _fcmService = scope.ServiceProvider.GetRequiredService<IFirebaseCloudMsgService>();
                    await SuggestNotificationProcess(_fcmService, _unitOfWork, _packageService, _packageUtils);
                    if (DateTime.UtcNow.Minute == 0) {
                        await SuggestNotificationProcess(_fcmService, _unitOfWork, _packageService, _packageUtils);
                        await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
                    }
                }

            }
        }

        public async Task SuggestNotificationProcess(
            IFirebaseCloudMsgService fcmService, IUnitOfWork unitOfWork, IPackageService packageService, PackageUtils packageUtils)
        {
            if (DateTime.Now.Minute == 0) {

                IPackageRepository packageRepo = unitOfWork.Packages;
                List<Package> approvedPackage = await packageRepo.GetAllAsync(
                    predicate: item => item.Status == PackageStatus.APPROVED);
                _logger.LogInformation($"Số lượng gói hàng đã được duyệt {approvedPackage.Count}");
                for (int i = 0; i < approvedPackage.Count; i++)
                {
                    try
                    {
                        await packageUtils.NotificationValidUserWithPackageV2(approvedPackage[i]);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Error when send notification to user suggest");
                    }
                }
            }

        }
    }
}
