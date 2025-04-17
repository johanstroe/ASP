using Data.Entities;
using Data.Repositories;
using Domain.Models;

public interface IClientRepository : IBaseRepository<ClientEntity, Client>
{

}