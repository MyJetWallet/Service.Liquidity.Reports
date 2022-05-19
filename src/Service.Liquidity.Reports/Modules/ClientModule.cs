using Autofac;
using MyJetWallet.Domain.ExternalMarketApi;
using MyJetWallet.Sdk.NoSql;
using Service.AssetsDictionary.Client;
using Service.IndexPrices.Client;

namespace Service.Liquidity.Reports.Modules
{
    public class ClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var myNoSqlClient = builder.CreateNoSqlClient(
                Program.Settings.MyNoSqlReaderHostPort, Program.LogFactory);
            builder.RegisterAssetsDictionaryClients(myNoSqlClient);
            builder.RegisterIndexPricesClient(myNoSqlClient);
            
            builder.RegisterExternalMarketClient(Program.Settings.ExternalApiGrpcUrl);
        }
    }
}