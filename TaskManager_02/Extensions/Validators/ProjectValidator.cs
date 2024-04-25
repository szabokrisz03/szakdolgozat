using FluentValidation;

using TaskManager_02.Data.ViewModels;
using TaskManager_02.Services.ProjectServices;

namespace TaskManager_02.Extensions.Validators;

/// <summary>
/// 
///     A felhasználó által létrehozandó projekt tulajdonságainak validálása.
/// 
/// </summary>
public class ProjectValidator : AbstractValidator<ProjectViewModel>
{
    public ProjectValidator(IProjectService projectService)
    {
        RuleFor(x => x.ProjectName)
            .NotEmpty().WithMessage("A név kitöltése kötelező!")
            .MustAsync(async (name, cancellationToken) => await projectService.ProjectExistAsync(name)).WithMessage("Már létezik ilyen nevű projekt!");
    }
}
