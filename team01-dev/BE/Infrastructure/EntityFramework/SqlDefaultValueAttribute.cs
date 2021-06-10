using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityFramework
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlDefaultValueAttribute : Attribute
    {
        public string DefaultValue { get; set; }
    }
}
