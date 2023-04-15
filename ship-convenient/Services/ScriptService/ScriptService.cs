using Bogus;
using ship_convenient.Constants.AccountConstant;
using ship_convenient.Constants.ConfigConstant;
using ship_convenient.Core.CoreModel;
using ship_convenient.Core.UnitOfWork;
using ship_convenient.Entities;
using ship_convenient.Model.PackageModel;
using ship_convenient.Services.GenericService;
using ship_convenient.Services.PackageService;
using unitofwork_core.Constant.Package;
using Route = ship_convenient.Entities.Route;

namespace ship_convenient.Services.ScriptService
{
    public class ScriptService : GenericService<ScriptService>,IScriptService
    {
        private readonly IPackageService _packageService;
       
        private const string MarkScript = "[script]";
        double minLongitude = 106.60934755879953;
        double maxLongitude = 106.82648934410292;
        double minLatitude = 10.77371671523056;
        double maxLatitude = 10.843294269787952;
        public ScriptService(ILogger<ScriptService> logger, IUnitOfWork unitOfWork, IPackageService packageService) : base(logger, unitOfWork)
        {
            this._packageService = packageService;
        }
       
        public async Task<ApiResponse> CreatePackages()
        {
            ApiResponse response = new();
            List<Account> scriptSenders = await _accountRepo.GetAllAsync(
                predicate: acc => acc.UserName.Contains(MarkScript));
            List<Guid> senderIds = scriptSenders.Select(a => a.Id).ToList();
            Faker<Package> FakerPackage = new Faker<Package>()
               .RuleFor(o => o.StartAddress, faker => faker.Address.FullAddress() + MarkScript)
               .RuleFor(o => o.StartLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
               .RuleFor(o => o.StartLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
               .RuleFor(o => o.DestinationAddress, faker => faker.Address.FullAddress())
               .RuleFor(o => o.DestinationLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
               .RuleFor(o => o.DestinationLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
               .RuleFor(o => o.Distance, faker => faker.Random.Double(min: 2.5, max: 20))
               .RuleFor(o => o.PickupName, faker => faker.Person.FullName)
               .RuleFor(o => o.PickupPhone, faker => faker.Person.Phone)
               .RuleFor(o => o.ReceiverName, faker => faker.Person.FullName)
               .RuleFor(o => o.ReceiverPhone, faker => faker.Person.Phone)
               .RuleFor(o => o.Height, faker => faker.Random.Double(min: 0.2, max: 0.8))
               .RuleFor(o => o.Width, faker => faker.Random.Double(min: 0.2, max: 0.8))
               .RuleFor(o => o.Length, faker => faker.Random.Double(min: 0.2, max: 0.8))
               .RuleFor(o => o.Weight, faker => faker.Random.Int(5, 20))
               .RuleFor(o => o.PhotoUrl, faker => faker.Image.ToString())
               .RuleFor(o => o.Note, faker => faker.Lorem.Sentence(6))
               .RuleFor(o => o.PriceShip, faker => faker.Random.Int(min: 10, max: 40) * 1000)
               .RuleFor(o => o.Status, faker => PackageStatus.WAITING)
               .RuleFor(o => o.SenderId, faker => faker.PickRandom(senderIds));
            List<Package> packages = FakerPackage.Generate(100);
            for (int i = 0; i < packages.Count; i++)
            {
                Faker<Product> FakerProduct = new Faker<Product>()
                       .RuleFor(p => p.Name, faker => string.Join(" ", faker.Lorem.Words()))
                       .RuleFor(p => p.Price, faker => faker.Random.Int(20, 150) * 1000)
                       .RuleFor(p => p.Description, faker => faker.Lorem.Sentence(1));
                List<Product> products = FakerProduct.Generate(2);
                packages[i].Products = products;
            }
            await _packageRepo.InsertAsync(packages);
            await _unitOfWork.CompleteAsync();
            string log = "";
            for (int i = 0; i < packages.Count; i++)
            {
                log += i + ". "+ packages[i].Id.ToString() + "\n";
            }
            response.ToSuccessResponse("Đã tạo 100 gói hàng\n" + log);
            return response;
        }

        public Task<ApiResponse> DeliveredPackages()
        {
            throw new NotImplementedException();


        }

        public async Task<ApiResponse> PickupPackages()
        {
            ApiResponse response = new ApiResponse();
            List<Package> packages = _unitOfWork.Packages.GetAll(predicate: p => p.StartAddress.Contains(MarkScript) && p.Status == PackageStatus.SELECTED);
            List<Package> packagesSuccess = packages.Skip(0).Take(40).ToList();
            List<Package> packagesFailed = packages.Skip(40).Take(20).ToList();

            string logTotal = $"{packages.Count} đang chờ lấy\n";
            string logSuccess = $"{packagesSuccess.Count} đã được lấy thành công\n";
            for (int i = 0; i < packagesSuccess.Count; i++)
            {
                await _packageService.PickupPackageSuccess(packagesSuccess[i].Id);
                logSuccess += $"{i}. {packagesSuccess[i].Id}\n";
            }
            List<string> reasonPickupFailed = new List<string> { 
                "Không liên lạc được", "Hàng quá khổ", "Hàng không giống ảnh", "Người gửi bận"
            };
            string logFailed = $"{packagesFailed.Count} đã lấy thất bại\n";
            for (int i = 0; i < packagesFailed.Count; i++)
            {
                Random random = new Random();
                PickupPackageFailedModel model = new PickupPackageFailedModel();
                model.PackageId = packagesFailed[i].Id;
                model.Reason = reasonPickupFailed[random.Next(0, 3)];
                await _packageService.PickupPackageFailed(model);
                logFailed += $"{i}. {packages[i].Id} - {model.Reason}\n";
            }
            logTotal += logSuccess + "===========\n" + logFailed;
            response.ToSuccessResponse(logTotal);
            return response;
        }

        public async Task<ApiResponse> SelectedPackages()
        {
            ApiResponse response = new();
            List<Account> deliverScript = _accountRepo.GetAll(
              predicate: acc => acc.UserName.Contains(MarkScript) && acc.Role == RoleName.DELIVER);
            List<Package> packagesScript = _packageRepo.GetAll(
                predicate: p => p.Sender.UserName.Contains(MarkScript));
            int indexLog = 0;
            string logSelected = "";
            for (int i = 0; i < deliverScript.Count; i++)
            {
                List<Package> packageForDeliver = packagesScript.Skip(i * 3).Take(3).ToList();
                List<Guid> packageIds = packageForDeliver.Select(p => p.Id).ToList();
                for (int j = 0; j < packageForDeliver.Count; j++)
                {
                    logSelected += indexLog + ". " + packageForDeliver[j].Id.ToString() + $"Đã được nhận bởi ({deliverScript[i].GetFullName()})" +"\n";
                    indexLog++;
                }
                await _packageService.DeliverSelectedPackages(deliverScript[i].Id, packageIds, isScript: true);
            }
            response.ToSuccessResponse($"{indexLog} gói hàng đã được nhận\n" + logSelected);
            return response;
        }

        public void CreateDefaultConfig(InfoUser infoUser)
        {
            ConfigUser configPackageDistance = new ConfigUser();
            configPackageDistance.Name = DefaultUserConfigConstant.PACKAGE_DISTANCE;
            configPackageDistance.Value = DefaultUserConfigConstant.DEFAULT_PACKAGE_DISTANCE_VALUE;
            configPackageDistance.InfoUser = infoUser;

            ConfigUser configWarningPrice = new ConfigUser();
            configWarningPrice.Name = DefaultUserConfigConstant.WARNING_PRICE;
            configWarningPrice.Value = DefaultUserConfigConstant.DEFAULT_WARNING_PRICE_VALUE;
            configWarningPrice.InfoUser = infoUser;

            ConfigUser configDirectionSuggest = new ConfigUser();
            configDirectionSuggest.Name = DefaultUserConfigConstant.DIRECTION_SUGGEST;
            configDirectionSuggest.Value = DefaultUserConfigConstant.DEFAULT_DIRECTION_SUGGEST_VALUE_TWO_WAY;
            configDirectionSuggest.InfoUser = infoUser;

            ConfigUser configIsActive = new ConfigUser();
            configIsActive.Name = DefaultUserConfigConstant.IS_ACTIVE;
            configIsActive.Value = "TRUE";
            configIsActive.InfoUser = infoUser;

            infoUser.ConfigUsers.Add(configWarningPrice);
            infoUser.ConfigUsers.Add(configPackageDistance);
            infoUser.ConfigUsers.Add(configDirectionSuggest);
            infoUser.ConfigUsers.Add(configIsActive);
        }

        public async Task<ApiResponse> RemoveScriptData()
        {
            ApiResponse response = new();
            List<Account> accounts = _unitOfWork.Accounts.GetAll(predicate: a => a.UserName.Contains(MarkScript)).ToList();
            _unitOfWork.Accounts.DeleteRange(accounts);

            List<Package> packages = _unitOfWork.Packages.GetAll(predicate: p => p.StartAddress.Contains(MarkScript));
            _unitOfWork.Packages.DeleteRange(packages);

            await _unitOfWork.CompleteAsync();
            response.ToSuccessResponse("Đã xóa dữ liệu script");
            return response;
        }

        public async Task<ApiResponse> CreateActiveAccount(int deliverCount, int senderCount)
        {
            ApiResponse response = new ApiResponse();
            List<string> avatarsLink = new List<string>();
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4333/4333609.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/2202/2202112.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4140/4140047.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/236/236832.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/3006/3006876.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4333/4333609.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4140/4140048.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/924/924874.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4202/4202831.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/921/921071.png");
            Faker<Account> FakerAccount = new Faker<Account>()
                .RuleFor(u => u.UserName, faker => faker.Person.UserName + MarkScript)
                .RuleFor(u => u.Password, faker => faker.Person.FirstName.ToLower())
                .RuleFor(u => u.Role, faker => RoleName.DELIVER)
                .RuleFor(u => u.Balance, faker => 50000000)
                .RuleFor(u => u.Status, faker => "ACTIVE");
            Faker<Account> FakerAccountSender = new Faker<Account>()
            .RuleFor(u => u.UserName, faker => faker.Person.UserName + MarkScript)
            .RuleFor(u => u.Password, faker => faker.Person.FirstName.ToLower())
            .RuleFor(u => u.Role, faker => RoleName.SENDER)
            .RuleFor(u => u.Balance, faker => 50000000)
            .RuleFor(u => u.Status, faker => "ACTIVE");
            Faker<InfoUser> FakerInfoUser = new Faker<InfoUser>()
                 .RuleFor(u => u.Email, faker => faker.Person.Email)
                .RuleFor(u => u.FirstName, faker => faker.Person.FirstName)
                .RuleFor(u => u.LastName, faker => faker.Person.LastName)
                .RuleFor(u => u.PhotoUrl, faker => faker.PickRandom(avatarsLink))
                .RuleFor(u => u.Gender, faker => faker.PickRandom(AccountGender.GetAll()))
                .RuleFor(u => u.Latitude, faker => faker.Random.Double(minLatitude, maxLatitude))
                .RuleFor(u => u.Longitude, faker => faker.Random.Double(minLongitude, maxLongitude))
                .RuleFor(u => u.Phone, faker => faker.Person.Phone);
            Faker<Route> FakerRoute = new Faker<Route>()
              .RuleFor(r => r.IsActive, faker => true)
              .RuleFor(r => r.FromName, faker => faker.Person.Address.Street)
              .RuleFor(u => u.FromLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
              .RuleFor(u => u.FromLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
              .RuleFor(r => r.ToName, faker => faker.Person.Address.Street)
              .RuleFor(u => u.ToLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
              .RuleFor(u => u.ToLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude));
            List<Account> accountSenders = FakerAccountSender.Generate(senderCount);
            List<Account> accounts = FakerAccount.Generate(deliverCount);
            for (int i = 0; i < accounts.Count; i++)
            {
                InfoUser infoUser = FakerInfoUser.Generate();
                CreateDefaultConfig(infoUser);
                infoUser.Routes.Add(FakerRoute.Generate());
                accounts[i].InfoUser = infoUser;

            }
            for (int i = 0; i < accountSenders.Count; i++)
            {
                InfoUser infoUser = FakerInfoUser.Generate();
                accountSenders[i].InfoUser = infoUser;
            }

            await _unitOfWork.Accounts.InsertAsync(accounts);
            await _unitOfWork.Accounts.InsertAsync(accountSenders);
            await _unitOfWork.CompleteAsync();
            response.ToSuccessResponse($"Đã tạo {deliverCount} người lấy hàng dùm và {senderCount} người nhờ lấy hàng");
            return response;
        }

        public async Task<ApiResponse> CreatePackages(int packageCount)
        {
            ApiResponse response = new();
            List<Account> scriptSenders = await _accountRepo.GetAllAsync(
                predicate: acc => acc.UserName.Contains(MarkScript));
            List<Guid> senderIds = scriptSenders.Select(a => a.Id).ToList();
            Faker<Package> FakerPackage = new Faker<Package>()
               .RuleFor(o => o.StartAddress, faker => faker.Address.FullAddress() + MarkScript)
               .RuleFor(o => o.StartLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
               .RuleFor(o => o.StartLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
               .RuleFor(o => o.DestinationAddress, faker => faker.Address.FullAddress())
               .RuleFor(o => o.DestinationLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
               .RuleFor(o => o.DestinationLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
               .RuleFor(o => o.Distance, faker => faker.Random.Double(min: 2.5, max: 20))
               .RuleFor(o => o.PickupName, faker => faker.Person.FullName)
               .RuleFor(o => o.PickupPhone, faker => faker.Person.Phone)
               .RuleFor(o => o.ReceiverName, faker => faker.Person.FullName)
               .RuleFor(o => o.ReceiverPhone, faker => faker.Person.Phone)
               .RuleFor(o => o.Height, faker => faker.Random.Double(min: 0.2, max: 0.8))
               .RuleFor(o => o.Width, faker => faker.Random.Double(min: 0.2, max: 0.8))
               .RuleFor(o => o.Length, faker => faker.Random.Double(min: 0.2, max: 0.8))
               .RuleFor(o => o.Weight, faker => faker.Random.Int(5, 20))
               .RuleFor(o => o.PhotoUrl, faker => faker.Image.ToString())
               .RuleFor(o => o.Note, faker => faker.Lorem.Sentence(6))
               .RuleFor(o => o.PriceShip, faker => faker.Random.Int(min: 10, max: 40) * 1000)
               .RuleFor(o => o.Status, faker => PackageStatus.WAITING)
               .RuleFor(o => o.SenderId, faker => faker.PickRandom(senderIds));
            List<Package> packages = FakerPackage.Generate(packageCount);
            for (int i = 0; i < packages.Count; i++)
            {
                Faker<Product> FakerProduct = new Faker<Product>()
                       .RuleFor(p => p.Name, faker => string.Join(" ", faker.Lorem.Words()))
                       .RuleFor(p => p.Price, faker => faker.Random.Int(20, 150) * 1000)
                       .RuleFor(p => p.Description, faker => faker.Lorem.Sentence(1));
                List<Product> products = FakerProduct.Generate(2);
                packages[i].Products = products;
            }
            await _packageRepo.InsertAsync(packages);
            await _unitOfWork.CompleteAsync();
            string log = "";
            for (int i = 0; i < packages.Count; i++)
            {
                log += i + ". " + packages[i].Id.ToString() + "\n";
            }
            response.ToSuccessResponse("Đã tạo 100 gói hàng\n" + log);
            return response;
        }

        public async Task<ApiResponse> ApprovedPackages(int packageCount)
        {
            ApiResponse response = new ApiResponse();
            List<Package> packages = _unitOfWork.Packages.GetAll(predicate: p => p.StartAddress.Contains(MarkScript));
            int indexPackage = 0;
            string log = "";
            for (int i = 0; i < packageCount; i++)
            {
                await _packageService.ApprovedPackage(packages[i].Id, isNotify: false);
                log += $"{indexPackage}. {packages[i].Id} đã được duyệt";
                indexPackage++;
            }
            response.ToSuccessResponse($"{indexPackage} gói hàng đã được duyệt\n {log}");
            return response;
        }

        public async Task<ApiResponse> SelectedPackages(int selectedSuccess)
        {
            ApiResponse response = new();
            List<Account> deliverScript = _accountRepo.GetAll(
              predicate: acc => acc.UserName.Contains(MarkScript) && acc.Role == RoleName.DELIVER);
            List<Package> packagesScript = _packageRepo.GetAll(
                predicate: p => p.StartAddress.Contains(MarkScript));
            List<Package> packageSuccess = packagesScript.Skip(0).Take(selectedSuccess).ToList();
            int indexLog = 0;
            string logSelected = "";
            for (int i = 0; i < deliverScript.Count; i++)
            {
                List<Package> packageForDeliver = packagesScript.Skip(i * 3).Take(3).ToList();
                List<Guid> packageIds = packageForDeliver.Select(p => p.Id).ToList();
                for (int j = 0; j < packageForDeliver.Count; j++)
                {
                    logSelected += indexLog + ". " + packageForDeliver[j].Id.ToString() + $"Đã được nhận bởi ({deliverScript[i].GetFullName()})" + "\n";
                    indexLog++;
                }
                await _packageService.DeliverSelectedPackages(deliverScript[i].Id, packageIds, isScript: true);
            }
            response.ToSuccessResponse($"{indexLog} gói hàng đã được nhận\n" + logSelected);
            return response;
        }

        public async Task<ApiResponse> PickupPackages(int pickupSuccess, int pickupFailed)
        {
            ApiResponse response = new ApiResponse();
            List<Package> packages = _unitOfWork.Packages.GetAll(predicate: p => p.StartAddress.Contains(MarkScript) && p.Status == PackageStatus.SELECTED);
            List<Package> packagesSuccess = packages.Skip(0).Take(pickupSuccess).ToList();
            List<Package> packagesFailed = packages.Skip(pickupSuccess).ToList();

            string logTotal = $"{packages.Count} đang chờ lấy\n";
            string logSuccess = $"{packagesSuccess.Count} đã được lấy thành công\n";
            for (int i = 0; i < packagesSuccess.Count; i++)
            {
                await _packageService.PickupPackageSuccess(packagesSuccess[i].Id);
                logSuccess += $"{i}. {packagesSuccess[i].Id}\n";
            }
            List<string> reasonPickupFailed = new List<string> {
                "Không liên lạc được", "Hàng quá khổ", "Hàng không giống ảnh", "Người gửi bận"
            };
            string logFailed = $"{packagesFailed.Count} đã lấy thất bại\n";
            for (int i = 0; i < packagesFailed.Count; i++)
            {
                Random random = new Random();
                PickupPackageFailedModel model = new PickupPackageFailedModel();
                model.PackageId = packagesFailed[i].Id;
                model.Reason = reasonPickupFailed[random.Next(0, 3)];
                await _packageService.PickupPackageFailed(model, isScript: true);
                logFailed += $"{i}. {packages[i].Id} - {model.Reason}\n";
            }
            logTotal += logSuccess + "===========\n" + logFailed;
            response.ToSuccessResponse(logTotal);
            return response;
        }

        public async Task<ApiResponse> DeliveredPackages(int deliveredSuccess, int deliveredFailed)
        {
            ApiResponse response = new ApiResponse();
            List<Package> packages = _unitOfWork.Packages.GetAll(predicate: p => p.StartAddress.Contains(MarkScript) && p.Status == PackageStatus.PICKUP_SUCCESS);
            List<Package> packagesSuccess = packages.Skip(0).Take(deliveredSuccess).ToList();
            List<Package> packagesFailed = packages.Skip(deliveredSuccess).Take(deliveredFailed).ToList();

            string logTotal = $"{packages.Count} Chờ giao hàng\n";
            string logSuccess = $"{packagesSuccess.Count} đã giao thành công\n";
            for (int i = 0; i < packagesSuccess.Count; i++)
            {
                await _packageService.DeliveredSuccess(packagesSuccess[i].Id, isScript: true);
                logSuccess += $"{i}. {packagesSuccess[i].Id}\n";
            }
            List<string> reasonDeliveredFailed = new List<string> {
                "Không liên lạc được", "Không chịu nhận", "Người nhận bận"
            };
            string logFailed = $"{packagesFailed.Count} đã giao thất bại\n";
            for (int i = 0; i < packagesFailed.Count; i++)
            {
                Random random = new Random();
                DeliveredFailedModel model = new DeliveredFailedModel();
                model.PackageId = packagesFailed[i].Id;
                model.Reason = reasonDeliveredFailed[random.Next(0, 3)];
                await _packageService.DeliveredFailed(model, isScript: true);
                logFailed += $"{i}. {packages[i].Id} - {model.Reason}\n";
            }
            logTotal += logSuccess + "===========\n" + logFailed;
            response.ToSuccessResponse(logTotal);
            return response;
        }
    }
}
