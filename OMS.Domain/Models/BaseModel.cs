using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OMS.Core.Models
{
    public abstract class BaseModel : IBaseModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public string FormatNumber(int? value)
        {
            return value?.ToString("N0");
        }
        public string FormatNumber(decimal? value)
        {
            return value?.ToString("N3");
        }
        public string FormatNumber(double? value)
        {
            return value?.ToString("N3");
        }
        public string FormatDate(DateTime date)
        {
            return date.ToString("dd-MMM-yyyy");
        }
        public string FormatTime(DateTime date)
        {
            return date.ToString("HH:mm:ss");
        }
        public string FormatTimeStamp(DateTime date)
        {
            return date.ToString("dd-MMM-yyyy HH:mm:ss");
        }
    }

}
