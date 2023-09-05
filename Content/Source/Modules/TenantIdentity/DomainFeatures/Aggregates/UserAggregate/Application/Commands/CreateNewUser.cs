﻿using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.DomainFeatures.Aggregates.UserAggregate.Domain;
using Shared.Infrastructure.CQRS.Command;
using System.Threading;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.UserAggregate.Application.Commands
{
    public class CreateNewUser : ICommand
    {
        public ApplicationUser User { get; set; }
        public ExternalLoginInfo LoginInfo { get; set; }
    }
    public class CreateNewUserCommandHandler : ICommandHandler<CreateNewUser>
    {
        private readonly UserManager<ApplicationUser> userManager;
        public CreateNewUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task HandleAsync(CreateNewUser command, CancellationToken cancellationToken)
        {
            await userManager.CreateAsync(command.User);
        }
    }
}
