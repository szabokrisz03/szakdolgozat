using FluentValidation;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using TaskManager.Srv.Model.Validation;
using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Components.Forms;

public partial class CreateProjectForm
{
    private MudForm? Form { get; set; }

    public bool IsValid { get; private set; }
    public string[] Errors { get; private set; } = new string[0];

    [Parameter] public ProjectViewModel Model { get; set; } = new();
    [Parameter] public EventCallback<bool> OnValidate { get; set; }

    [Inject] public ProjectValidator Validator { get; private set; } = null!;

    public Func<object, string, Task<IEnumerable<string>>> FieldValidator => async (model, field) =>
    {
        var result = await Validator.ValidateAsync(ValidationContext<ProjectViewModel>.CreateWithOptions((ProjectViewModel)model, x => x.IncludeProperties(field)));
        IsValid = result.IsValid;
        await OnValidate.InvokeAsync(IsValid);
        return IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage).ToArray();
    };

    public async Task<bool> ValidateForm()
    {
        if (Form != null)
            await Form.Validate();

        IsValid = Form.IsValid;
        await OnValidate.InvokeAsync(IsValid);

        return IsValid;
    }
}
