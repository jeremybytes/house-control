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

            var currentTime = DateTime.Now;
            controller.ScheduleOneTimeItem(currentTime.AddMinutes(1), 1, DeviceCommands.On);
            controller.ScheduleOneTimeItem(currentTime.AddMinutes(2), 2, DeviceCommands.On);
            controller.ScheduleOneTimeItem(currentTime.AddMinutes(3), 1, DeviceCommands.Off);
            controller.ScheduleOneTimeItem(currentTime.AddMinutes(4), 2, DeviceCommands.Off);


            Console.WriteLine("Test Completed");

            string command = "";
            while (command != "q")
            {
                command = Console.ReadLine();
                if (command == "s")
                {
                    var schedule = controller.GetCurrentScheduleItems();
                    foreach (var item in schedule)
                    {
                        Console.WriteLine("{0} - Device: {1}, Command: {2}",
                            item.EventTime.ToString("G"), 
                            item.Device.ToString(), 
                            item.Command.ToString());
                    }
                }
            }
        }
    }
}
