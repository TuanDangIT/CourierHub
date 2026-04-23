using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Enums
{
    /// <summary>
    /// Label format enumeration representing the different formats in which shipping labels can be generated and returned by courier providers. This enum is used to specify the desired label format when requesting a shipping label for a parcel. The supported formats include:
    /// <list>
    /// <item><description>Pdf - Portable Document Format</description></item>
    /// <item><description>Zpl - Zebra Programming Language</description></item>
    /// <item><description>Epl - Eltron Programming Language</description></item>
    /// </list>
    /// </summary>
    public enum LabelFormat
    {
        /// <summary>
        /// Pdf - Portable Document Format
        /// </summary>
        Pdf,

        /// <summary>
        /// Zpl - Zebra Programming Language
        /// </summary>
        Zpl,

        /// <summary>
        /// Epl - Eltron Programming Language
        /// </summary>
        Epl
    }
}
