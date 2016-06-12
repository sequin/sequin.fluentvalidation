namespace Sequin.FluentValidation.Pipeline
{
    using System.Threading.Tasks;
    using global::FluentValidation;
    using global::FluentValidation.Results;
    using Sequin.Pipeline;

    public class ValidateCommand : CommandPipelineStage
    {
        private readonly IValidatorFactory validatorFactory;

        public ValidateCommand(IValidatorFactory validatorFactory)
        {
            this.validatorFactory = validatorFactory;
        }

        protected override async Task Process<TCommand>(TCommand command)
        {
            var validationResult = await Validate(command);

            if (!validationResult.IsValid)
            {
                throw new CommandValidationException(validationResult);
            }
        }

        private Task<ValidationResult> Validate<TCommand>(TCommand command)
        {
            var validator = validatorFactory.GetValidator<TCommand>();
            if (validator == null)
            {
                throw new ValidatorNotRegisteredException(typeof(TCommand));
            }

            return validator.ValidateAsync(command);
        }
    }
}
