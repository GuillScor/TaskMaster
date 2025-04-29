using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Models;

[Table("commentaires")]
public class Commentaire
{
    [Key]
    public int ID_Commentaire { get; set; }

    public string Contenu { get; set; }

    public DateTime DatePost { get; set; }

    [ForeignKey("Utilisateur")]
    public int ID_Utilisateur { get; set; }
    public virtual Utilisateur Utilisateur { get; set; }

    [ForeignKey("Tache")]
    public int ID_Tache { get; set; }
    public virtual Tache Tache { get; set; }

}