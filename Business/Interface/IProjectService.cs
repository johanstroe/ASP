using Business.Model;
using Domain.Models;

namespace Business.Interface
{
    public interface IProjectService
    {
        Task<ProjectResult> CreateProjectAsync(AddProjectForm formData);
        Task<ProjectResult> DeleteProjectAsync(string id);
        Task<ProjectResult<Projects>> GetProjectAsync(string id);
        Task<ProjectResult<IEnumerable<Projects>>> GetProjectsAsync();
        Task<ProjectResult> UpdateProjectAsync(EditProjectForm formData);
    }
}