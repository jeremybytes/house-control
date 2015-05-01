using System;
using System.Collections.Generic;
using System.IO;

namespace HouseControl.Library
{
    public class CSVLoader
    {
        public IEnumerable<ScheduleItem> LoadScheduleItems(string fileName)
        {
            var schedule = new List<ScheduleItem>();
            if (File.Exists(fileName))
            {
                using (var sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var fields = line.Split(',');
                        var scheduleItem = new ScheduleItem()
                        {
                            ScheduleSet = fields[0],
                            Info = new ScheduleInfo()
                            {
                                EventTime = ScheduleHelper.Yesterday() +
                                            DateTime.Parse(fields[1]).TimeOfDay,
                                TimeType = (ScheduleTimeType)Enum.Parse(typeof(ScheduleTimeType), fields[2], true),
                                RelativeOffset = TimeSpan.Parse(fields[3]),
                                Type = (ScheduleType)Enum.Parse(typeof(ScheduleType), fields[4], true),
                            },
                            Device = Int32.Parse(fields[5]),
                            Command = (DeviceCommands)Enum.Parse(typeof(DeviceCommands), fields[6]),
                            IsEnabled = bool.Parse(fields[7]),
                        };
                        schedule.Add(scheduleItem);
                    }
                }
            }
            return schedule;
        }
    }
}
