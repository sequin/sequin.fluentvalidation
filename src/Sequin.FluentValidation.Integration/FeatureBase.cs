namespace Sequin.FluentValidation.Integration
{
    using System.Reflection;
    using Configuration;
    using Microsoft.Owin.Testing;
    using Owin.Middleware;
    using Pipeline;
    using Sequin.Owin;
    using Sequin.Owin.Extensions;
    using Xbehave;

    public abstract class FeatureBase
    {
        private CommandTrackingPostProcessor postProcessor;

        protected TestServer Server { get; private set; }

        [Background]
        public void Background()
        {
            postProcessor = new CommandTrackingPostProcessor();
            Server = TestServer.Create(app =>
                                       {
                                           app.UseSequin(SequinOptions.Configure()
                                                                      .WithOwinDefaults()
                                                                      .WithPostProcessPipeline(postProcessor)
                                                                      .WithPipeline(x => new ValidateCommand(new ReflectionValidatorFactory(Assembly.GetExecutingAssembly()))
                                                                                         {
                                                                                             Next = x.IssueCommand
                                                                                         })
                                                                      .Build(), new[]
                                                                                {
                                                                                    new ResponseMiddleware(typeof(CommandValidationResponseMiddleware), new DefaultDictionaryFormatter())
                                                                                });
                                       });
        }

        protected bool HasExecuted(string commandName)
        {
            return postProcessor.ExecutedCommands.ContainsKey(commandName);
        }
    }
}