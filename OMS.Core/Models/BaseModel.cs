using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OMS.Core.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
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

        protected string FormatNumber(int? value)
        {
            return value?.ToString("N0");
        }
        protected string FormatNumber(decimal? value)
        {
            return value?.ToString("N3");
        }
        protected string FormatNumber(double? value)
        {
            return value?.ToString("N3");
        }
    }

}
