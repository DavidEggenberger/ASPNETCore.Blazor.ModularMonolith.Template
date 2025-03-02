﻿using Microsoft.AspNetCore.Mvc;
using Shared.Features.Misc;
using System;

namespace Modules.TenantIdentity.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(IServiceProvider serviceProvider) : base(serviceProvider) 
        {
        }
    }
}
