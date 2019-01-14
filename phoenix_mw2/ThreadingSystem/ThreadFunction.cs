﻿using System;

namespace Phoenix.ThreadingSystem
{
    internal class ThreadFunction
    {
        public string Name { get; set; }
        public Action Function { get; set; }

        public ThreadFunction(string name, Action func)
        {
            Name = name;
            Function = func;
        }
    }
}
