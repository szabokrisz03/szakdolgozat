using FluentValidation;

using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.TaskServices;

namespace TaskManager.Srv.Model.Validation;

public class TaskValidator : AbstractValidator<TaskViewModel>
{
    public TaskValidator(ITaskDisplayService taskDisplayService)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("A név kitöltése kötelező")
            .NotNull().WithMessage("A név kitöltése kötelező")

            .MustAsync(async (name, _) => !await taskDisplayService.TaskNameExistsAsync(name))
            .WithMessage("Ilyen néven már létezik feladat!");
    }
}
