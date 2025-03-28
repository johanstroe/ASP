

using Data.Contexts;
using Data.Entities;
using Domain.Models;

namespace Data.Repositories;

public interface IProjectRepository : IBaseRepository<ProjectEntity, Projects>
{

}
public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity, Projects>(context), IProjectRepository
{

}

