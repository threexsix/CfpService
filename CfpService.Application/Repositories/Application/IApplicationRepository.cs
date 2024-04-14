using CfpService.Application.Entities;

namespace CfpService.Application.Repositories.Application;

public interface IApplicationRepository
{
    public Task<ConferenceApplication?> GetByIdAsync(Guid id);
    public Task<ConferenceApplication?> AddAsync(ConferenceApplication conferenceApplication);
    public Task<ConferenceApplication?> PutAsync(ConferenceApplication conferenceApplication);
    public Task DeleteAsync(Guid id);
    public Task SubmitAsync(Guid id);
    public Task<IEnumerable<ConferenceApplication>> GetSubmittedApplicationsAsync(DateTime time);
    public Task<IEnumerable<ConferenceApplication>> GetUnSubmittedApplicationsAsync(DateTime time);
    public  Task<ConferenceApplication?> GetUserUnSubmittedApplicationAsync(Guid userId);
    public  Task<bool> ExistByApplicationIdAsync(Guid applicationId);
    public  Task<bool> ExistUnsubmittedFromUserAsync(Guid userId);
    public  Task<bool> IsSubmittedAsync(Guid applicationId);
}