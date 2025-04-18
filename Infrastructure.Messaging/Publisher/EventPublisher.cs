﻿using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messaging.Publisher
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<EventPublisher> _logger;

        public EventPublisher(IPublishEndpoint publishEndpoint, ILogger<EventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Publish<T>(T message) where T : class
        {
            try
            {
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                await _publishEndpoint.Publish(message, cts.Token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al publicar el mensaje de tipo {EventType}", typeof(T).Name);
            }
        }
    }
}
