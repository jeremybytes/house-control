using System;
using System.Collections.Generic;
using System.IO;

namespace HouseControl.Library
{
    public class CSVSaver
    {
        public void SaveScheduleItems(string fileName, IEnumerable<ScheduleItem> schedule)
        {
            using (var sr = new StreamWriter(fileName, false))
            {
                foreach (var item in schedule)
                {
                    sr.Write(item.ScheduleSet);
                    sr.Write(",");
                    var time = new DateTime(2000, 01, 01);
                    if (item.Info.TimeType == ScheduleTimeType.Standard)
                        time = time + item.Info.EventTime.TimeOfDay;
                    sr.Write(time.ToString("s"));
                    sr.Write(",");
                    sr.Write(item.Info.TimeType);
                    sr.Write(",");
                    sr.Write(item.Info.RelativeOffset);
                    sr.Write(",");
                    sr.Write(item.Info.Type);
                    sr.Write(",");
                    sr.Write(item.Device);
                    sr.Write(",");
                    sr.Write(item.Command);
                    sr.Write(",");
                    sr.WriteLine(item.IsEnabled);
                }
            }
        }
    }
}
