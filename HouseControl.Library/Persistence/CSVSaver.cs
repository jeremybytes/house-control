using System;
using System.Collections.Generic;
using System.IO;

namespace HouseControl.Library
{
    public class CSVSaver : IScheduleSaver
    {
        public void SaveScheduleItems(string filename, IEnumerable<ScheduleItem> schedule)
        {
            using (var writer = new StreamWriter(filename, false))
            {
                foreach (var item in schedule)
                {
                    writer.Write(item.ScheduleSet);
                    writer.Write(",");
                    var time = new DateTime(2000, 01, 01);
                    if (item.Info.TimeType == ScheduleTimeType.Standard)
                        time = time + item.Info.EventTime.TimeOfDay;
                    writer.Write(time.ToString("s"));
                    writer.Write(",");
                    writer.Write(item.Info.TimeType);
                    writer.Write(",");
                    writer.Write(item.Info.RelativeOffset);
                    writer.Write(",");
                    writer.Write(item.Info.Type);
                    writer.Write(",");
                    writer.Write(item.Device);
                    writer.Write(",");
                    writer.Write(item.Command);
                    writer.Write(",");
                    writer.WriteLine(item.IsEnabled);
                }
            }
        }
    }
}
