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

            Console.WriteLine("Controller Running (press 'Enter' to quit)");
            Console.ReadLine();
        }
    }
}
