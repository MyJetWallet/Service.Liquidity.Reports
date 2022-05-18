using System;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using JetBrains.Annotations;
using MyJetWallet.Sdk.GrpcMetrics;
using ProtoBuf.Grpc.Client;
using Service.Liquidity.Reports.Grpc;

namespace Service.Liquidity.Reports.Client
{
    [UsedImplicitly]
    public class LiquidityReportsClientFactory
    {
        private readonly CallInvoker _channel;

        public LiquidityReportsClientFactory(string assetsDictionaryGrpcServiceUrl)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress(assetsDictionaryGrpcServiceUrl);
            _channel = channel.Intercept(new PrometheusMetricsInterceptor());
        }

        public ILiquidityReportService GetLiquidityReportService() => 
            _channel.CreateGrpcService<ILiquidityReportService>();
        public IHedgeReportService GetHedgeReportService() => 
            _channel.CreateGrpcService<IHedgeReportService>();
        public IExchangeReportService GetExchangeReportService() =>
            _channel.CreateGrpcService<IExchangeReportService>();
    }
}
