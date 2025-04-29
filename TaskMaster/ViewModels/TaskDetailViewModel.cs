using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskMaster.Models;

namespace TaskMaster.ViewModels;

public partial class TaskDetailViewModel : ObservableObject
{
    [ObservableProperty]
    private Tache task;

    public TaskDetailViewModel(Tache task)
    {
        Task = task;
    }

    [RelayCommand]
    private async Task Return()
    {
        await Shell.Current.GoToAsync(".."); // Retour à la page précédente
    }

}
