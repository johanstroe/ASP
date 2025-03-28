using Business.Model;
using Data.Repositories;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository)
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<StatusResult> GetStatusesAsync()
    {
        var result = await _statusRepository.GetAllAsync();
        return result.Succeeded
            ? new StatusResult { Succeeded = true, StatusCode = result.StatusCode, Result = result.Result } 
    }
}


