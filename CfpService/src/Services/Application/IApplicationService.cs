using CfpService.Dtos.Application;

namespace CfpService.Services.Application;

public interface IApplicationService
{
    public IEnumerable<GetApplicationDto> GetApplications(DateTime? submittedAfter, DateTime? unsubmittedOlder);
    public GetApplicationDto AddApplication(PostApplicationDto dto);
    public GetApplicationDto GetApplicationById(Guid id);
    public GetApplicationDto EditApplication(Guid id, PutApplicationDto dto);
    public void DeleteApplication(Guid id);
    public void SubmitApplication(Guid id);

    public IEnumerable<GetApplicationDto> GetSubmittedApplications(DateTime time);
    public IEnumerable<GetApplicationDto> GetUnSubmittedApplications(DateTime time);
    public GetApplicationDto GetUserUnSubmittedApplication(Guid userId);
    public bool ExistByApplicationId(Guid applicationId);
    public bool ExistUnsubmittedFromUser(Guid userId);
    public bool IsSubmitted(Guid applicationId);
    public bool IsApplicationValidToSubmit(Guid applicationId);
}