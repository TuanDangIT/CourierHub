using CourierHub.Core.Result;
using CourierHub.Core.Validation;
using CourierHub.InPost.Client.Models.Requests;
using CourierHub.InPost.Client.Validators.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Validators;

internal class CreateParcelRequestValidator : AbstractValidator<CreateParcelRequest>
{
    public CreateParcelRequestValidator()
    {
        RuleFor("Receiver", x => x.Receiver)
            .Required()
            .SetValidator(new PeerValidator());

        RuleFor("Sender", x => x.Sender)
            .Required()
            .SetValidator(new PeerValidator());

        RuleFor("Insurance", x => x.Insurance!)
            .SetValidator(new InsuranceValidator());

        RuleFor("CashOnDelivery", x => x.Cod)
            .SetValidator(new CashOnDeliveryValidator()!);
    }
}
