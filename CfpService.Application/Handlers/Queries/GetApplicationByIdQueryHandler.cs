using CfpService.Application.Mappers.ApplicationMapper;
using CfpService.Application.Queries.Application;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Dtos.Application;
using MediatR;

namespace CfpService.Application.Handlers.Queries;

public class GetApplicationByIdQueryHandler : IRequestHandler<GetApplicationByIdQuery, GetApplicationDto>
{
    private readonly IApplicationRepository _repository;
    private readonly IApplicationMapper _mapper;

    public GetApplicationByIdQueryHandler(IApplicationRepository repository, IApplicationMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetApplicationDto> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _repository.ExistByApplicationId(request.Id) == false)
            throw new KeyNotFoundException($"application with id {request.Id} not found");
        
        var application = await _repository.GetById(request.Id);
        
        return _mapper.ToDto(application);
    }
}