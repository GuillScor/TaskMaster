using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskMaster.Models;
using TaskMaster.Views;
using TaskMaster.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskMaster.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private AppDbContext dbContext; //Database

    [ObservableProperty]
    private string taskTitle;
    [ObservableProperty]
    private string taskDescription;

    public ObservableCollection<Tache> Tasks { get; set; } = new();

    public MainViewModel(AppDbContext context)
    {
        dbContext = context;
        LoadTasks();        
    }

    private async void LoadTasks()
    {
        try
        {
            Console.WriteLine($"Load tasks");
            var tasks = await dbContext.Taches.ToListAsync();

            Tasks.Clear();
            foreach (var task in tasks)
            {
                Tasks.Add( task );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du chargement des tâches : {ex.Message}");
        }
    }


    [RelayCommand]
    private async void AddTask()
    {
        if (string.IsNullOrEmpty(TaskTitle))
        {
            Console.WriteLine("Le titre est requis.");
            return;
        }

        var newTask = new Tache
        {
            Titre = TaskTitle,
            Description = TaskDescription
        };

        try
        {
            dbContext.Taches.Add(newTask);
            await dbContext.SaveChangesAsync();

            Tasks.Add(newTask);

            TaskTitle = string.Empty;
            TaskDescription = string.Empty;

            Console.WriteLine("Tâche ajoutée.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'ajout de la tâche : {ex.Message}");
        }
    }



    [RelayCommand]
    private async Task ShowDetails(Tache task)
    {
        if (task != null)
        {
            // Naviguer vers la page de détails, en passant la tâche sélectionnée
            await Shell.Current.Navigation.PushAsync(new TaskDetailPage(task));
        }
    }

    [RelayCommand]
    private async void DeleteTask(Tache task)
    {
        if (task != null)
        {
            dbContext.Taches.Remove(task);
            await dbContext.SaveChangesAsync();

            Tasks.Remove(task);
        };
    }

}
