using MediatR;
using Microsoft.Extensions.Logging;
using PersonRegistry.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Behaviours
{


    internal sealed class CustomCommandHandlerDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly ILogger<CustomCommandHandlerDecorator<TRequest, TResponse>> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CustomCommandHandlerDecorator(
            IRequestHandler<TRequest, TResponse> inner,
            ILogger<CustomCommandHandlerDecorator<TRequest, TResponse>> logger,
            IUnitOfWork unitOfWork)
        {
            _inner = inner;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        // Logs start/end, runs the handler, then persists via UnitOfWork.
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var commandName = typeof(TRequest).Name;

            try
            {
                _logger.LogInformation("Started processing {CommandName}", commandName);
                var result = await _inner.Handle(request, cancellationToken);
                _logger.LogInformation("Finished processing {CommandName}", commandName);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Finished saving data in database {CommandName}", commandName);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process {CommandName}", commandName);
                throw;
            }
        }
    }
}