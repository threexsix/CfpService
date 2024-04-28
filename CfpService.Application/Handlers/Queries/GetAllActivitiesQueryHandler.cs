using CfpService.Application.Mappers.ActivityMapper;
using CfpService.Application.Queries.Activity;
using CfpService.Application.Repositories.Activity;
using CfpService.Contracts.Dtos.Activity;
using CfpService.Contracts.Errors;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Handlers.Queries;

public class GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery, Result<List<GetActivityDto>>>
{
    private readonly IActivityRepository _repository;
    private readonly IActivityMapper _mapper;

    public GetAllActivitiesQueryHandler(IActivityRepository repository, IActivityMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetActivityDto>>> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
    {
        var activities = (await _repository.GetAllActivitiesAsync()).ToList();

        if (!activities.Any())
            return Result.Fail<List<GetActivityDto>>(ActivityErrors.ActivitiesNotFound());
            
        return Result.Ok(activities.Select(x => _mapper.ToDto(x)).ToList());
    }
}