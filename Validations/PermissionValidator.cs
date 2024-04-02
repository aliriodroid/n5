using System.Security.Cryptography.Xml;
using FluentValidation;
using N5User.Commands;
using N5User.Data.Models;

namespace N5User.Validations;

public class PermissionValidator:AbstractValidator<CreatePermissionCommand>
{
    public PermissionValidator()
    {
        RuleFor(x => x.EmployeeForename).NotEmpty().Length(3,20).WithMessage("Please specify a Forename");
        RuleFor(x => x.EmployeeSurname).NotEmpty().WithMessage("Please specify a Surename");
        RuleFor(x => x.PermissionDate).NotEmpty();
    }
    
    
}