using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Manager
{
    public static class ContextManager
    {
        public static readonly DemoEntities demoContext;

        static ContextManager()
        {
            demoContext = new DemoEntities();
        }
    }
}