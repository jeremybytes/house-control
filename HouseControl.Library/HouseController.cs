using System;
using System.Linq;
using System.Timers;

namespace HouseControl.Library
{
    public class HouseController
    {
        private ICommander commander;
        public ICommander Commander
        {
            get
            {
                if (commander == null)
                    commander = new SerialCommander();
                return commander;
            }
            set
            {
                if (commander != value)
                    commander = value;
            }
        }

        private Timer scheduler = new Timer(60000);
        private Schedule schedule = new Schedule();

        public HouseController()
        {
            scheduler.Elapsed += scheduler_Elapsed;
            scheduler.AutoReset = true;
            scheduler.Start();
        }

        private void scheduler_Elapsed(object sender, ElapsedEventArgs e)
        {
            var itemsToProcess = schedule.Where(si =>
                si.IsEnabled &&
                TimeDurationFromNow(si.EventTime) < TimeSpan.FromSeconds(30));

            foreach (var item in itemsToProcess)
                SendCommand(item.Device, item.Command);

            #if DEBUG
            string logMessage = string.Format("Schedule Items Processed: {0} - "
                + "Total Items: {1} - Active Items: {2}",
                itemsToProcess.Count().ToString(),
                schedule.Count.ToString(),
                schedule.Count(si => si.IsEnabled));
            Console.WriteLine(logMessage);
            #endif
        }

        private TimeSpan TimeDurationFromNow(DateTime checkTime)
        {
            // Note: this code breaks down around 00:00 since
            // TimeOfDay is timespan since 00:00 (regardless of date)
            return (checkTime.TimeOfDay - DateTime.Now.TimeOfDay).Duration();
        }

        public void ResetAll()
        {
            for(int i = 1; i <=8; i++)
            {
                SendCommand(i, DeviceCommands.Off);
            }
        }

        public void SendCommand(int device, DeviceCommands command)
        {
            var message = MessageGenerator.GetMessage(device, command);
            Commander.SendCommand(message);
            Console.WriteLine("{0} - Device: {1}, Command: {2}",
                DateTime.Now.ToString("t"), device.ToString(), command.ToString());
        }

    }
}
