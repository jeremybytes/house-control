using System;
using System.Collections.Generic;
using System.IO;

namespace HouseControl.Library
{
    public class Schedule : List<ScheduleItem>
    {
        public Schedule()
        {
            LoadScheduleFromCSV();

            // These items are good for testing the system is working
            // TODO: Figure out a way to do this with configuration / parameterization
            this.Add(new ScheduleItem(DateTime.Now.AddMinutes(1), 1, DeviceCommands.On));
            this.Add(new ScheduleItem(DateTime.Now.AddMinutes(2), 2, DeviceCommands.On));
            this.Add(new ScheduleItem(DateTime.Now.AddMinutes(3), 1, DeviceCommands.Off));
            this.Add(new ScheduleItem(DateTime.Now.AddMinutes(4), 2, DeviceCommands.Off));
        }

        public void LoadScheduleFromCSV()
        {
            var fileName = AppDomain.CurrentDomain.BaseDirectory + "ScheduleData.txt";

            if (File.Exists(fileName))
            {
                var sr = new StreamReader(fileName);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var fields = line.Split(',');
                    var scheduleItem = new ScheduleItem()
                    {
                        ScheduleSet = fields[0],
                        EventTime = DateTime.Parse(fields[1]),
                        Device = Int32.Parse(fields[2]),
                        Command = (DeviceCommands)Enum.Parse(typeof(DeviceCommands), fields[3]),
                        IsEnabled = bool.Parse(fields[4]),
                    };
                    this.Add(scheduleItem);
                }
            }
        }
    }
}
