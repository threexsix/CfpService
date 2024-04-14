using CfpService.Application.Mappers.ApplicationMapper;
using CfpService.Application.Queries.Application;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Dtos.Application;
using CfpService.Contracts.Errors;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Handlers.Queries;

public class GetUnSubmittedApplicationByUserIdQueryHandler : IRequestHandler<GetUnSubmittedApplicationByUserIdQuery, Result<GetApplicationDto>>
{
    private readonly IApplicationRepository _repository;
    private readonly IApplicationMapper _mapper;

    public GetUnSubmittedApplicationByUserIdQueryHandler(IApplicationRepository repository, IApplicationMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<GetApplicationDto>> Handle(GetUnSubmittedApplicationByUserIdQuery request, CancellationToken cancellationToken)
    {
        var application = await _repository.GetUserUnSubmittedApplication(request.UserId);

        if (application == null)
            return Result.Fail<GetApplicationDto>(ApplicationErrors.UserUnsubmittedApplicationNotFound());
        
        return Result.Ok(_mapper.ToDto(application));
    }
}