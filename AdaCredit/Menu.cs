using AdaCredit.UseCases;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdaCredit
{
    public class Menu
    {
        public static void Show()
        {
            //Sub-Menus:
            var subClient = new ConsoleMenu(Array.Empty<string>(), level: 1)
            .Add("Cadastrar Novo Cliente", () => AddNewClient.Execute())
            .Add("Consultar os Dados de um Cliente Existente", () => ConsultClientData.Execute())
            .Add("Alterar o Cadastro de um Cliente Existente", () => ChangeClientData.Execute())
            .Add("Desativar o Cadastro de um Cliente Existente", () => DeactivateClient.Execute())
            .Add("Voltar", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Clientes";
                config.EnableBreadcrumb = true;
                config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

            var subEmployee = new ConsoleMenu(Array.Empty<string>(), level: 1)
            .Add("Cadastrar Novo Funcionário", () => AddNewEmployee.Execute())
            .Add("Alterar a Senha de um Funcionário Existente", () => ChangeEmployeePassword.Execute())
            .Add("Desativar o Cadastro de um Funcionário Existente", () => DeactivateEmployee.Execute())
            .Add("Voltar", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Funcionários";
                config.EnableBreadcrumb = true;
                config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

            var subTransaction = new ConsoleMenu(Array.Empty<string>(), level: 1)
            .Add("Processar Transações (Reconciliação Bancária)", () => ProcessesTransactions.Execute())
            .Add("Voltar", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Transações";
                config.EnableBreadcrumb = true;
                config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

            var subReport = new ConsoleMenu(Array.Empty<string>(), level: 1)
            .Add("Exibir Todos os Clientes Ativos com seus Respectivos Saldos", () => GetReport.ActiveClients())
            .Add("Exibir Todos os Clientes Inativos", () => GetReport.InactiveClients())
            .Add("Exibir Todos os Funcionários Ativos e sua Última Data e Hora de Login", () => GetReport.ActiveEmployees())
            .Add("Exibir Transações com Erro (Detalhes da transação e do Erro)", () => GetReport.FailedTransactions())
            .Add("Voltar", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Relatórios";
                config.EnableBreadcrumb = true;
                config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

            //Menu Principal:
            var menu = new ConsoleMenu(Array.Empty<string>(), level: 0)
              .Add("Clientes", subClient.Show)
              .Add("Funcionários", subEmployee.Show)
              .Add("Transações", subTransaction.Show)
              .Add("Relatórios", subReport.Show)
              .Add("Encerrar Sistema", () => TerminateSystem())
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = true;
                  config.Title = "Ada Credit";
                  config.EnableWriteTitle = false;
                  config.EnableBreadcrumb = true;
              });

            menu.Show();            
        }
        public static void SomeAction(string v)
        {
            Console.WriteLine("Ação Executada.");
            Console.ReadKey();
        }
        private static void TerminateSystem()
        {
            Console.Clear();
            Console.WriteLine("Sistema Encerrado.");
            Environment.Exit(0);
        }
    }
}
