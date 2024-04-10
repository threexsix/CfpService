namespace CfpService.Repositories.Application;

public interface IApplicationRepository
{
    public Entities.Application GetById(Guid id);
    public Entities.Application Add(Entities.Application application);
    public Entities.Application Put(Entities.Application application);
    public void Delete(Guid id);
    public void Submit(Guid id);
    public IEnumerable<Entities.Application> GetSubmittedApplications(DateTime time);
    public IEnumerable<Entities.Application> GetUnSubmittedApplications(DateTime time);
    public Entities.Application GetUserUnSubmittedApplication(Guid userId);
    public bool ExistByApplicationId(Guid applicationId);
    public bool ExistUserDraft(Guid userId);
    public bool IsSubmitted(Guid applicationId);
}