using Business.Dtos;
using Business.Interface;
using Business.Model;
using Data.Entities;
using Data.Repositories;
using Domain.Dtos;
using Domain.Extentions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService, IClientService clientService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;
    private readonly IClientService _clientService = clientService;


    public async Task<ProjectResult> CreateProjectAsync(AddProjectForm formData)
    {
        formData.MemberId = "85f20754-0b98-4aaf-82f1-b6ddc2f00c16";
        formData.ClientId = "1";

        if (formData == null)
            return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields are supplied" };



        var projectEntity = formData.MapTo<ProjectEntity>();
        var statusResult = await _statusService.GetStatusByIdAsync(1);
        var status = statusResult.Result;

        projectEntity.StatusId = status!.Id;

        //  Hantera bilduppladdning
        if (formData.ProjectImage != null && formData.ProjectImage.Length > 0)
        {
            var fileName = Path.GetFileName(formData.ProjectImage.FileName);
            var filePath = Path.Combine("wwwroot/Images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formData.ProjectImage.CopyToAsync(stream);
            }

            projectEntity.Image = "/Images/" + fileName;
        }



        var result = await _projectRepository.AddAsync(projectEntity);

        return result.Succeeded
            ? new ProjectResult { Succeeded = true, StatusCode = 201 }
            : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };


    }


    public async Task<ProjectResult<IEnumerable<Projects>>> GetProjectsAsync()
    {
        var response = await _projectRepository.GetAllAsync(
            orderByDescending: true,
            sortBy: s => s.Created,
            where: null,
            p => p.Member,
            p => p.Status,
            p => p.Client
        );

        var clients = (await _clientService.GetClientsAsync()).Result?.ToList() ?? [];

        var projects = (response.Result ?? []).Select(p =>
        {
            if (string.IsNullOrWhiteSpace(p.ClientId) || p.Client != null)
                return p;

            p.Client = clients.FirstOrDefault(c => c.Id == p.ClientId)
                       ?? new Client { Id = p.ClientId, ClientName = "Okänd klient" };

            return p;
        });

        return new ProjectResult<IEnumerable<Projects>>
        {
            Succeeded = true,
            StatusCode = 201,
            Result = projects
        };
    }



    public async Task<ProjectResult<Projects>> GetProjectAsync(string id)
    {
        var response = await _projectRepository.GetAsync
            (
            where: x => x.Id == id,
            include => include.Member,
            include => include.Status,
            include => include.Client
            );
        return response.Succeeded
            ? new ProjectResult<Projects> { Succeeded = true, StatusCode = 200, Result = response.Result }
            : new ProjectResult<Projects> { Succeeded = false, StatusCode = 404, Error = $"Project '{id}' was not found" };

    }

    //Genererad UpdateProjectAsync av Chatgpt4o
    public async Task<ProjectResult> UpdateProjectAsync(EditProjectForm formData)
    {
        if (formData == null)
            return new ProjectResult
            {
                Succeeded = false,
                StatusCode = 400,
                Error = "Form data is null!"
            };

        // 1. Mappa formuläret direkt till en ProjectEntity
        var entity = formData.MapTo<ProjectEntity>();

        // 2. Hantera eventuell ny bild
        if (formData.ProjectImage != null && formData.ProjectImage.Length > 0)
        {
            var fileName = Path.GetFileName(formData.ProjectImage.FileName);
            var filePath = Path.Combine("wwwroot/Images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formData.ProjectImage.CopyToAsync(stream);
            }

            entity.Image = "/Images/" + fileName;
        }


        // 4. Uppdatera via repository
        var updateResult = await _projectRepository.UpdateAsync(entity);

        // 5. Returnera resultatet
        return updateResult.Succeeded
            ? new ProjectResult { Succeeded = true, StatusCode = 200 }
            : new ProjectResult { Succeeded = false, StatusCode = updateResult.StatusCode, Error = updateResult.Error };
    }

}

