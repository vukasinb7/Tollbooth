using System.Collections.Generic;
using TollStationSystem.Core.Devices.Model;

namespace TollStationSystem.Core.Devices.Repository
{
    public interface IDeviceRepo
    {
        List<Device> Devices { get; }

        void Add(Device device);
        Device FindById(int id);
        int GenerateId();
        void Load();
        void Serialize();
    }
}