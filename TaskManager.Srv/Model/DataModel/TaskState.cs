using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TaskManager.Srv.Model.DataModel;

[Serializable]
public enum TaskState
{
    [Display(Name = "Ig�nyfelm�r�s")]
    Igeny_felmeres = 0,

    [Display(Name = "Specifik�ci� alatt")]
    Specifikacio_alatt,

    [Display(Name = "Fejleszt�sre v�r")]
    Fejlesztesre_var,

    [Display(Name = "Fejleszt�s alatt")]
    Fejlesztes_alatt,

    [Display(Name = "Tesztel�s alatt")]
    Teszteles_alatt,

    [Display(Name = "Kiad�sra v�r")]
    Kiadasra_var,

    [Display(Name = "Verzi�zva")]
    Verziozva,
}