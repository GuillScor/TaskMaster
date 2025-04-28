using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Models
{
    [Table("tache")]
    public class Tache
    {
        [Key]
        public int ID_Tache { get; set; }

        public string Titre { get; set; }

        public string Description { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime echeance { get; set; }

        public string Statut { get; set; }

        public string Priorite { get; set; }

        public string Categorie { get; set; }

        [ForeignKey("Projet")]
        public int ID_Projet { get; set; }
        public virtual Projet Projet { get; set; }

        [ForeignKey("Responsable")]
        public int ID_Responsable { get; set; }
        public virtual Utilisateur Responsable { get; set; }

        [ForeignKey("CreePar")]
        public int ID_CreePar { get; set; }
        public virtual Utilisateur CreePar { get; set; }

        [ForeignKey("TacheParent")]
        public int? ID_TacheParent { get; set; }
        public virtual Tache TacheParent { get; set; }

        public virtual ICollection<Tache> SousTaches { get; set; } = new List<Tache>();

        public virtual ICollection<Commentaire> Commentaires { get; set; } = new List<Commentaire>();

        // Modification ici : collection d'Etiquetted (la table de jointure)
        public virtual ICollection<Etiquetted> Etiquetteds { get; set; } = new List<Etiquetted>();
    }

}
