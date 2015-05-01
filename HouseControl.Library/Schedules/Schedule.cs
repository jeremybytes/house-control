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
            var loader = new CSVLoader();
            this.Clear();
            this.AddRange(loader.LoadScheduleItems(fileName));

            RollSchedule();
        }

        public void RollSchedule()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                var currentItem = this[i];
                while (currentItem.Info.EventTime.IsInPast())
                {
                    if (currentItem.Info.Type == ScheduleType.Once)
                    {
                        this.RemoveAt(i);
                        break;
                    }

                    switch (currentItem.Info.Type)
                    {
                        case ScheduleType.Daily:
                            currentItem.Info.EventTime =
                                ScheduleHelper.RollForwardToNextDay(currentItem.Info);
                            break;
                        case ScheduleType.Weekday:
                            currentItem.Info.EventTime =
                                ScheduleHelper.RollForwardToNextWeekdayDay(currentItem.Info);
                            break;
                        case ScheduleType.Weekend:
                            currentItem.Info.EventTime =
                                ScheduleHelper.RollForwardToNextWeekendDay(currentItem.Info);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}
