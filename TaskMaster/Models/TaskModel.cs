using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TaskMaster.Models;

[Table("tache")]
public class Tache
{
    [Key]
    public int ID_Tache { get; set; }

    [Column("titre")]
    public string Titre { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("DateCreation")]
    public DateTime? DateCreation { get; set; }
    [Column("echeance")]
    public DateTime? DateEcheance { get; set; }
    [Column("statut")]
    public string? Statut { get; set; }
    [Column("priorite")]
    public string? Priorite { get; set; }
    [Column("categorie")]
    public string? Categorie { get; set; }

    [ForeignKey("Projet")]
    [Column("ID_Projet")]
    public int? ID_Projet { get; set; }
    public virtual Projet Projet { get; set; }

    [ForeignKey("Responsable")]
    [Column("ID_Realisateur")]
    public int ID_Responsable { get; set; }
    public virtual Utilisateur Responsable { get; set; }

    [ForeignKey("CreePar")]
    [Column("ID_Auteur")]
    public int ID_CreePar { get; set; }
    public virtual Utilisateur CreePar { get; set; }

    [ForeignKey("TacheParent")]
    [Column("ID_Tache_Parent")]
    public int? ID_TacheParent { get; set; }
    public virtual Tache TacheParent { get; set; }

    public virtual ICollection<Tache> SousTaches { get; set; }

    public virtual ICollection<Commentaire> Commentaires { get; set; }

    public virtual ICollection<Etiquetted> Etiquetteds { get; set; } = new List<Etiquetted>();
}