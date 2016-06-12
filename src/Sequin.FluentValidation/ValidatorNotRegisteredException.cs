namespace Sequin.FluentValidation
{
    using System;

    public class ValidatorNotRegisteredException : Exception
    {
        internal ValidatorNotRegisteredException(Type commandType) : base($"A validator for command '{commandType.Name}' has not been registered")
        {
            CommandType = commandType;
        }

        public Type CommandType { get; }
    }
}
