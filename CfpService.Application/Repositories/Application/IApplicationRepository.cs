using CfpService.Application.Entities;

namespace CfpService.Application.Repositories.Application;

public interface IApplicationRepository
{
    public Task<ConferenceApplication?> GetById(Guid id);
    public Task<ConferenceApplication?> Add(ConferenceApplication conferenceApplication);
    public Task<ConferenceApplication?> Put(ConferenceApplication conferenceApplication);
    public Task Delete(Guid id);
    public Task Submit(Guid id);
    public Task<IEnumerable<ConferenceApplication>> GetSubmittedApplications(DateTime time);
    public Task<IEnumerable<ConferenceApplication>> GetUnSubmittedApplications(DateTime time);
    public  Task<ConferenceApplication?> GetUserUnSubmittedApplication(Guid userId);
    public  Task<bool> ExistByApplicationId(Guid applicationId);
    public  Task<bool> ExistUnsubmittedFromUser(Guid userId);
    public  Task<bool> IsSubmitted(Guid applicationId);
}