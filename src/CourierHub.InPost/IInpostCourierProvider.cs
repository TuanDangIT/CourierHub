using CourierHub.InPost.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost;

/// <summary>
/// InPost courier provider interface.
/// </summary>
public interface IInpostCourierProvider
{
    /// <summary>
    /// Parcel service for managing parcels.
    /// </summary>
    IParcelService Parcels { get; }
}