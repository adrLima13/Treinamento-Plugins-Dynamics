using System;
using Microsoft.Xrm.Sdk;

namespace PluginsTreinamento
{
    public class MinhaPrimeiraAction : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            IOrganizationServiceFactory serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService serviceAdmin = serviceFactory.CreateOrganizationService(null);

            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            trace.Trace("Minha Primeira Action executada com sucesso!");

            Entity entityAluno = new Entity("dio_alunos");
            entityAluno["dio_name"] = "Adriano de Albuquerque Lima";
            entityAluno["dio_cpf"] = "111.222.333-44";
            entityAluno["ownerid"] = new EntityReference("systemuser", context.UserId);
            Guid guidAluno = serviceAdmin.Create(entityAluno);
            trace.Trace("Aluno criado: " + guidAluno);
        }
    }
}
