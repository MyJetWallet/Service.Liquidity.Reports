using Autofac;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Reports.Modules
{
    public class ServiceBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), Program.LogFactory);

            const string queryName = "Liquidity-Reports";

            builder.RegisterMyServiceBusSubscriberBatch<AssetPortfolioTrade>(serviceBusClient, AssetPortfolioTrade.TopicName, 
                $"LiquidityReports",
                TopicQueueType.PermanentWithSingleConnection);

            builder.RegisterMyServiceBusSubscriberBatch<ChangeBalanceHistory>(serviceBusClient, ChangeBalanceHistory.TopicName, 
                $"LiquidityReports",
                TopicQueueType.PermanentWithSingleConnection);
            
            builder.RegisterMyServiceBusSubscriberBatch<ManualSettlement>(serviceBusClient, ManualSettlement.TopicName, 
                $"LiquidityReports",
                TopicQueueType.PermanentWithSingleConnection);
        }
    }
}