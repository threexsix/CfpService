using CfpService.Application.Mappers.ActivityMapper;
using CfpService.Application.Queries.Activity;
using CfpService.Application.Repositories.Activity;
using CfpService.Contracts.Dtos.Activity;
using MediatR;

namespace CfpService.Application.Handlers.Queries;

public class GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery, List<GetActivityDto>>
{
    private readonly IActivityRepository _repository;
    private readonly IActivityMapper _mapper;

    public GetAllActivitiesQueryHandler(IActivityRepository repository, IActivityMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetActivityDto>> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
    {
        var activities = await _repository.GetAllActivities();
        
        if (activities == null) 
            throw new  ArgumentException("no activity found");
            
        return activities.Select(x => _mapper.ToDto(x)).ToList();
    }
}