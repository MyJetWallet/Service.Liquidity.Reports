using MyJetWallet.Sdk.Postgres;

namespace Service.Liquidity.Reports.Database.DesignTime
{
    public class ContextFactory : MyDesignTimeContextFactory<DatabaseContext>
    {
        public ContextFactory() : base(options => new DatabaseContext(options))
        {

        }
    }
}