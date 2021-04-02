using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Grpc.Core.Logging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.Liquidity.Engine.Domain.Models.Portfolio;

namespace Service.Liquidity.Reports.Jobs
{
    public class ReportAggregator
    {
        private readonly ILogger<ReportAggregator> _logger;

        private readonly object _gate = new object();

        private readonly List<PositionPortfolio> _positions = new List<PositionPortfolio>();
        private List<PortfolioTrade> _trades = new List<PortfolioTrade>();

        public ReportAggregator(
            ILogger<ReportAggregator> logger,
            ISubscriber<IReadOnlyList<PortfolioTrade>> tradeSubscriber,
            ISubscriber<IReadOnlyList<PositionAssociation>> associationPositionSubscriber,
            ISubscriber<IReadOnlyList<PositionPortfolio>> closePositionSubscriber)
        {
            _logger = logger;
            tradeSubscriber.Subscribe(HandleTrades);
            associationPositionSubscriber.Subscribe(HandleAssociations);
            closePositionSubscriber.Subscribe(HandleClosePosition);
        }

        private async ValueTask HandleClosePosition(IReadOnlyList<PositionPortfolio> positions)
        {
            lock (_gate)
            {
                foreach (var position in positions)
                {
                    _logger.LogInformation("Close position: {jsoContext}", JsonConvert.SerializeObject(position));
                    _positions.Insert(0, position);
                }

                while (_positions.Count > 50)
                {
                    _positions.RemoveAt(positions.Count-1);
                }
            }
        }

        private async ValueTask HandleAssociations(IReadOnlyList<PositionAssociation> associations)
        {
            foreach (var association in associations)
            {
                _logger.LogInformation("Position trade association: {jsoContext}", JsonConvert.SerializeObject(association));
            }

        }

        private async ValueTask HandleTrades(IReadOnlyList<PortfolioTrade> trades)
        {
            lock (_gate)
            {
                foreach (var trade in trades)
                {
                    _logger.LogInformation("Trade: {jsoContext}", JsonConvert.SerializeObject(trade));
                    _trades.Insert(0, trade);
                }

                while (_trades.Count > 50)
                {
                    _trades.RemoveAt(_trades.Count - 1);
                }
            }

        }

        public List<PortfolioTrade> GetTrades()
        {
            lock (_gate)
            {
                return _trades.ToList();
            }
        }

        public List<PositionPortfolio> GetClosePositions()
        {
            lock (_gate)
            {
                return _positions.ToList();
            }
        }
    }
}