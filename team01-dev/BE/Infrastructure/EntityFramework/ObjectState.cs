using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityFramework
{
    public enum ObjectState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}
