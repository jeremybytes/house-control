using System;

namespace HouseControl.Library
{
    public class ScheduleItem
    {
        public string ScheduleSet { get; set; }
        public DateTime EventTime { get; set; }
        public ScheduleTimeType TimeType { get; set; }
        public int Device { get; set; }
        public DeviceCommands Command { get; set; }
        public ScheduleType Type { get; set; }
        public bool IsEnabled { get; set; }
        
        public ScheduleItem() {}
    }
}
