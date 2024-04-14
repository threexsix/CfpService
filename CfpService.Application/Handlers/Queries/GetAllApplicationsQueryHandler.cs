using CfpService.Application.Mappers.ApplicationMapper;
using CfpService.Application.Queries.Application;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Dtos.Application;
using CfpService.Contracts.Errors;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Handlers.Queries;

public class GetAllApplicationsQueryHandler : IRequestHandler<GetAllApplicationsQuery, Result<List<GetApplicationDto>>>
{
    private readonly IApplicationRepository _repository;
    private readonly IApplicationMapper _mapper;

    public GetAllApplicationsQueryHandler(IApplicationRepository repository, IApplicationMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetApplicationDto>>> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
    {
        if (request.SubmittedAfter.HasValue == request.UnsubmittedOlder.HasValue)
        {
            return Result.Fail<List<GetApplicationDto>>(ApplicationErrors.UnspecifiedParameter());
        }

        var applications = await (request.SubmittedAfter.HasValue ? 
            GetSubmittedApplications(request.SubmittedAfter.Value) : 
            GetUnSubmittedApplications(request.UnsubmittedOlder.Value));
        
        return Result.Ok(applications);
    }
    
    private async Task<List<GetApplicationDto>> GetSubmittedApplications(DateTime time)
    {
        var applications = await _repository.GetSubmittedApplications(time);
        return applications.Select(x => _mapper.ToDto(x)).ToList();
    }
    
    private async Task<List<GetApplicationDto>> GetUnSubmittedApplications(DateTime time)
    {
        var applications = await _repository.GetUnSubmittedApplications(time);
        return applications.Select(x => _mapper.ToDto(x)).ToList();
    }
}