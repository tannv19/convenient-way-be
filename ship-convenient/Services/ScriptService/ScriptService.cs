using Bogus;
using ship_convenient.Constants.AccountConstant;
using ship_convenient.Constants.ConfigConstant;
using ship_convenient.Core.CoreModel;
using ship_convenient.Core.UnitOfWork;
using ship_convenient.Entities;
using ship_convenient.Services.GenericService;
using unitofwork_core.Constant.Package;
using Route = ship_convenient.Entities.Route;

namespace ship_convenient.Services.ScriptService
{
    public class ScriptService : GenericService<ScriptService>,IScriptService
    {
        private const string MarkScript = "[script]";
        double minLongitude = 106.60934755879953;
        double maxLongitude = 106.82648934410292;
        double minLatitude = 10.77371671523056;
        double maxLatitude = 10.843294269787952;
        public ScriptService(ILogger<ScriptService> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork)
        {
        }

        public Task<ApiResponse> ApprovedPackages()
        {
            throw new NotImplementedException();
        }
        // acccount : username
        public async Task<ApiResponse> CreateActiveAccount()
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
            List<Account> accountSenders = FakerAccountSender.Generate(10);
            List<Account> accounts = FakerAccount.Generate(20);
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
            response.ToSuccessResponse("Đã tạo 20 người lấy hàng dùm và 10 người nhờ lấy hàng");
            return response;
        }

        public async Task<ApiResponse> CreatePackages()
        {
            ApiResponse response = new();
            List<Account> scriptSenders = await _accountRepo.GetAllAsync(
                predicate: acc => acc.UserName.Contains(MarkScript));
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
               .RuleFor(o => o.Sender, faker => faker.PickRandom(scriptSenders));
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
            response.ToSuccessResponse("Đã tạo 100 gói hàng");
            return response;
        }

        public Task<ApiResponse> DeliveredSuccessPackages()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> PickupSuccessPackages()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> SelectedPackages()
        {
            throw new NotImplementedException();
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
    }
}
