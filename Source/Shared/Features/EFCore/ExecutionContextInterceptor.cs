﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared.Features.Misc;
using Shared.Features.Misc.Errors;
using Shared.Features.Misc.ExecutionContext;

namespace Shared.Features.EFCore
{
    public class ExecutionContextInterceptor : IMaterializationInterceptor
    {
        public object InitializedInstance(
            MaterializationInterceptionData materializationData,
            object instance)
        {
            if (instance is Entity entity)
            {
                entity.ExecutionContext = materializationData
                    .Context
                    .GetService<IExecutionContext>();

                if (entity.TenantId != entity.ExecutionContext.TenantId)
                {
                    throw Error.UnAuthorized;
                }
            }

            return instance;
        }
    }
}
