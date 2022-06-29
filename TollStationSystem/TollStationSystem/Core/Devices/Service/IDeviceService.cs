using System.Collections.Generic;
using TollStationSystem.Core.Devices.Model;

namespace TollStationSystem.Core.Devices.Service
{
    public interface IDeviceService
    {
        List<Device> Devices { get; }

        void Add(Device device);
        Device FindById(int id);
        List<Device> GenerateDevices();
        int GenerateId();
        void Load();
        void Serialize();
        void Fix(Device device);
    }
}