using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

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

    static class Calc
    {
        public static float Distance(int X1, int Y1, int X2, int Y2) => Vector2.Distance(new Vector2(X1, Y1), new Vector2(X2, Y2));
    }
}
