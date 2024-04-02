using N5User.Data.Models;
using MediatR;
using System.Numerics;
using N5User.Data.Dtos;

namespace N5User.Queries;


public class GetPermissionsQuery : IRequest<List<PermissionDto>>
{
    
}