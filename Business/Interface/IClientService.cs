using Business.Model;

namespace Business.Interface
{
    public interface IClientService
    {
        Task<ClientResult> CreateClientAsync(AddClientForm form);
        Task<ClientResult> GetClientsAsync();
    }
}