using System;
using System.Collections.Generic;

namespace CLK.AspNet.Identity
{
    public interface IPermission<out TKey>
    {
        // Properties
        TKey Id { get; }

        string Name { get; set; }
    }
}