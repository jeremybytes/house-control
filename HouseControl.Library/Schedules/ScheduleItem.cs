using System;

namespace HouseControl.Library
{
    public class ScheduleItem
    {
        public string ScheduleSet { get; set; }
        public DateTime EventTime { get; set; }
        public int Device { get; set; }
        public DeviceCommands Command { get; set; }
        public bool IsEnabled { get; set; }
        
        public ScheduleItem() {}

        public ScheduleItem(DateTime eventTime, int device, DeviceCommands command)
        {
            ScheduleSet = string.Empty;
            EventTime = eventTime;
            Device = device;
            Command = command;
            IsEnabled = true;
        }
    }
}
