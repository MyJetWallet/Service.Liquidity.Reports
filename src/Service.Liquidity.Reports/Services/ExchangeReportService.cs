using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Reports.Database;
using Service.Liquidity.Reports.Database.Entities;
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
            DatabaseContextFactory contextFactory
        )
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
                    fromDate, toDate, page, pageSize,  request.ExchangeFilter);
                
                response = response ?? new List<Withdrawal>();
                
                return new GetWithdrawalsHistoryResponse
                {
                    Items = response
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