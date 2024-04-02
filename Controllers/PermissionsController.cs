using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5User.Commands;
using N5User.Data.Dtos;
using N5User.Data.Models;
using N5User.Data.UnitOfWork;
using N5User.Queries;
using N5User.Services;

namespace N5User.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionsController: ControllerBase
{
    private readonly IMediator mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly MessageService _messageService;

    public PermissionsController(IMediator mediator, IUnitOfWork unitOfWork,MessageService messageService )
    {
        this.mediator = mediator;
        _unitOfWork = unitOfWork;
        _messageService = messageService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPermissionsAsync()
    {
        
        var permssions = await mediator.Send(new GetPermissionsQuery());
        await _messageService.ProduceAsync("Permissions", new MessageDto(Guid.NewGuid(), "get"));
        return Ok(permssions);
    }
    
    [HttpPost]
    public async Task<IActionResult> RequestPermissionAsync(PermissionDto permissionDto)
    {
        Permission permission = await mediator.Send(new CreatePermissionCommand(permissionDto));
        await _messageService.ProduceAsync("Permissions", new MessageDto(Guid.NewGuid(), "request"));
        return Ok(permission);
    }

    [HttpPut]
    public async Task<IActionResult> ModifyPermissionAsync(int id,[FromBody] PermissionDto permissionDto)
    {
        var permissionUpdated = await mediator.Send(new UpdatePermissionCommand(id,permissionDto));
        await _messageService.ProduceAsync("Permissions", new MessageDto(Guid.NewGuid(), "modify"));
        return Ok(permissionUpdated);
    }
    
}