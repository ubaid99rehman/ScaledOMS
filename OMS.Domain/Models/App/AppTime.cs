﻿using OMS.Core.Models.App;
using System;

namespace OMS.Core.Models
{
    public class AppTime : BaseModel, IAppTime
    {
        private DateTime _currentTime;
        public DateTime CurrentTime
        {
            get { return _currentTime; }
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value;
                    OnPropertyChanged(nameof(CurrentTime));
                }
            }
        }

        //Cosntructor
        public AppTime()
        {
            CurrentTime = DateTime.Now;
        }
    }

}
