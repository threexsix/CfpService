using CfpService.Dtos;
using CfpService.Models;

namespace CfpService.Services;

public interface IApplicationService
{
    public GetApplicationDto AddApplication(PostApplicationDto dto);
    public GetApplicationDto GetApplicationById(Guid id);
    public GetApplicationDto EditApplication(Guid id, PutApplicationDto dto);
    public void DeleteApplication(Guid id);
    public void SubmitApplication(Guid id);

    public IEnumerable<GetApplicationDto> GetSubmittedApplications(DateTime time);
    public IEnumerable<GetApplicationDto> GetUnSubmittedApplications(DateTime time);
    public GetApplicationDto GetUserUnSubmittedApplication(Guid userId);
}