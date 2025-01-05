﻿using Shared.Kernel.BuildingBlocks.IntegrationEvents;

namespace Shared.Features.Messaging.IntegrationEvents
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> where TIntegrationEvent : IIntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent integrationEvent, CancellationToken cancellation);
    }
}
