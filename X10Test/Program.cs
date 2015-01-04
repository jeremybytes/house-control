using HouseControl.Library;
using System;
using System.Threading;

namespace X10Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Test");

            var controller = new HouseController();
            controller.Commander = new FakeCommander();
            controller.SendCommand(5, DeviceCommands.On);
            controller.SendCommand(5, DeviceCommands.Off);

            Console.WriteLine("Test Completed");
            Console.ReadLine();
        }
    }
}
