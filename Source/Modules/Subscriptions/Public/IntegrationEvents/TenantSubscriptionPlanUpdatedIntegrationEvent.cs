﻿using Shared.Kernel.BuildingBlocks.IntegrationEvents;
using Shared.Kernel.DomainKernel;

namespace Modules.Subscriptions.Public.IntegrationEvents
{
    public class TenantSubscriptionPlanUpdatedIntegrationEvent : IIntegrationEvent
    {
        public Guid TenantId { get; set; }
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
    }
}
