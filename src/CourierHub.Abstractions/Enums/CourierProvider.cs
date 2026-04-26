using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Enums
{
    /// <summary>
    /// Courier provider enumeration representing the different built-in courier providers supported by the system. 
    /// </summary>
    public enum CourierProvider
    {
        /// <summary>
        /// InPost courier provider.
        /// </summary>
        InPost,

        /// <summary>
        /// Dpd courier provider.
        /// </summary>
        Dpd,

        /// <summary>
        /// Furgonetka courier provider.
        /// </summary>
        Furgonetka,
    }
}
