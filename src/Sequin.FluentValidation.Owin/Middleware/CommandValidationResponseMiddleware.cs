namespace Sequin.FluentValidation.Owin.Middleware
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Owin;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class CommandValidationResponseMiddleware : OwinMiddleware
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly IValidationResultFormatter validationResultFormatter;

        public CommandValidationResponseMiddleware(OwinMiddleware next, IValidationResultFormatter validationResultFormatter) : base(next)
        {
            this.validationResultFormatter = validationResultFormatter;
        }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (CommandValidationException ex)
            {
                var errors = validationResultFormatter.Format(ex.ValidationResult);

                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                context.Response.ReasonPhrase = ex.Message;
                context.Response.ContentType = "application/json";
                context.Response.Write(JsonConvert.SerializeObject(errors, SerializerSettings));
            }
            catch (ValidatorNotRegisteredException ex)
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Response.ReasonPhrase = ex.Message;
            }
        }
    }
}