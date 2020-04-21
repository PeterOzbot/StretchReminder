using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StretchReminder.Reminder.Timers
{
    public class TimerDuration : INotifyPropertyChanged
    {
        private bool _readonly;
        /// <summary>
        /// Flag indicating that the duration can be edited.
        /// </summary>
        public bool Readonly
        {
            get { return _readonly; }
            set
            {
                _readonly = value;
                RaisePropertyChanged();
            }
        }

        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;

                Hours = value.Hours;
                Minutes = value.Minutes;
                Seconds = value.Seconds;

                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Hours));
                RaisePropertyChanged(nameof(Minutes));
                RaisePropertyChanged(nameof(Seconds));
            }
        }

        public int Hours
        {
            get;
            set;
        }
        public int Minutes
        {
            get;
            set;
        }
        public int Seconds
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
