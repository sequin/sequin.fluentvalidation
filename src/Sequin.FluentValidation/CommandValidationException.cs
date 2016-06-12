namespace Sequin.FluentValidation
{
    using System;
    using global::FluentValidation.Results;

    public class CommandValidationException : Exception
    {
        public CommandValidationException(ValidationResult validationResult) : base("The command contained validation errors")
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; }
    }
}
