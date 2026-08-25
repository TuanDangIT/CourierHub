using CourierHub.Core.Result;
using CourierHub.Core.Validation;
using CourierHub.InPost.Client.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Validators.Shared;

internal class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor("Street", x => x.Street)
            .Required()
            .NotEmpty()
            .Length(1, 255);

        RuleFor("BuildingNumber", x => x.BuildingNumber)
            .NotEmpty()
            .Length(1, 255);

        RuleFor("City", x => x.City)
            .Required()
            .NotEmpty()
            .Length(1, 255);

        RuleFor("PostCode", x => x.PostCode)
            .Required()
            .NotEmpty()
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("must be in Polish postal code format (XX-XXX).");

        RuleFor("CountryCode", x => x.CountryCode!)
            .Length(2, 2);
    }
}
