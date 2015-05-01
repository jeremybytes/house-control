using System;
using System.Collections.Generic;
using System.IO;

namespace HouseControl.Library
{
    public class Schedule : List<ScheduleItem>
    {
        private string filename;

        public Schedule(string filename)
        {
            this.filename = filename;
            LoadSchedule();
        }

        public void LoadSchedule()
        {
            LoadScheduleFromJson();
        }

        public void SaveSchedule()
        {
            SaveSchedulToJson();
        }

        private void LoadScheduleFromCSV()
        {
            var loader = new CSVLoader();
            this.Clear();
            this.AddRange(loader.LoadScheduleItems(filename));

            RollSchedule();
        }

        private void LoadScheduleFromJson()
        {
            var loader = new JsonLoader();
            this.Clear();
            this.AddRange(loader.LoadScheduleItems(filename));

            RollSchedule();
        }

        private void SaveScheduleToCSV()
        {
            var saver = new CSVSaver();
            saver.SaveScheduleItems(filename, this);
        }

        private void SaveSchedulToJson()
        {
            var saver = new JsonSaver();
            saver.SaveScheduleItems(filename, this);
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
