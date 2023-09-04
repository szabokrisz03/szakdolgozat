using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TaskManager.Srv.Model.Validation;
using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Components.Forms;

public partial class CreateMilestoneForm
{
    private MudForm? MudForm;
    public bool IsValid { get; private set; }
    public string[] Errors { get; private set; } = new string[0];

    [Parameter] public MilestoneViewModel milestoneView { get; set; } = new();
    [Parameter] public EventCallback<bool> OnValidate { get; set; }

    [Inject] public MilestoneValidator Validator { get; private set; } = null!;

    /// <summary>
    /// Validálás.
    /// </summary>
    public Func<object, string, Task<IEnumerable<string>>> FieldValidator => async (model, field) =>
    {
        var result = await Validator.ValidateAsync(ValidationContext<MilestoneViewModel>.CreateWithOptions((MilestoneViewModel)model, x => x.IncludeProperties(field).IncludeProperties(nameof(MilestoneViewModel.TaskId))));
        IsValid = result.IsValid;
        await OnValidate.InvokeAsync(IsValid);
        return IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage).ToArray();
    };
}
