﻿using Modules.TenantIdentity.Public.DTOs.Tenant;
using Shared.Features.Messaging.Queries;
using Shared.Features.Misc.ExecutionContext;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Queries
{
    public class GetTenant : Query<TenantDTO>
    {
        public Guid TenantId { get; set; }
    }

    public class GetTenantByIdQueryHandler : ServerExecutionBase<TenantIdentityModule>, IQueryHandler<GetTenant, TenantDTO>
    {
        public GetTenantByIdQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<TenantDTO> HandleAsync(GetTenant query, CancellationToken cancellation)
        {
            var tenant = await module.TenantIdentityDbContext.GetTenantByIdAsync(query.TenantId);
            
            return tenant.ToDTO();
        }
    }
}
