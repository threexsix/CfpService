using CfpService.Application.Commands.Application;
using CfpService.Application.Mappers.ApplicationMapper;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Dtos.Application;
using MediatR;

namespace CfpService.Application.Handlers.Commands;

public class EditApplicationCommandHandler : IRequestHandler<EditApplicationCommand, GetApplicationDto>
{
    private readonly IApplicationRepository _repository;
    private readonly IApplicationMapper _mapper;

    public EditApplicationCommandHandler(IApplicationRepository repository, IApplicationMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<GetApplicationDto> Handle(EditApplicationCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.ExistByApplicationId(request.Dto.Id) == false)
            throw new KeyNotFoundException($"application with id {request.Dto.Id} not found");
            
        if (await _repository.IsSubmitted(request.Dto.Id))
            throw new ArgumentException("cannot edit submitted application");

        var application = await _repository.GetById(request.Dto.Id);
        
        var alteredApplication = await _repository.Put(_mapper.ToEntity(request.Dto, application));
        
        return _mapper.ToDto(alteredApplication);
    }
}