using CfpService.Dtos.Application;
using CfpService.Repositories.Application;

namespace CfpService.Services.Application;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public  IEnumerable<GetApplicationDto> GetApplications(DateTime? submittedAfter, DateTime? unsubmittedOlder)
    {
        if (submittedAfter.HasValue == unsubmittedOlder.HasValue)
        {
            throw new ArgumentException("specify exactly one of the following query parameters: 'submittedAfter' or 'unsubmittedOlder'");
        }

        if (submittedAfter.HasValue)
        {
            return _applicationRepository.GetSubmittedApplications(submittedAfter.Value);
        }
        else
        {
            return _applicationRepository.GetUnSubmittedApplications(unsubmittedOlder.Value);
        }
    }
    public GetApplicationDto AddApplication(PostApplicationDto dto)
    {
        if (ExistUnsubmittedFromUser(dto.Author))
            throw new ArgumentException("cannot add, user has not-submitted application");
        
        return _applicationRepository.Add(dto);
    }


    public GetApplicationDto GetApplicationById(Guid id)
    {
        if (!ExistByApplicationId(id))
            throw new KeyNotFoundException($"application with id {id} not found");
        
        return _applicationRepository.GetById(id);
    }

    public GetApplicationDto EditApplication(Guid id, PutApplicationDto dto)
    {
        if (!ExistByApplicationId(id))
            throw new KeyNotFoundException($"application with id {id} not found");
            
        if (IsSubmitted(id))
            throw new ArgumentException("cannot edit submitted application");

        
        return _applicationRepository.Put(id, dto);
    }

    public void DeleteApplication(Guid id)
    {
        if (!ExistByApplicationId(id))
            throw new KeyNotFoundException($"application with id {id} not found");
        
        if (IsSubmitted(id))
            throw new ArgumentException("cannot delete submitted application");
        
        _applicationRepository.Delete(id);
    }

    public void SubmitApplication(Guid id)
    {
        if (!ExistByApplicationId(id))
            throw new KeyNotFoundException($"application with id {id} not found");
        
        if (!IsApplicationValidToSubmit(id))
            throw new ArgumentException("cannot submit, key fields are not filled in application");
        
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
        
        if (application == null) 
            throw new ArgumentException("user doesn't have any submitted applications");
        
        return application;
    }

    public bool ExistByApplicationId(Guid applicationId)
    {
        return _applicationRepository.ExistByApplicationId(applicationId);
    }

    public bool ExistUnsubmittedFromUser(Guid userId)
    {
        return _applicationRepository.ExistUserDraft(userId);
    }

    public bool IsSubmitted(Guid applicationId)
    {
        return _applicationRepository.IsSubmitted(applicationId);
    }

    public bool IsApplicationValidToSubmit(Guid applicationId)
    {
        var dto = _applicationRepository.GetById(applicationId);
        return (!string.IsNullOrWhiteSpace(dto.Name) && !string.IsNullOrWhiteSpace(dto.Activity) &&
                !string.IsNullOrWhiteSpace(dto.Outline));
    }
}