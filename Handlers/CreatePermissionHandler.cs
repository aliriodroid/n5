using FluentValidation;
using MediatR;
using N5User.Commands;
using N5User.Data.Models;
using N5User.Data.UnitOfWork;
using Nest;

namespace N5User.Handlers;

public class CreatePermissionHandler: IRequestHandler<CreatePermissionCommand, Permission>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ElasticClient _elasticClient;

    private readonly IValidator<CreatePermissionCommand> _permissionValidator;
    
    public CreatePermissionHandler(IUnitOfWork unitOfWork,IValidator<CreatePermissionCommand> permissionValidator, ElasticClient elasticClient)
    {
        _unitOfWork = unitOfWork;
        _permissionValidator = permissionValidator;
        _elasticClient = elasticClient;
    }
    public async Task<Permission> Handle(CreatePermissionCommand command, CancellationToken cancellationToken)
    {
        _permissionValidator.ValidateAndThrow(command);
        
        
        var permission = new Permission()
        {
            EmployeeForename = command.EmployeeForename,
            EmployeeSurname = command.EmployeeSurname,
            PermissionTypeId = command.PermisitonTypeId,
            PermissionDate = command.PermissionDate
        };
        var result = await _unitOfWork.PermissionRepository.RequestPermissionAsync(permission);
        await _unitOfWork.Save();
        await  _elasticClient.IndexDocumentAsync(permission);
        return result ;
    }
    
}