using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net.Sockets;

namespace Lab1___prawidlowe
{
    class DevicesSzukajka
    {
        public async Task<IList<Device>> GetDevices()
        {
            // for not block the UI it will run in a different threat
            var task = Task.Run(() =>
            {
                var devices = new List<Device>();
                using (var bluetoothClient = new BluetoothClient())
                {
                    var array = bluetoothClient.DiscoverDevices();
                    var count = array.Length;
                    for (var i = 0; i < count; i++)
                    {
                        devices.Add(new Device(array[i]));
                    }
                }
                return devices;
            });
            return await task;
        }
    }
}
