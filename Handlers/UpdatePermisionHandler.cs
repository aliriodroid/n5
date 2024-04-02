using MediatR;
using N5User.Commands;
using N5User.Data.Dtos;
using N5User.Data.UnitOfWork;
using Nest;

namespace N5User.Handlers;

public class UpdatePermisionHandler : IRequestHandler<UpdatePermissionCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ElasticClient _elasticClient;

    public UpdatePermisionHandler(IUnitOfWork unitOfWork, ElasticClient elasticClient)
    {
        _unitOfWork = unitOfWork;
        _elasticClient = elasticClient;
    }
    
    public async Task<int> Handle(UpdatePermissionCommand command, CancellationToken cancellationToken)
    {
        var permission = await _unitOfWork.PermissionRepository.GetPermisionById(command.Id);
        if (permission == null)
            return default;

        permission.EmployeeForename = command.EmployeeForename;
        permission.EmployeeSurname = command.EmployeeSurname;
        permission.PermissionDate = command.PermissionDate;
        permission.PermissionTypeId = command.PermisitonTypeId;

        var permTypeDto = new PermissionTypeDto(permission.PermissionType.Description);

        PermissionDto permissionDto = new PermissionDto(
            permission.EmployeeForename,
            permission.EmployeeSurname,
            permission.PermissionTypeId,
            permission.PermissionDate,
            permTypeDto);
        
        var result = await _unitOfWork.PermissionRepository.ModifyPermissionAsync(command.Id,permission);
        await _unitOfWork.Save();
        await  _elasticClient.IndexDocumentAsync(permissionDto);
        return result ;
        
    }
}