using Konnect.Service.DatabaseManager;
using Konnect.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Konnect.Service
{
    public class ServiceBase(KonnektContext dbContext)
    {
        /// <summary>
        /// A handler which constructs a service response which handles the result of a desired type within a given Task, as well as any errors.
        /// </summary>
        /// <typeparam name="T">The type of the result of the task.</typeparam>
        /// <param name="method">The task to be handled.</param>
        /// <returns>The a service response containing the result and details of the given Task.</returns>
        internal async Task<ServiceResponse<T>> ExecAsync<T>(Func<KonnektContext, ServiceResponse<T>, Task<T?>> method)
            where T : class
        {
            ServiceResponse<T> serviceResponse = new();

            if (dbContext is null)
            {
                serviceResponse.Errors.Add("Database could not initialize");
            }
            else
            {
                try
                {
                    if (method.DynamicInvoke(dbContext, serviceResponse) is not Task<T?> invoke)
                    {
                        serviceResponse.Success = false;
                        return serviceResponse;
                    }

                    serviceResponse.Result = await invoke;
                }
                catch (Exception ex)
                {
                    serviceResponse.Errors.Add($"{ex.Message} - {ex.InnerException?.Message}" ?? "No Message Specified.");
                }
            }

            if (serviceResponse.Errors.Count == 0)
                serviceResponse.Success = true;

            return serviceResponse;
        }

        internal async Task<ServiceResponse> ExecAsync(Func<KonnektContext, ServiceResponse, Task> method)
        {
            return await ExecAsync<object>(async (db, resp) =>
            {
                await Task.Delay(1);
                return null;
            });
        }
    }
}