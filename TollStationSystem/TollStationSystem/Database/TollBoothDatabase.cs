using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.DeploymentHistory.Repository;
using TollStationSystem.Core.Devices.Repository;
using TollStationSystem.Core.Locations.Repository;
using TollStationSystem.Core.Payments.Repository;
using TollStationSystem.Core.PriceLists.Repository;
using TollStationSystem.Core.Sections.Repository;
using TollStationSystem.Core.SpeedingPenalties.Repository;
using TollStationSystem.Core.TollBooths.Repository;
using TollStationSystem.Core.TollStations.Repository;
using TollStationSystem.Core.Users.Repository;

namespace TollStationSystem
{
    public enum VehicleType { CLASS_1A, CLASS_1, CLASS_2, CLASS_3, CLASS_4 }

    public enum TollBoothType { MANUAL, AUOTOMATIC }

    public enum UserType { CLERK, STATION_WORKER, MANAGER, ADMINISTRATOR, BOSS }

    public enum DeviceType { RAMP, SEMAPHORE, TAG_READER }
}

namespace TollStationSystem.Database
{
    
    public class TollBoothDatabase
    {
        IDeploymentHistoryRepo deploymentHistoryRepo;
        IDeviceRepo deviceRepo;
        ILocationRepo locationRepo;
        IPaymentRepo paymentRepo;
        IPriceListRepo priceListRepo;
        ISectionRepo sectionRepo;
        ISpeedingPenaltyRepo speedingPenaltyRepo;
        ITollBoothRepo tollBoothRepo;
        ITollStationRepo tollStationRepo;
        IBossRepo bossRepo;
        ITagUserRepo tagUserRepo;
        IUserRepo userRepo;

        public TollBoothDatabase()
        {
            deploymentHistoryRepo = new DeploymentHistoryRepo();
            deviceRepo = new DeviceRepo();
            locationRepo = new LocationRepo();
            paymentRepo = new PaymentRepo();
            priceListRepo = new PriceListRepo();
            sectionRepo = new SectionRepo();
            speedingPenaltyRepo = new SpeedingPenaltyRepo();
            tollBoothRepo = new TollBoothRepo();
            tollStationRepo = new TollStationRepo();
            bossRepo = new BossRepo();
            tagUserRepo = new TagUserRepo();
            userRepo = new UserRepo();
        }

        public IDeploymentHistoryRepo DeploymentHistoryRepo { get => deploymentHistoryRepo; }

        public IDeviceRepo DeviceRepo { get => deviceRepo; set => deviceRepo = value; }

        public ILocationRepo LocationRepo { get => locationRepo; }

        public IPaymentRepo PaymentRepo { get => paymentRepo; }

        public IPriceListRepo PriceListRepo { get => priceListRepo; }

        public ISpeedingPenaltyRepo SpeedingPenaltyRepo { get => speedingPenaltyRepo; }

        public ITollStationRepo TollStationRepo { get => tollStationRepo; }

        public IBossRepo BossRepo { get => bossRepo; }

        public ITagUserRepo TagUserRepo { get => tagUserRepo; }

        public IUserRepo UserRepo { get => userRepo; }

        public ISectionRepo SectionRepo { get => sectionRepo; }

        public ITollBoothRepo TollBoothRepo { get => tollBoothRepo; }
    }
}
