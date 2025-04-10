using Application.DTOs;
using Domain.Entities;
using Infrastructure.Configuration.Context;
using Infrastructure.Interface;
using Infrastructure.Messaging.Contract;
using MassTransit;
using MassTransit.JobService;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Common;
using Transversal.Common.Interfaces;

namespace Infrastructure.Messaging.Consumer
{
    public class LogCreatedConsumer : IConsumer<LogCreatedEvent>
    {
        private readonly ILogRepository _logRepository;
        private readonly IUnitOfWork<LogDbContext> _unitOfWork;
        private readonly ILogger<LogCreatedConsumer> _logger;
        public LogCreatedConsumer(ILogRepository logRepository, IUnitOfWork<LogDbContext> unitOfWork, ILogger<LogCreatedConsumer> logger)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<LogCreatedEvent> context)
        {
            var msg = context.Message;
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var log = new Log
                {
                    MachineName = msg.MachineName,
                    Logged = msg.Logged,
                    Level = msg.Level,
                    Message = msg.Message,
                    Logger = msg.Logger,
                    Properties = msg.Properties,
                    Callsite = msg.Callsite,
                    Exception = msg.Exception,
                    ApplicationId = msg.ApplicationId
                };

                await _logRepository.AddAsync(log);

                await _unitOfWork.SaveAsync();
                await transaction.CommitAsync();

                _logger.LogInformation(Messages.ProductCreated);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(string.Format(Messages.UnexpectedError, ex.Message));
            }
        }
    }
}
