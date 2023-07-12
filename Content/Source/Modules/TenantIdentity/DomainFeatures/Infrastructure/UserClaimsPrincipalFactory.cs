﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Shared.Infrastructure.CQRS.Query;
using Shared.Kernel.BuildingBlocks.Authorization.Constants;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Application.Queries;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure
{
    public class ContextUserClaimsPrincipalFactory<TUser> : IUserClaimsPrincipalFactory<TUser> where TUser : User
    {
        private readonly IQueryDispatcher queryDispatcher;
        public ContextUserClaimsPrincipalFactory(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }
        public async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var _user = await queryDispatcher.DispatchAsync<GetUserById, User>(new GetUserById { UserId = user.Id });

            var claimsForUserQuery = new GetClaimsForUser { User = _user };
            var claimsForUser = await queryDispatcher.DispatchAsync<GetClaimsForUser, IEnumerable<Claim>>(claimsForUserQuery);
            
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimsForUser, IdentityConstants.ApplicationScheme, nameType: ClaimConstants.UserNameClaimType, ClaimConstants.UserRoleInTenantClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
