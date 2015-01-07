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
                        EventTime = DateTime.Today + 
                                    DateTime.Parse(fields[1]).TimeOfDay,
                        Device = Int32.Parse(fields[2]),
                        Command = (DeviceCommands)Enum.Parse(typeof(DeviceCommands), fields[3]),
                        // TODO: Parse schedule type from file at some point
                        Type = ScheduleTypes.Daily,
                        IsEnabled = bool.Parse(fields[4]),
                    };
                    this.Add(scheduleItem);
                }
                RollSchedule();
            }
        }

        public void RollSchedule()
        {
            for (int i = Count-1; i >= 0; i--)
            {
                var currentItem = this[i];
                if (currentItem.EventTime < DateTime.Now)
                {
                    switch (currentItem.Type)
                    {
                        case ScheduleTypes.Daily:
                            currentItem.EventTime = 
                                ScheduleHelper.RollForwardToNextDay(currentItem.EventTime);
                            break;
                        case ScheduleTypes.Weekday:
                            currentItem.EventTime = 
                                ScheduleHelper.RollForwardToNextWeekdayDay(currentItem.EventTime);
                            break;
                        case ScheduleTypes.Weekend:
                            currentItem.EventTime = 
                                ScheduleHelper.RollForwardToNextWeekendDay(currentItem.EventTime);
                            break;
                        case ScheduleTypes.Once:
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
