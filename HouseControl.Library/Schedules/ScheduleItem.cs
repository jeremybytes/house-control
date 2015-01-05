using System;

namespace HouseControl.Library
{
    public class ScheduleItem
    {
        public string ScheduleSet { get; set; }
        public DateTime EventTime { get; set; }
        public int Device { get; set; }
        public DeviceCommands Command { get; set; }
        public ScheduleTypes Type { get; set; }
        public bool IsEnabled { get; set; }
        
        public ScheduleItem() {}
    }
}
