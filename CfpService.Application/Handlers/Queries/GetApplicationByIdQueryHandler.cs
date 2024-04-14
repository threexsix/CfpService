using CfpService.Application.Mappers.ApplicationMapper;
using CfpService.Application.Queries.Application;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Dtos.Application;
using CfpService.Contracts.Errors;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Handlers.Queries;

public class GetApplicationByIdQueryHandler : IRequestHandler<GetApplicationByIdQuery, Result<GetApplicationDto>>
{
    private readonly IApplicationRepository _repository;
    private readonly IApplicationMapper _mapper;

    public GetApplicationByIdQueryHandler(IApplicationRepository repository, IApplicationMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<GetApplicationDto>> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _repository.ExistByApplicationId(request.Id) == false)
            return Result.Fail<GetApplicationDto>(ApplicationErrors.ApplicationNotFound(request.Id));

        var application = await _repository.GetById(request.Id);
        
        return Result.Ok(_mapper.ToDto(application));
    }
}