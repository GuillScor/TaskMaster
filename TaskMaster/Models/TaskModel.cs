using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TaskMaster.Models;

[Table("Tache")] // Assurez-vous que le nom de la table correspond à votre table dans la base de données.
public class TaskModel
{
    [Key] // Cette annotation indique que la propriété est la clé primaire.
    public int ID_Tache { get; set; }

    [Required] // Assurez-vous que le titre ne soit pas nul.
    public string Titre { get; set; }

    [Required] // Assurez-vous que la description ne soit pas nulle.
    public string Description { get; set; }
}