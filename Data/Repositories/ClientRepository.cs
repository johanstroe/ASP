﻿

using Data.Contexts;
using Data.Entities;
using Domain.Models;

namespace Data.Repositories;



public class ClientRepository(DataContext context) : BaseRepository<ClientEntity, Client>(context), IClientRepository
{

}

