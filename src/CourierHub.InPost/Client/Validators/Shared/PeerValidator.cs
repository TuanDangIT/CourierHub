using CourierHub.Core.Validation;
using CourierHub.InPost.Client.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Validators.Shared;

internal class PeerValidator : AbstractValidator<Peer>
{
    public PeerValidator()
    {
        RuleFor("Name", x => x.Name)
            .NotEmptyNullable()
            .LengthNullable(1, 255);

        RuleFor("FirstName", x => x.FirstName)
            .Required()
            .NotEmpty()
            .Length(1, 255);

        RuleFor("LastName", x => x.LastName)
            .Required()
            .NotEmpty()
            .Length(1, 255);

        RuleFor("Email", x => x.Email)
            .Required()
            .NotEmpty()
            .Length(1, 255);

        RuleFor("Phone", x => x.Phone)
            .Required()
            .NotEmpty()
            .Length(1, 255);

        RuleFor("Address", x => x.Address)
            .Required()
            .SetValidator(new AddressValidator());

        RuleFor("CompanyName", x => x.CompanyName)
            .NotEmptyNullable()
            .LengthNullable(1, 255);
    }
}