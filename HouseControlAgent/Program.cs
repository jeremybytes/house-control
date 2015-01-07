using HouseControl.Library;
using System;

namespace HouseControlAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Controller");

            var controller = new HouseController();

            Console.WriteLine("Controller Running ('q' to quit)");

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
