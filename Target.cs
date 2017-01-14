using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpLib.Display
{
    /// <summary>
    /// Targets define to which logical element our fields and layouts APIs apply to.
    /// </summary>
    public enum Target
    {
        /// <summary>
        /// Fields and layouts APIs apply to our client.
        /// </summary>
        Client=0,

        /// <summary>
        /// Fields and layout APIs apply to our display.
        /// </summary>
        Display=1
    };
}
