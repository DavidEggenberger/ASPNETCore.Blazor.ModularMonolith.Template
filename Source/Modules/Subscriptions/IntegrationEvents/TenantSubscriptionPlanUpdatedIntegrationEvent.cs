﻿using Shared.Kernel.BuildingBlocks.Auth;
using Shared.Kernel.BuildingBlocks.Events;

namespace Modules.Subscriptions.IntegrationEvents
{
    public class TenantSubscriptionPlanUpdatedIntegrationEvent : IIntegrationEvent
    {
        public Guid TenantId { get; set; }
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
    }
}
