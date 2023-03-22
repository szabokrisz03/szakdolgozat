using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TaskManager.Srv.Model.DataModel;

[Serializable]
public enum TaskState
{
    [Display(Name = "Igényfelmérés")]
    Igeny_felmeres = 0,

    [Display(Name = "Specifikáció alatt")]
    Specifikacio_alatt,

    [Display(Name = "Fejlesztésre vár")]
    Fejlesztesre_var,

    [Display(Name = "Fejlesztés alatt")]
    Fejlesztes_alatt,

    [Display(Name = "Tesztelés alatt")]
    Teszteles_alatt,

    [Display(Name = "Kiadásra vár")]
    Kiadasra_var,

    [Display(Name = "Verziózva")]
    Verziozva,
}