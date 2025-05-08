using Konnect.Service.DatabaseManager;
using Konnect.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace Konnect.Service.Services
{
    public class ServiceBase<ServiceType>
    {
        private IDbContextFactory<KonnektContext> _dbContextFactory;
        private readonly ILogger _logger;
        public ServiceBase(IDbContextFactory<KonnektContext> dbContextFactory, ILogger<ServiceType> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        /// <summary>
        /// A handler which constructs a service response which handles the result of a desired type within a given Task, as well as any errors.
        /// </summary>
        /// <typeparam name="DbType">The type of the result of the task.</typeparam>
        /// <param name="method">The task to be handled.</param>
        /// <returns>The a service response containing the result and details of the given Task.</returns>
        internal async Task<ServiceResponse<DbType>> ExecAsync<DbType>(Func<KonnektContext, ServiceResponse<DbType>, Task<DbType?>> method)
        {
            ServiceResponse<DbType> serviceResponse = new();
            DbContext dbContext = _dbContextFactory.CreateDbContext();

            try
            {
                if (method.DynamicInvoke(dbContext, serviceResponse) is not Task<DbType?> invoke)
                {
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                serviceResponse.Result = await invoke;
            }
            catch (Exception ex)
            {
                var error = FormatErrorMessage($"{ex.Message} - {ex.InnerException?.Message ?? "No Inner Exception Specified."}");
                serviceResponse.Errors.Add(error);
                _logger.LogError(error);
            }

            if (serviceResponse.Errors.Count == 0)
                serviceResponse.Success = true;

            await dbContext.DisposeAsync();
            return serviceResponse;
        }

        internal async Task<ServiceResponse> ExecAsync(Func<KonnektContext, ServiceResponse, Task> method)
        {
            var exec = await ExecAsync<object>(async (db, resp) =>
            {
                await method.Invoke(db, resp);
                return null;
            });

            return exec as ServiceResponse;
        }

        private string FormatErrorMessage(string message)
            => $"[EXCEPTION] - Code: {Guid.NewGuid()} - Message: {message}";
    }
}