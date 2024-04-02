using MediatR;
using N5User.Data.Dtos;
using N5User.Data.Models;
using N5User.Data.UnitOfWork;
using N5User.Queries;
using Nest;

namespace N5User.Handlers;

public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, List<PermissionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ElasticClient _elasticClient;

    public GetPermissionsHandler(IUnitOfWork unitOfWork, ElasticClient elasticClient)
    {
        _unitOfWork = unitOfWork;
        _elasticClient = elasticClient;
    }

    public async Task<List<PermissionDto>> Handle(GetPermissionsQuery query, CancellationToken cancellationToken)
    {
        var results = await _unitOfWork.PermissionRepository.GetPermissionsAsync();
        foreach (var result in results)
        {
            await  _elasticClient.IndexDocumentAsync(result);
        }
        return results;
    }
    
}