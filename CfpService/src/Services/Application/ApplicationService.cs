using CfpService.Dtos;
using CfpService.Dtos.Application;
using CfpService.Models;
using CfpService.Repositories;
using CfpService.Repositories.Application;

namespace CfpService.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public GetApplicationDto AddApplication(PostApplicationDto dto)
    {
        return _applicationRepository.Add(dto);
    }


    public GetApplicationDto GetApplicationById(Guid id)
    {
        return _applicationRepository.GetById(id);
    }

    public GetApplicationDto EditApplication(Guid id, PutApplicationDto dto)
    {
        return _applicationRepository.Put(id, dto);
    }

    public void DeleteApplication(Guid id)
    {
        _applicationRepository.Delete(id);
    }

    public void SubmitApplication(Guid id)
    {
        _applicationRepository.Submit(id);
    }

    public IEnumerable<GetApplicationDto> GetSubmittedApplications(DateTime time)
    {
        var applications = _applicationRepository.GetSubmittedApplications(time);
        return applications;
    }
    
    public IEnumerable<GetApplicationDto> GetUnSubmittedApplications(DateTime time)
    {
        var applications = _applicationRepository.GetUnSubmittedApplications(time);
        return applications;
    }

    public GetApplicationDto GetUserUnSubmittedApplication(Guid userId)
    {
        var application = _applicationRepository.GetUserUnSubmittedApplication(userId);
        return application;
    }

    public bool AnyDraftUserApplications(Guid userId)
    {
        return _applicationRepository.Exist(userId);
    }
}