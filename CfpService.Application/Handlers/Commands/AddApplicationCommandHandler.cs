using CfpService.Application.Commands.Application;
using CfpService.Application.Mappers.ApplicationMapper;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Dtos.Application;
using MediatR;

namespace CfpService.Application.Handlers.Commands;

public class AddApplicationCommandHandler : IRequestHandler<AddApplicationCommand, GetApplicationDto>
{
    private readonly IApplicationRepository _repository;
    private readonly IApplicationMapper _mapper;

    public AddApplicationCommandHandler(IApplicationRepository repository, IApplicationMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<GetApplicationDto> Handle(AddApplicationCommand request, CancellationToken cancellationToken)
    {
        if (_repository.ExistUnsubmittedFromUser(request.Dto.Author))
            throw new ArgumentException("cannot add, user has not-submitted application");

        var application = _mapper.ToEntity(request.Dto);
        var addedApplication = _repository.Add(application);
        
        return _mapper.ToDto(addedApplication);
    }
}