﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollStationSystem.Core.Devices.Model;
using TollStationSystem.Core.Devices.Repository;

namespace TollStationSystem.Core.Devices.Service
{
    public class DeviceService : IDeviceService
    {
        IDeviceRepo deviceRepo;

        public DeviceService(IDeviceRepo deviceRepo)
        {
            this.deviceRepo = deviceRepo;
        }

        public List<Device> Devices { get => deviceRepo.Devices; }

        public void Add(Device device)
        {
            deviceRepo.Add(device);
        }

        public Device FindById(int id)
        {
            return deviceRepo.FindById(id);
        }

        public int GenerateId()
        {
            return deviceRepo.GenerateId();
        }

        public void Load()
        {
            deviceRepo.Load();
        }

        public void Serialize()
        {
            deviceRepo.Serialize();
        }
        public List<Device> GenerateDevices()
        {
            List<Device> devices = new();
            Device ramp = new Device(GenerateId(), "Ramp", false, DeviceType.RAMP);
            Device semaphore = new Device(GenerateId(), "Semaphore", false, DeviceType.SEMAPHORE);
            Device tagReader = new Device(GenerateId(), "Tag Reader", false, DeviceType.TAG_READER);
            deviceRepo.Add(ramp);
            deviceRepo.Add(semaphore);
            deviceRepo.Add(tagReader);
            devices.Add(ramp);
            devices.Add(semaphore);
            devices.Add(tagReader);
            return devices;
        }
    }
}
