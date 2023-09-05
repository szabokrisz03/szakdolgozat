using System.ComponentModel.DataAnnotations;

namespace TaskManager.Srv.Model.ViewModel;

public class StateFilterViewModell
{
    [Display(Name = "Igényfelmérés")]
    public bool Igenyfelmeres { get; set; } = true;
    [Display(Name = "Specifikáció alatt")]
    public bool SpecifikacioAlatt { get; set; } = true;
    [Display(Name = "Fejlesztésre vár")]
    public bool FejlsztesreVar { get; set; } = true;
    [Display(Name = "Fejlesztés alatt")]
    public bool FejlesztesAlatt { get; set; } = true;
    [Display(Name = "Tesztelés alatt")]
    public bool TesztelesAlatt { get; set; } = true;
    [Display(Name = "Kiadásra vár")]
    public bool KiadasraVar { get; set; } = true;
    [Display(Name = "Verziózva")]
    public bool Verziozva { get; set; } = true;
    [Display(Name = "Ajánlatadás")]
    public bool Ajanlatadas { get; set; } = true;
    [Display(Name = "Meghiúsult")]
    public bool Meghiusult { get; set; } = true;
}
