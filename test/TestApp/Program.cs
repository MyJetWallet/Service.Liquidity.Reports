using System;
using System.Threading.Tasks;
using ProtoBuf.Grpc.Client;
using Service.Liquidity.Reports.Client;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;

            Console.Write("Press enter to start");
            Console.ReadLine();


            var factory = new LiquidityReportsClientFactory("http://localhost:80");


            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
