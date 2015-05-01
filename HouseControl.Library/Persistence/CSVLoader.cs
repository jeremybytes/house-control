using System;
using System.Collections.Generic;
using System.IO;

namespace HouseControl.Library
{
    public class CSVLoader : IScheduleLoader
    {
        public IEnumerable<ScheduleItem> LoadScheduleItems(string filename)
        {
            var schedule = new List<ScheduleItem>();
            if (File.Exists(filename))
            {
                using (var reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
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
