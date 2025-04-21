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
        var client = new ClientEntity
        {
            Id = Guid.NewGuid().ToString(),
            ClientName = form.ClientName
        };

        if (form.ClientImage is { Length: > 0 })
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(form.ClientImage.FileName)}";
            var imagePath = Path.Combine("wwwroot", "Images", "Clients", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);

            using var stream = new FileStream(imagePath, FileMode.Create);
            await form.ClientImage.CopyToAsync(stream);

            client.ImageUrl = $"/Images/Clients/{fileName}";
        }

        var result = await _clientRepository.AddAsync(client);

        return new ClientResult
        {
            Succeeded = result.Succeeded,
            StatusCode = result.StatusCode,
            Error = result.Error
        };
    }




}


