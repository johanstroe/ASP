using Data.Entities;
using Data.Repositories;
using Domain.Models;

namespace Data.Interface;

public interface IStatusRepository : IBaseRepository<StatusEntity, Status>
{

}

