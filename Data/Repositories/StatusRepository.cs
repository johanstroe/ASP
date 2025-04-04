

using Data.Contexts;
using Data.Entities;
using Data.Interface;
using Domain.Models;

namespace Data.Repositories;
public class StatusRepository(DataContext context) : BaseRepository<StatusEntity, Status>(context), IStatusRepository
{

}

