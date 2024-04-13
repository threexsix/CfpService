using CfpService.Application.Mappers.ApplicationMapper;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Dtos.Application;

namespace CfpService.Application.Services.Application;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationMapper _mapper;

    public ApplicationService(IApplicationRepository applicationRepository, IApplicationMapper mapper)
    {
        _applicationRepository = applicationRepository;
        _mapper = mapper;
    }

    public  IEnumerable<GetApplicationDto> GetApplications(DateTime? submittedAfter, DateTime? unsubmittedOlder)
    {
        if (submittedAfter.HasValue == unsubmittedOlder.HasValue)
        {
            throw new ArgumentException("specify exactly one of the following query parameters: 'submittedAfter' or 'unsubmittedOlder'");
        }

        return submittedAfter.HasValue ? GetSubmittedApplications(submittedAfter.Value) : GetUnSubmittedApplications(unsubmittedOlder.Value);
    }
    public GetApplicationDto AddApplication(PostApplicationDto dto)
    {
        if (ExistUnsubmittedFromUser(dto.Author))
            throw new ArgumentException("cannot add, user has not-submitted application");

        var application = _mapper.ToEntity(dto);
        var addedApplication = _applicationRepository.Add(application);
        
        return _mapper.ToDto(addedApplication);
    }

    public GetApplicationDto GetApplicationById(Guid id)
    {
        if (!ExistByApplicationId(id))
            throw new KeyNotFoundException($"application with id {id} not found");
        
        var application = _applicationRepository.GetById(id);
        
        return _mapper.ToDto(application);
    }

    public GetApplicationDto EditApplication(PutApplicationDto dto)
    {
        if (!ExistByApplicationId(dto.Id))
            throw new KeyNotFoundException($"application with id {dto.Id} not found");
            
        if (IsSubmitted(dto.Id))
            throw new ArgumentException("cannot edit submitted application");

        var application = _applicationRepository.GetById(dto.Id);
        
        var alteredApplication = _applicationRepository.Put(_mapper.ToEntity(dto, application));
        
        return _mapper.ToDto(alteredApplication);
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
        var applications = _applicationRepository.GetSubmittedApplications(time).Select(x => _mapper.ToDto(x));
        return applications;
    }
    
    public IEnumerable<GetApplicationDto> GetUnSubmittedApplications(DateTime time)
    {
        var applications = _applicationRepository.GetUnSubmittedApplications(time).Select(x => _mapper.ToDto(x));
        return applications;
    }

    public GetApplicationDto GetUserUnSubmittedApplication(Guid userId)
    {
        var application = _applicationRepository.GetUserUnSubmittedApplication(userId);
        
        if (application == null) 
            throw new ArgumentException("user doesn't have any submitted applications");
        
        return _mapper.ToDto(application);
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