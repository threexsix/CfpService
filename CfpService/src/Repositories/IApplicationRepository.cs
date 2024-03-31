using CfpService.Dtos;
using CfpService.Models;

namespace CfpService.Repositories;

public interface IApplicationRepository
{
    public GetApplicationDto GetById(Guid id);
    public GetApplicationDto Add(PostApplicationDto dto);
    public GetApplicationDto Put(Guid id, PutApplicationDto dto);
    public void Delete(Guid id);
    public void Submit(Guid id);
    public IEnumerable<GetApplicationDto> GetSubmittedApplications(DateTime time);
    public IEnumerable<GetApplicationDto> GetUnSubmittedApplications(DateTime time);
    public GetApplicationDto GetUserUnSubmittedApplication(Guid userId);
}