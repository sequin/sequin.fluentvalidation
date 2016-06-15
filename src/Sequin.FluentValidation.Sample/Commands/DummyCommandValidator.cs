﻿namespace Sequin.FluentValidation.Sample.Commands
{
    using global::FluentValidation;

    public class DummyCommandValidator : AbstractValidator<DummyCommand>
    {
        public DummyCommandValidator()
        {
            RuleFor(x => x.DummyProperty).GreaterThan(0);
            RuleFor(x => x.OtherDummyProperty).GreaterThan(0);
        }
    }
}