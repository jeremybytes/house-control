using System;
using System.Collections.Generic;
using System.IO;

namespace HouseControl.Library
{
    public class Schedule : List<ScheduleItem>
    {
        public Schedule(string filename)
        {
            LoadScheduleFromCSV(filename);
        }

        private void LoadScheduleFromCSV(string fileName)
        {
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
                        EventTime = ScheduleHelper.Today() +
                                    DateTime.Parse(fields[1]).TimeOfDay,
                        TimeType = (ScheduleTimeType)Enum.Parse(typeof(ScheduleTimeType), fields[2], true),
                        Device = Int32.Parse(fields[3]),
                        Command = (DeviceCommands)Enum.Parse(typeof(DeviceCommands), fields[4]),
                        Type = (ScheduleType)Enum.Parse(typeof(ScheduleType), fields[5], true),
                        IsEnabled = bool.Parse(fields[6]),
                    };
                    this.Add(scheduleItem);
                }
                RollSchedule();
            }
        }

        public void RollSchedule()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                var currentItem = this[i];
                if (currentItem.EventTime.IsInPast())
                {
                    switch (currentItem.Type)
                    {
                        case ScheduleType.Daily:
                            currentItem.EventTime =
                                ScheduleHelper.RollForwardToNextDay(currentItem.EventTime, currentItem.TimeType);
                            break;
                        case ScheduleType.Weekday:
                            currentItem.EventTime =
                                ScheduleHelper.RollForwardToNextWeekdayDay(currentItem.EventTime, currentItem.TimeType);
                            break;
                        case ScheduleType.Weekend:
                            currentItem.EventTime =
                                ScheduleHelper.RollForwardToNextWeekendDay(currentItem.EventTime, currentItem.TimeType);
                            break;
                        case ScheduleType.Once:
                            this.RemoveAt(i);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}
