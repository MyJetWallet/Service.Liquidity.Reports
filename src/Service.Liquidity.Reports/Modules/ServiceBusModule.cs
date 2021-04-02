using System.Collections.Generic;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Service.Liquidity.Engine.Client.ServiceBus;
using Service.Liquidity.Engine.Domain.Models.Portfolio;

namespace Service.Liquidity.Reports.Modules
{
    public class ServiceBusModule : Module
    {
        public static ILogger ServiceBusLogger { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            ServiceBusLogger = Program.LogFactory.CreateLogger(nameof(MyServiceBusTcpClient));

            var serviceBusClient = new MyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), ApplicationEnvironment.HostName);
            serviceBusClient.Log.AddLogException(ex => ServiceBusLogger.LogInformation(ex, "Exception in MyServiceBusTcpClient"));
            serviceBusClient.Log.AddLogInfo(info => ServiceBusLogger.LogDebug($"MyServiceBusTcpClient[info]: {info}"));
            serviceBusClient.SocketLogs.AddLogInfo((context, msg) => ServiceBusLogger.LogInformation($"MyServiceBusTcpClient[Socket {context?.Id}|{context?.ContextName}|{context?.Inited}][Info] {msg}"));
            serviceBusClient.SocketLogs.AddLogException((context, exception) => ServiceBusLogger.LogInformation(exception, $"MyServiceBusTcpClient[Socket {context?.Id}|{context?.ContextName}|{context?.Inited}][Exception] {exception.Message}"));

            builder.RegisterInstance(serviceBusClient).AsSelf().SingleInstance();

            var queryName = "Liquidity-Reports";

            builder
                .RegisterInstance(new PortfolioTradeSubscriber(serviceBusClient, queryName, TopicQueueType.Permanent))
                .As<ISubscriber<IReadOnlyList<PortfolioTrade>>>()
                .SingleInstance();

            builder
                .RegisterInstance(new PositionAssociationSubscriber(serviceBusClient, queryName, TopicQueueType.Permanent))
                .As<ISubscriber<IReadOnlyList<PositionAssociation>>>()
                .SingleInstance();

            builder
                .RegisterInstance(new PositionPortfolioSubscriber(serviceBusClient, queryName, TopicQueueType.Permanent))
                .As<ISubscriber<IReadOnlyList<PositionPortfolio>>>()
                .SingleInstance();
        }
    }
}