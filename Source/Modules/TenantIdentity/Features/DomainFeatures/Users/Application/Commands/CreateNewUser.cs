﻿using Microsoft.AspNetCore.Identity;
using Shared.Features.Messaging.Commands;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Users.Application.Commands
{
    public class CreateNewUser : Command
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
