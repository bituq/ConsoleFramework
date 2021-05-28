using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFramework.Utils
{
    interface IStackImplementation<T>
    {
        void Push(T obj);
        void PushRange(IEnumerable<T> range);
        T Peek();
        void ClearStack();
        int StackLength { get; }
    }

    interface ICacheImplementation<T>
    {
        void AddToCache(T obj);
        void AddRangeToCache(IEnumerable<T> range);
        void RemoveFromCache(T obj);
        void ClearCache();
        int CacheLength { get; }
    }

    interface ICacheStackAdapter
    {
        void CacheToStack();
        void StackToCache();
    }
}
