using CfpService.Application.Entities;

namespace CfpService.Application.Repositories.Application;

public interface IApplicationRepository
{
    public ConferenceApplication GetById(Guid id);
    public ConferenceApplication Add(ConferenceApplication conferenceApplication);
    public ConferenceApplication Put(ConferenceApplication conferenceApplication);
    public void Delete(Guid id);
    public void Submit(Guid id);
    public IEnumerable<ConferenceApplication> GetSubmittedApplications(DateTime time);
    public IEnumerable<ConferenceApplication> GetUnSubmittedApplications(DateTime time);
    public ConferenceApplication GetUserUnSubmittedApplication(Guid userId);
    public bool ExistByApplicationId(Guid applicationId);
    public bool ExistUserDraft(Guid userId);
    public bool IsSubmitted(Guid applicationId);
}