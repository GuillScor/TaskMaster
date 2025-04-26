using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Models;
[Table("etiquettes")]
public class Etiquette
{
    [Key]
    public int ID_Etiquette { get; set; }
    public string Nom { get; set; }
    public virtual ICollection<Tache> Taches { get; set; }
}