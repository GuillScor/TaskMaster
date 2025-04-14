using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Models;

[Table("projet")]
public class Projet
{
    [Key]
    public int ID_Projet { get; set; }

    public string Nom { get; set; }

    public virtual ICollection<Tache> Taches { get; set; }

    public virtual ICollection<Equipe> Membres { get; set; }
}
