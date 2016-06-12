namespace Sequin.FluentValidation.Integration.Fakes
{
    using System.Threading.Tasks;

    public class ValidatedCommandHandler : IHandler<ValidatedCommand>
    {
        public Task Handle(ValidatedCommand command)
        {
            return Task.FromResult(0);
        }
    }
}