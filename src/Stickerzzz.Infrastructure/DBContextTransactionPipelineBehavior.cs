using MediatR;
using Stickerzzz.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stickerzzz.Infrastructure
{
    /// <summary>
    /// Adds transaction to the processing pipeline
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class DBContextTransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly AppDbContext _context;

        public DBContextTransactionPipelineBehavior(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse result = default(TResponse);

            try
            {
                _context.BeginTransaction();

                result = await next();

                _context.CommitTransaction();
            }
            catch (Exception ex)
            {
                _context.RollbackTransaction();
                throw;
            }

            return result;
        }
    }
}
