using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Models;
[Table("equipe")]
public class Equipe
{
    [Key, Column(Order = 0)]
    public int ID_Utilisateur { get; set; }

    [Key, Column(Order = 1)]
    public int ID_Projet { get; set; }

    [ForeignKey("ID_Utilisateur")]
    public virtual Utilisateur Utilisateur { get; set; }

    [ForeignKey("ID_¨Projet")]
    public virtual Projet Projet { get; set; }
}