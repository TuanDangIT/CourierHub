using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.Abstractions.Interfaces;

/// <summary>
/// Courier request extension interface. Implement this interface to create extensions that can be added to courier requests for additional specific to that courier functionality or data. 
/// </summary>
public interface ICourierRequestExtension
{
}

/// <summary>
/// Courier response extension interface. Implement this interface to create extensions that can be added to courier responses for additional specific to that courier functionality or data.
/// </summary>
public interface ICourierResponseExtension
{
}
