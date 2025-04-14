using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Models;


[Table("utilisateurs")]
public class Utilisateur
{
    [Key]
    public int ID_Utilisateur { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string Email { get; set; }

    public string MotDePasse { get; set; }  

    public virtual ICollection<Tache> TachesResponsable { get; set; }

    public virtual ICollection<Tache> TachesCree { get; set; }

    public virtual ICollection<Equipe> Projets { get; set; }

    public virtual ICollection<Commentaire> Commentaires { get; set; }

}

