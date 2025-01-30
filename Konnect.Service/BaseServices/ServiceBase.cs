using Konnect.Service.DatabaseManager;

namespace Konnect.Service.BaseServices
{
    public class ServiceBase
    {
        internal async Task<ServiceResponse<T>> ExecAsync<T>(Func<KonnektContext, ServiceResponse<T>, Task<T?>> method)
        {
            KonnektContext context = new KonnektContext();
            ServiceResponse<T> serviceResponse = new ServiceResponse<T>();

            try
            {
                var invoke = method.DynamicInvoke(context, serviceResponse) as Task<T?>;

                if (invoke is null)
                {
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                serviceResponse.Result = await invoke;
            }
            catch (Exception ex)
            {
                serviceResponse.Errors.Add(($"{ex.Message} - {ex.InnerException?.Message}") ?? "No Message Specified.");
            }

            if (!serviceResponse.Errors.Any())
                serviceResponse.Success = true;

            return serviceResponse;
        }

        internal async Task<ServiceResponse> ExecAsync(Func<KonnektContext, ServiceResponse, Task> method)
        {
            KonnektContext context = new KonnektContext();
            ServiceResponse serviceResponse = new ServiceResponse();

            try
            {
                var invoke = method.DynamicInvoke(context, serviceResponse) as Task;

                if (invoke is null)
                {
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                await invoke;
            }
            catch (Exception ex)
            {
                serviceResponse.Errors.Add(($"{ex.Message} - {ex.InnerException?.Message}") ?? "No Message Specified.");
            }

            if (!serviceResponse.Errors.Any())
                serviceResponse.Success = true;

            return serviceResponse;
        }
    }
}

public class ServiceResponse<T>() : ServiceResponse
{
    public T? Result { get; set; }
}

public class ServiceResponse()
{
    public bool Success { get; set; } = false;
    public List<string> Errors { get; set; } = new List<string>();
}