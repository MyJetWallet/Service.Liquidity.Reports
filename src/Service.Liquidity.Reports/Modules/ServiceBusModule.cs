using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using Service.Liquidity.Hedger.Domain.Models;
using Service.Liquidity.Reports.Domain.Models.Models;
using Service.Liquidity.TradingPortfolio.Domain.Models;

namespace Service.Liquidity.Reports.Modules
{
    public class ServiceBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.SpotServiceBusHostPort), Program.LogFactory);
            
            builder.RegisterMyServiceBusSubscriberBatch<PortfolioTrade>(serviceBusClient, PortfolioTrade.TopicName, 
                $"LiquidityReports",
                TopicQueueType.PermanentWithSingleConnection);

            builder.RegisterMyServiceBusSubscriberBatch<PortfolioChangeBalance>(serviceBusClient, PortfolioChangeBalance.TopicName, 
                $"LiquidityReports",
                TopicQueueType.PermanentWithSingleConnection);
            
            builder.RegisterMyServiceBusSubscriberBatch<PortfolioSettlement>(serviceBusClient, PortfolioSettlement.TopicName, 
                $"LiquidityReports",
                TopicQueueType.PermanentWithSingleConnection);
            
            builder.RegisterMyServiceBusSubscriberBatch<PortfolioFeeShare>(serviceBusClient, PortfolioFeeShare.TopicName, 
                $"LiquidityReports",
                TopicQueueType.PermanentWithSingleConnection);
            
            builder.RegisterMyServiceBusSubscriberSingle<HedgeOperation>(serviceBusClient, HedgeOperation.TopicName, 
                $"LiquidityReports",
                TopicQueueType.PermanentWithSingleConnection);
            
            builder.RegisterMyServiceBusPublisher<Withdrawal>(serviceBusClient, Withdrawal.TopicName, true);

        }
    }
}