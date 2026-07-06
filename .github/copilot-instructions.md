# Copilot Instructions

## Project Guidelines
- User prefers a hybrid provider identification approach in CourierHub: keep enum-based built-in provider support and add extensible string-based provider identifiers for custom providers.
- Provider-specific properties that are not part of the domain abstraction model must come from `CreateParcelRequest.Metadata` using keys in the form `{CourierName}_{PropertyName}` or `{CourierName}_{PropertyName}.{SubPropertyName}` for nested metadata.

## Code Style
- When using default arguments in methods, use the 'default' keyword instead of 'null'. Example: 'T? defaultValue = default' instead of 'T? defaultValue = null'.
- Prefer naming static helper classes like `MetadataExtensions` and `AddressFormattingExtensions` as utilities rather than extensions, since they are static utility classes.

## Documentation
- Every property class should include XML summary documentation for its properties.
- Constructors and methods should include XML summary documentation.