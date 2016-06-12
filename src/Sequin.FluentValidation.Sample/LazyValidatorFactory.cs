namespace Sequin.FluentValidation.Sample
{
    using System;
    using global::FluentValidation;
    using Commands;

    public class LazyValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            // Lazy factory for sample
            return new DummyCommandValidator();
        }
    }
}