﻿using System;

namespace OMS.Core.Services.Cache
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan? expiration = null);
        void Remove(string key);
        bool ContainsKey(string key);
    }

}
