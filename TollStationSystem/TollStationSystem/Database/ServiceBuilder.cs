using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.DeploymentHistory.Service;
using TollStationSystem.Core.Devices.Service;
using TollStationSystem.Core.Locations.Service;
using TollStationSystem.Core.Payments.Service;
using TollStationSystem.Core.PriceLists.Service;
using TollStationSystem.Core.Sections.Service;
using TollStationSystem.Core.SpeedingPenalties.Service;
using TollStationSystem.Core.TollBooths.Service;
using TollStationSystem.Core.TollStations.Service;
using TollStationSystem.Core.Users.Service;

namespace TollStationSystem.Database
{
    public class ServiceBuilder
    {
        IDeploymentHistoryService deploymentHisyoryService;
        IDeviceService deviceService;
        ILocationService locationService;
        IPaymentService paymentService;
        IPriceListService priceListService;
        ISectionService sectionService;
        ISpeedingPenaltyService speedingPenaltyService;
        ITollBoothService tollBoothService;
        ITollStationService tollStationService;
        IBossService bossService;
        ITagUserService tagUserService;
        IUserService userService;

        public ServiceBuilder()
        {
            TollBoothDatabase tollBoothDatabase = new();
            deploymentHisyoryService = new DeploymentHistoryService(tollBoothDatabase.DeploymentHistoryRepo);
            deviceService = new DeviceService(tollBoothDatabase.DeviceRepo);
            locationService = new LocationService(tollBoothDatabase.LocationRepo);
            priceListService = new PriceListService(tollBoothDatabase.PriceListRepo);
            sectionService = new SectionService(tollBoothDatabase.SectionRepo);
            speedingPenaltyService = new SpeedingPenaltyService(tollBoothDatabase.SpeedingPenaltyRepo);
            tollBoothService = new TollBoothService(tollBoothDatabase.TollBoothRepo, deviceService);
            bossService = new BossService(tollBoothDatabase.BossRepo);
            paymentService = new PaymentService(tollBoothDatabase.PaymentRepo,priceListService,tollStationService);
            tollStationService = new TollStationService(tollBoothDatabase.TollStationRepo, tollBoothService);
            tagUserService = new TagUserService(tollBoothDatabase.TagUserRepo);
            userService = new UserService(tollBoothDatabase.UserRepo, bossService);
        }

        public IDeploymentHistoryService DeploymentHisyoryService { get => deploymentHisyoryService; }

        public IDeviceService DeviceService { get => deviceService; set => deviceService = value; }

        public ILocationService LocationService { get => locationService; }

        public IPaymentService PaymentService { get => paymentService; }

        public IPriceListService PriceListService { get => priceListService; }

        public ISectionService SectionService { get => sectionService; }

        public ISpeedingPenaltyService SpeedingPenaltyService { get => speedingPenaltyService; }

        public ITollBoothService TollBoothService { get => tollBoothService; }

        public ITollStationService TollStationService { get => tollStationService; }

        public IBossService BossService { get => bossService; }

        public ITagUserService TagUserService { get => tagUserService; }

        public IUserService UserService { get => userService; }
    }
}
