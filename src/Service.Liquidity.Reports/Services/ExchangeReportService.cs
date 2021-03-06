using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.ServiceBus;
using Service.Liquidity.Reports.Database;
using Service.Liquidity.Reports.Domain.Models.Models;
using Service.Liquidity.Reports.Grpc;
using Service.Liquidity.Reports.Grpc.Models.Exchange;
using Service.Liquidity.Reports.Grpc.Models.Hedger;

namespace Service.Liquidity.Reports.Services
{
    
    public class ExchangeReportService : IExchangeReportService
    {
        private readonly ILogger<ExchangeReportService> _logger;
        private readonly DatabaseContextFactory _contextFactory;


        public ExchangeReportService(
            ILogger<ExchangeReportService> logger,
            DatabaseContextFactory contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        public async Task<GetWithdrawalsHistoryResponse> GetWithdrawalsHistoryAsync(
            GetWithdrawalsHistoryRequest request)
        {
            try
            {
                var fromDate = request.From;
                var toDate = request.To;
                var page = Math.Abs(request.Page);
                var pageSize = Math.Abs(request.PageSize);

                await using var ctx = _contextFactory.Create();
                var response = await ctx.GetExchangeWithdrawalsHistoryAsync(
                    fromDate, toDate, page, pageSize, request.ExchangeFilter, request.AssetFilter);
                
                var withdrawals = response.Item1 ??  new List<Withdrawal>();
                var totalWithdrawals = response.Item2;
                
                return new GetWithdrawalsHistoryResponse
                {
                    Items = withdrawals,
                    TotalItems = totalWithdrawals
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to GetWithdrawalsHistoryResponse {@request}", request);
                return new GetWithdrawalsHistoryResponse
                {
                    IsError = true,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}