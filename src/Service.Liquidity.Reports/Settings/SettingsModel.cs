using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.Liquidity.Reports.Settings
{
    public class SettingsModel
    {
        [YamlProperty("LiquidityReports.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("LiquidityReports.SpotServiceBusHostPort")]
        public string SpotServiceBusHostPort { get; set; }

        [YamlProperty("LiquidityReports.ZipkinUrl")]
        public string ZipkinUrl { get; set; }
        
        [YamlProperty("LiquidityReports.PostgresConnectionString")]
        public string PostgresConnectionString { get; set; }

        [YamlProperty("LiquidityReports.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        [YamlProperty("LiquidityReports.ServiceBusQuerySuffix")]
        public string ServiceBusQuerySuffix { get; set; }
    }
}