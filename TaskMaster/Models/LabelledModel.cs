using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Models;
[Table("etiquettes")]
public class Etiquetted
{
    [Key, Column(Order = 0)]
    public int ID_Tache { get; set; }   

    [Key, Column(Order = 1)]
    public int ID_Etiquette { get; set; }

    [ForeignKey("ID_Tache")]
    public virtual Tache Tache { get; set; }

    [ForeignKey("ID_Etiquette")]
    public virtual Etiquette Etiquette { get; set; }
}