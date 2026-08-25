using CourierHub.Core.Validation;
using CourierHub.InPost.Client.Models.Common;
using CourierHub.InPost.Client.Models.Common.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierHub.InPost.Client.Validators.Shared;

internal class InsuranceValidator : AbstractValidator<InsuranceRequest>
{
    public InsuranceValidator()
    {
        RuleFor("Amount", x => x.Amount)
            .Required()
            .HasRange(0, 10000000);

        RuleFor("Currency", x => x.Currency)
            .LengthNullable(3, 3);
    }
}
