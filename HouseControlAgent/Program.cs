﻿using HouseControl.Library;
using HouseControl.Sunset;
using Ninject;

namespace HouseControlAgent;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting Test");

        var controller = InitializeHouseController();

        // For hardware/scheduling testing purposes
        // Uncomment this section to ensure that the hardware
        // and scheduling is working as expected.

        //await controller.SendCommand(5, DeviceCommand.On);
        //await controller.SendCommand(5, DeviceCommand.Off);
        await Task.Delay(1); // placeholder to keep Main signature when test code is not used

        //var currentTime = DateTime.Now;
        //controller.ScheduleOneTimeItem(currentTime.AddMinutes(1), 3, DeviceCommand.On);
        //controller.ScheduleOneTimeItem(currentTime.AddMinutes(2), 5, DeviceCommand.On);
        //controller.ScheduleOneTimeItem(currentTime.AddMinutes(3), 3, DeviceCommand.Off);
        //controller.ScheduleOneTimeItem(currentTime.AddMinutes(4), 5, DeviceCommand.Off);

        Console.WriteLine("Test Completed");

        string command = "";
        while (command != "q")
        {
            command = Console.ReadLine() ?? "";
            if (command == "s")
            {
                var schedule = controller.GetCurrentScheduleItems();
                foreach (var item in schedule)
                {
                    Console.WriteLine($"{item.Info.EventTime:G} - {item.Info.TimeType} ({item.Info.RelativeOffset}), Device: {item.Device}, Command: {item.Command}");
                }
            }
            if (command == "r")
            {
                controller.ReloadSchedule();
            }
        }
    }

    private static HouseController InitializeHouseController()
    {
        IKernel container = new StandardKernel();

        container.Bind<LatLongLocation>()
            //45.6382281,-122.7013602 = Vancouver, WA, USA
            .ToConstant(new LatLongLocation(45.6382, -122.7013));

        container.Bind<ISunsetProvider>()
            .To<CachingSunsetProvider>()
            .InSingletonScope()
            .WithConstructorArgument<ISunsetProvider>(
                container.Get<SolarTimesSunsetProvider>());

        var sunset = container.Get<ISunsetProvider>()
            .GetSunset(DateTime.Today.AddDays(1));
        Console.WriteLine($"Sunset Tomorrow: {sunset:G}");

        container.Bind<ScheduleFileName>().ToConstant(
            new ScheduleFileName(AppDomain.CurrentDomain.BaseDirectory + "ScheduleData"));

        container.Bind<HouseController>().ToSelf()
            .WithPropertyValue("Commander", new FakeCommander());

        var controller = container.Get<HouseController>();

        return controller;
    }
}
