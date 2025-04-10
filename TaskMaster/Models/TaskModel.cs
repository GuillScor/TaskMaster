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

    public string Titre { get; set; }

    public string Description { get; set; }
}