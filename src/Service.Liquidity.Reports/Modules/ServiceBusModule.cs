﻿using Autofac;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using Service.Liquidity.Portfolio.Domain.Models;
using Service.Liquidity.TradingPortfolio.Domain.Models;

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

            builder.RegisterMyServiceBusSubscriberBatch<PortfolioChangeBalance>(serviceBusClient, PortfolioChangeBalance.TopicName, 
                $"LiquidityReports",
                TopicQueueType.PermanentWithSingleConnection);
            
            builder.RegisterMyServiceBusSubscriberBatch<PortfolioSettlement>(serviceBusClient, PortfolioSettlement.TopicName, 
                $"LiquidityReports",
                TopicQueueType.PermanentWithSingleConnection);
            
            builder.RegisterMyServiceBusSubscriberBatch<PortfolioFeeShare>(serviceBusClient, PortfolioFeeShare.TopicName, 
                $"LiquidityReports",
                TopicQueueType.PermanentWithSingleConnection);
        }
    }
}