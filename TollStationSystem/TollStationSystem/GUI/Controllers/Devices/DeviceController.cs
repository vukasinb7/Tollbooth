using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.Devices.Service;

namespace TollStationSystem.GUI.Controllers.Devices
{
    public class DeviceController
    {
        IDeviceService deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        public List<Device> Devices { get => deviceService.Devices; }

        public void Add(Device device)
        {
            deviceService.Add(device);
        }

        public Device FindById(int id)
        {
            return deviceService.FindById(id);
        }

        public int GenerateId()
        {
            return deviceService.GenerateId();
        }

        public void Load()
        {
            deviceService.Load();
        }

        public void Serialize()
        {
            deviceService.Serialize();
        }
        public List<Device> GenerateDevices()
        {
            return deviceService.GenerateDevices();
        }

        public void Fix(Device device)
        {
            deviceService.Fix(device);
        }
        
        public void ReportMalfunction(int deviceId)
        {
            deviceService.ReportMalfunction(deviceId);
        }
    }
}
