﻿using FluentValidation;

using TaskManager.Srv.Model.ViewModel;
using TaskManager.Srv.Services.MilestoneServices;
using TaskManager.Srv.Services.ProjectServices;
using TaskManager.Srv.Services.TaskServices;

namespace TaskManager.Srv.Model.Validation;

public class MilestoneValidator : AbstractValidator<MilestoneViewModel>
{
    public MilestoneValidator(
    IMilestoneDisplayService milestoneDisplayService)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("A név kitöltése kötelező")
            .NotNull().WithMessage("A név kitöltése kötelező");

        RuleFor(x => x.Name)
            .CustomAsync(async (name, context, _) =>
            {
                var dto = context.InstanceToValidate;
                if (await milestoneDisplayService.MilestoneNameExistsAsync(dto.TaskId, dto.Name))
                {
                    context.AddFailure("Ezzel a névvel már létezik mérföldkő a feladatban!");
                }
            });
    }
}