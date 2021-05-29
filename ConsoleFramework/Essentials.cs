﻿using System;
using System.Collections.Generic;
using System.Text;
using ConsoleFramework.Data;
using ConsoleFramework.Utils;

namespace ConsoleFramework.Essentials
{
    class Instance : ICacheImplementation<Cell>
    {
        protected Viewport viewport;
        protected List<Cell> garbageCollector = new List<Cell>();
        protected List<Cell> cache = new List<Cell>();

        public Instance(Viewport Viewport)
        {
            viewport = Viewport;
            viewport.AddInstance(this);
        }

        public int CacheLength => cache.Count;
        public void AddRangeToCache(IEnumerable<Cell> range)
        {
            foreach (Cell cell in range)
                AddToCache(cell);
        }
        public void AddToCache(Cell cell)
        {
            if (cache.Exists(other => other.PositionEquals(cell)))
                RemoveFromCache(cache.Find(other => other.PositionEquals(cell)));
            cache.Add(cell);
        }
        public void ClearCache() => cache.Clear();
        public void RemoveFromCache(Cell cell) => cache.Remove(cell);

        internal void AddToViewport()
        {
            viewport.AddRangeToCache(cache);
            viewport.garbageCollector.AddRange(garbageCollector);
            cache.RemoveAll(cell => garbageCollector.Contains(cell));
            garbageCollector.Clear();
        }
    }

    class Viewport : IStackImplementation<Cell>, ICacheImplementation<Cell>, ICacheStackAdapter
    {
        internal List<Cell> garbageCollector = new List<Cell>();
        List<Instance> instances = new List<Instance>();
        List<Cell> cache = new List<Cell>();
        Stack<Cell> stack = new Stack<Cell>();
        public List<List<Selectable>> SelectionOrder = new List<List<Selectable>>();
        internal bool Active = false;
        public Selectable ActiveSelectable
        {
            get
            {
                return SelectionOrder.Find(list => list.Exists(selectable => selectable.Active)).Find(s => s.Active);
            }
        }

        public Viewport() => InputHandler.Viewports.Add(this);
        public Viewport(bool Active)
        {
            this.Active = Active;
            InputHandler.Viewports.Add(this);
        }

        public IReadOnlyList<Instance> Instances => instances;
        public int StackLength => stack.Count;
        public int CacheLength => cache.Count;
        public void AddRangeToCache(IEnumerable<Cell> range)
        {
            foreach (Cell cell in range)
                AddToCache(cell);
        }
        public void AddToCache(Cell cell)
        {
            if (cache.Exists(other => other.PositionEquals(cell)) || !cache.Contains(cell))
                RemoveFromCache(cache.Find(other => other.PositionEquals(cell)));
            cache.Add(cell);
        }
        public void CacheToStack()
        {
            PushRange(cache);
            cache.ForEach(cell => cell.MemoryLength++);
        }
        public void ClearCache() => cache.Clear();
        public void ClearStack() => stack.Clear();
        public Cell Peek() => stack.Peek();
        public void Push(Cell cell)
        {
            if (!stack.Contains(cell) && cell.MemoryLength < 1)
                stack.Push(cell);
        }
        public void PushRange(IEnumerable<Cell> range)
        {
            foreach (Cell cell in range)
                Push(cell);
        }
        public void RemoveFromCache(Cell cell) => cache.Remove(cell);
        public void StackToCache() => AddRangeToCache(stack);
        public void AddInstance(Instance instance) => instances.Add(instance);
        public void RemoveInstance(Instance instance) => instances.Remove(instance);

        public void Draw()
        {
            InitializeInstances();
            CacheToStack();
            Cell cell;
            while (stack.TryPop(out cell))
                cell.Draw();
        }

        public void Clean()
        {
            cache.ForEach(cell => Push(new Cell(cell.X, cell.Y)));
            Draw();
        }

        void InitializeInstances()
        {
            cache.RemoveAll(cell => garbageCollector.Contains(cell));
            garbageCollector.Clear();
            foreach (Instance instance in instances)
                instance.AddToViewport();
        }
    }
}
