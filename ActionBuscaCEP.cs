using System;
using System.Text;
using System.Net;
using Microsoft.Xrm.Sdk;

namespace PluginsTreinamento
{
    public class ActionBuscaCEP : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context =
                    (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            IOrganizationServiceFactory serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService serviceAdmin = serviceFactory.CreateOrganizationService(null);

            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            var cep = context.InputParameters["CepInput"];
            trace.Trace("Cep informado: " + cep);

            var viaCEPUrl = $"https://viacep.com.br/ws/{cep}/json/";
            string result = string.Empty;
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                result = client.DownloadString(viaCEPUrl);
            }
            context.OutputParameters["ResultadoCEP"] = result;

            trace.Trace("Resultado: " + result);
        }
    }
}
