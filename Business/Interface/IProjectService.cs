using Business.Dtos;
using Business.Model;
using Domain.Dtos;
using Domain.Models;

namespace Business.Interface
{
    public interface IProjectService
    {
        Task<ProjectResult> CreateProjectAsync(AddProjectForm formData);
        Task<ProjectResult<Projects>> GetProjectAsync(string id);
        Task<ProjectResult<IEnumerable<Projects>>> GetProjectsAsync();
    }
}