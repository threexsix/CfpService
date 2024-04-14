using CfpService.Application.Mappers.ApplicationMapper;
using CfpService.Application.Queries.Application;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Dtos.Application;
using MediatR;

namespace CfpService.Application.Handlers.Queries;

public class GetUnSubmittedApplicationByUserIdQueryHandler : IRequestHandler<GetUnSubmittedApplicationByUserIdQuery, GetApplicationDto>
{
    private readonly IApplicationRepository _repository;
    private readonly IApplicationMapper _mapper;

    public GetUnSubmittedApplicationByUserIdQueryHandler(IApplicationRepository repository, IApplicationMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetApplicationDto> Handle(GetUnSubmittedApplicationByUserIdQuery request, CancellationToken cancellationToken)
    {
        var application = _repository.GetUserUnSubmittedApplication(request.UserId);
        
        if (application == null) 
            throw new ArgumentException("user doesn't have any submitted applications");
        
        return _mapper.ToDto(application);
    }
}