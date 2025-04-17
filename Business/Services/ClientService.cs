using Business.Interface;
using Business.Model;
using Data.Entities;
using Data.Repositories;
using Domain.Extentions;

namespace Business.Services;



public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ClientResult> GetClientsAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        return result.MapTo<ClientResult>();

    }

    public async Task<ClientResult> CreateClientAsync(AddClientForm form)
    {
        var entity = new ClientEntity
        {
            ClientName = form.ClientName,

        };

        var result = await _clientRepository.AddAsync(entity);

        return new ClientResult
        {
            Succeeded = result.Succeeded,
            StatusCode = result.StatusCode,
            Error = result.Error
        };
    }

}


