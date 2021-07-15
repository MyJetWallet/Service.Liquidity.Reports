using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Service.Liquidity.Reports.Database;
using Service.Liquidity.Reports.Jobs;

namespace Service.Liquidity.Reports.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ReportAggregator>()
                .AsSelf()
                .AutoActivate()
                .SingleInstance();
            
            builder
                .RegisterType<PortfolioTradeHandlerJob>()
                .As<IStartable>()
                .AutoActivate()
                .SingleInstance();

            builder.RegisterType<DatabaseContextFactory>().AsSelf().SingleInstance();
        }
    }
}