using CfpService.Application.Commands.Application;
using CfpService.Application.Mappers.ApplicationMapper;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Dtos.Application;
using CfpService.Contracts.Errors;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Handlers.Commands;

public class AddApplicationCommandHandler : IRequestHandler<AddApplicationCommand, Result<GetApplicationDto>>
{
    private readonly IApplicationRepository _repository;
    private readonly IApplicationMapper _mapper;

    public AddApplicationCommandHandler(IApplicationRepository repository, IApplicationMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<Result<GetApplicationDto>> Handle(AddApplicationCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.ExistUnsubmittedFromUser(request.Dto.Author))
            return Result.Fail<GetApplicationDto>(ApplicationErrors.UserHasUnsubmittedApplication());

        var application = _mapper.ToEntity(request.Dto);
        var addedApplication = await _repository.Add(application);
        
        return Result.Ok(_mapper.ToDto(addedApplication));
    }
}