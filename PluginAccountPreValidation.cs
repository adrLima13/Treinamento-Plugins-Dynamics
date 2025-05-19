using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;


namespace PluginsTreinamento
{
    public class PluginAccountPreValidation : IPlugin
    {
        // método requerido para execuçao do Plugin recebendo como parametro os dados do provedor de serviço
        public void Execute(IServiceProvider serviceProvider)
        {
            // Variavel contendo o contexto da execuçao
            IPluginExecutionContext context =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Variável contendo o Service Factory da Organizaçao
            IOrganizationServiceFactory serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            // Variavel contendo o Service Admin que estabele os servicos de conexao com o Dataverse
            IOrganizationService serviceAdmin = serviceFactory.CreateOrganizationService(null);

            // Variavel do Trace que armazena informações de LOG
            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Variável do tipo Entity vazia
            Entity entidadeContexto = null;

            if (context.InputParameters.Contains("Target")) // Verifica se contém dados para o destino
            {
                entidadeContexto = (Entity)context.InputParameters["Target"]; // atribui o contexto da entidade para a variavel

                trace.Trace("Entidade do Contexto: " + entidadeContexto.Attributes.Count); // armazena informaçoes de LOG

                if (entidadeContexto == null) //verifica se a entidade do contexto está vazia
                {
                    return; // caso verdadeira retorna sem nada executar
                }

                if (!entidadeContexto.Contains("telephone1")) // verifica se o atributo telephonel não está presente no contexto
                {
                    throw new InvalidPluginExecutionException("Campo Telefone principal é obrigatório!"); // exibe Exception de Erro
                }
            }
        }
    }
}
