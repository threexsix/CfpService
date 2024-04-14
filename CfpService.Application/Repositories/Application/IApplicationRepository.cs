using CfpService.Application.Entities;

namespace CfpService.Application.Repositories.Application;

public interface IApplicationRepository
{
    public ConferenceApplication? GetById(Guid id);
    public ConferenceApplication? Add(ConferenceApplication conferenceApplication);
    public ConferenceApplication? Put(ConferenceApplication conferenceApplication);
    public void Delete(Guid id);
    public void Submit(Guid id);
    public List<ConferenceApplication> GetSubmittedApplications(DateTime time);
    public List<ConferenceApplication> GetUnSubmittedApplications(DateTime time);
    public ConferenceApplication? GetUserUnSubmittedApplication(Guid userId);
    public bool ExistByApplicationId(Guid applicationId);
    public bool ExistUnsubmittedFromUser(Guid userId);
    public bool IsSubmitted(Guid applicationId);
}