namespace Sequin.FluentValidation.Sample
{
    using Configuration;
    using global::Owin;
    using Owin.Middleware;
    using Pipeline;
    using Sequin.Owin;
    using Sequin.Owin.Extensions;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseSequin(SequinOptions.Configure()
                                       .WithOwinDefaults()
                                       .WithPipeline(x => new ValidateCommand(new LazyValidatorFactory())
                                                          {
                                                              Next = x.IssueCommand
                                                          })
                                       .Build(), new[]
                                                 {
                                                     new ResponseMiddleware(typeof(CommandValidationResponseMiddleware), new DefaultDictionaryFormatter())
                                                 });
        }
    }
}