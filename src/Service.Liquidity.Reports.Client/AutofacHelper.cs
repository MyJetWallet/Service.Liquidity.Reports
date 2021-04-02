using Autofac;
using Service.Liquidity.Reports.Grpc;

namespace Service.Liquidity.Reports.Client
{
    public static class AutofacHelper
    {
        public static void RegisterLiquidityReportClient(this ContainerBuilder builder, string liquidityReportGrpcServiceUrl)
        {
            var factory = new LiquidityReportsClientFactory(liquidityReportGrpcServiceUrl);

            builder.RegisterInstance(factory.GetLiquidityReportService()).As<ILiquidityReportService>().SingleInstance();
        }
    }
}