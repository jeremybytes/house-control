using System;
using System.Collections.Generic;
using System.IO;

namespace HouseControl.Library
{
    public class Schedule : List<ScheduleItem>
    {
        private string filename;

        private IScheduleLoader loader;
        public IScheduleLoader Loader
        {
            get
            {
                if (loader == null)
                    loader = new JsonLoader();
                return loader;
            }
            set
            {
                if (loader != value)
                    loader = value;
            }
        }

        private IScheduleSaver saver;

        public IScheduleSaver Saver
        {
            get
            {
                if (saver == null)
                    saver = new JsonSaver();
                return saver;
            }
            set
            {
                if (saver != value)
                    saver = value;
            }
        }

        public Schedule(string filename)
        {
            this.filename = filename;
            LoadSchedule();
        }

        public void LoadSchedule()
        {
            this.Clear();
            this.AddRange(Loader.LoadScheduleItems(filename));
            RollSchedule();
        }

        public void SaveSchedule()
        {
            Saver.SaveScheduleItems(filename, this);
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
