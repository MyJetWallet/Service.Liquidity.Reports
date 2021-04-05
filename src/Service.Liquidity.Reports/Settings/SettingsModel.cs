using SimpleTrading.SettingsReader;

namespace Service.Liquidity.Reports.Settings
{
    [YamlAttributesOnly]
    public class SettingsModel
    {
        [YamlProperty("LiquidityReports.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("LiquidityReports.SpotServiceBusHostPort")]
        public string SpotServiceBusHostPort { get; set; }

        [YamlProperty("LiquidityReports.ZipkinUrl")]
        public string ZipkinUrl { get; set; }
    }
}