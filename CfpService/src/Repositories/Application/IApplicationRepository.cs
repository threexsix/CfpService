using CfpService.Dtos.Application;

namespace CfpService.Repositories.Application;

public interface IApplicationRepository
{
    public GetApplicationDto GetById(Guid id);
    public GetApplicationDto Add(PostApplicationDto dto);
    public GetApplicationDto Put(PutApplicationDto dto);
    public void Delete(Guid id);
    public void Submit(Guid id);
    public IEnumerable<GetApplicationDto> GetSubmittedApplications(DateTime time);
    public IEnumerable<GetApplicationDto> GetUnSubmittedApplications(DateTime time);
    public GetApplicationDto GetUserUnSubmittedApplication(Guid userId);
    public bool ExistByApplicationId(Guid applicationId);
    public bool ExistUserDraft(Guid userId);
    public bool IsSubmitted(Guid applicationId);
}