using HouseControl.Library;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Collections.Generic;

namespace HouseControl.Editor
{
    public class EditorViewModel : INotifyPropertyChanged
    {
        public IEnumerable<DeviceCommands> DeviceCommandsValues
        {
            get
            {
                return Enum.GetValues(typeof(DeviceCommands)).Cast<DeviceCommands>();
            }
        }

        public IEnumerable<ScheduleTimeType> ScheduleTimeTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(ScheduleTimeType)).Cast<ScheduleTimeType>();
            }
        }

        public IEnumerable<ScheduleType> ScheduleTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(ScheduleType)).Cast<ScheduleType>();
            }
        }

        private string filename = AppDomain.CurrentDomain.BaseDirectory + "ScheduleData.json";
        public string FileName
        {
            get { return filename; }
            set
            {
                if (filename == value) return;
                filename = value;
                LocalSchedule = new Schedule(filename);
                RaisePropertyChanged();
            }
        }

        private Schedule localSchedule;
        public Schedule LocalSchedule
        {
            get { return localSchedule; }
            set
            {
                if (localSchedule == value) return;
                localSchedule = value;
                RaisePropertyChanged();
            }
        }

        public EditorViewModel()
        {
            LocalSchedule = new Schedule(filename);
        }

        public void SaveSchedule()
        {
            LocalSchedule.SaveSchedule();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
