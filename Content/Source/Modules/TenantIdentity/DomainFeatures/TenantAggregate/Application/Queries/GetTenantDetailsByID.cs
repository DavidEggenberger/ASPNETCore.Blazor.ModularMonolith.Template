﻿using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.DomainFeatures.Application.Queries;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Shared.Infrastructure.CQRS.Command;
using Shared.Infrastructure.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Queries
{
    public class GetTenantDetailsByID : IQuery<TenantDetailDTO>
    {
        public Guid TenantId { get; set; }
    }
    public class GetTenantDetailsByIDQueryHandler : IQueryHandler<GetTenantDetailsByID, TenantDetailDTO>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public GetTenantDetailsByIDQueryHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task<TenantDetailDTO> HandleAsync(GetTenantDetailsByID query, CancellationToken cancellation)
        {
            var tenantDetail = await tenantIdentityDbContext.Tenants.Where(t => t.TenantId == query.TenantId).SingleAsync();
            return tenantDetail.ToDetailDTO();
        }
    }
}
