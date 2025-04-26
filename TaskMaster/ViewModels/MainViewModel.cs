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
    private string nom;

    [ObservableProperty]
    private string prenom;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string motDePasse;

    [ObservableProperty]
    private bool afficherPopupInscription = true; // Affichage de l'overlay
    public bool PopupMasquee => !AfficherPopupInscription;

    // Surcharge du setter pour mettre à jour la visibilité de l'autre propriété automatiquement
    partial void OnAfficherPopupInscriptionChanged(bool oldValue, bool newValue)
    {
        // Cette ligne permet de notifier que la valeur de PopupMasquee a changé
        OnPropertyChanged(nameof(PopupMasquee));
    }



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

    [RelayCommand]
    private async Task AddUtilisateur()
    {
        if (string.IsNullOrWhiteSpace(Nom) || string.IsNullOrWhiteSpace(Prenom) ||
            string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(MotDePasse))
        {
            await Application.Current.MainPage.DisplayAlert("Erreur", "Veuillez remplir tous les champs.", "OK");
            return;
        }

        try
        {
            // Vérifier si l'email est déjà utilisé
            var existe = await dbContext.Utilisateurs.AnyAsync(u => u.Email == Email);
            if (existe)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Cet email est déjà utilisé.", "OK");
                return;
            }

            var utilisateur = new Utilisateur
            {
                Nom = Nom,
                Prenom = Prenom,
                Email = Email,
                MotDePasse = MotDePasse
            };

            dbContext.Utilisateurs.Add(utilisateur);
            await dbContext.SaveChangesAsync();

            Console.WriteLine("Utilisateur ajouté !");
            AfficherPopupInscription = false;
        }
        catch (Exception ex)
        {
            var fullMessage = ex.Message;
            if (ex.InnerException != null)
            {
                fullMessage += "\nDétails internes : " + ex.InnerException.Message;
            }

            Console.WriteLine($"Erreur lors de l'inscription : {fullMessage}");
            await Application.Current.MainPage.DisplayAlert("Erreur", $"Exception : {fullMessage}", "OK");
        }
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
