using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskMaster.Models;
using TaskMaster.Views;
using TaskMaster.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace TaskMaster.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly AppDbContext dbContext;

        // Champs de formulaire Inscription
        [ObservableProperty] private string nom;
        [ObservableProperty] private string prenom;
        [ObservableProperty] private string email;
        [ObservableProperty] private string motDePasse;

        // Champs de formulaire Connexion
        [ObservableProperty] private string connexionEmail;
        [ObservableProperty] private string connexionMotDePasse;

        [ObservableProperty] private Utilisateur utilisateurConnecte;

        // Affichage du popup
        [ObservableProperty] private bool afficherPopupInscription = true;
        public bool PopupMasquee => !AfficherPopupInscription;

        // Affichage du nom de l'utilisateur connecté
        public string NomUtilisateurConnecte => UtilisateurConnecte != null ? $"{UtilisateurConnecte.Prenom} {UtilisateurConnecte.Nom}" : string.Empty;

        [ObservableProperty] private bool isLoggedIn;

        // Champs pour Ajout de Tâche
        [ObservableProperty] private string taskTitle;
        [ObservableProperty] private string taskDescription;

        public ObservableCollection<Tache> Tasks { get; set; } = new();

        public MainViewModel(AppDbContext context)
        {
            dbContext = context;
            LoadTasks();
        }

        // ----------- INSCRIPTION -----------
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

                UtilisateurConnecte = utilisateur;
                IsLoggedIn = true;
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

        // ----------- CONNEXION -----------
        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(ConnexionEmail) || string.IsNullOrWhiteSpace(ConnexionMotDePasse))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Veuillez remplir tous les champs de connexion.", "OK");
                return;
            }

            try
            {
                var utilisateur = await dbContext.Utilisateurs
                    .FirstOrDefaultAsync(u => u.Email == ConnexionEmail && u.MotDePasse == ConnexionMotDePasse);

                if (utilisateur == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", "Email ou mot de passe incorrect.", "OK");
                    return;
                }

                UtilisateurConnecte = utilisateur;
                IsLoggedIn = true;
                AfficherPopupInscription = false;

                Console.WriteLine("Utilisateur connecté !");
                LoadTasks();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la connexion : {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Erreur", $"Exception : {ex.Message}", "OK");
            }
        }

        // ----------- GESTION DES PROPRIÉTÉS -----------
        partial void OnAfficherPopupInscriptionChanged(bool oldValue, bool newValue)
        {
            OnPropertyChanged(nameof(PopupMasquee));
        }

        partial void OnUtilisateurConnecteChanged(Utilisateur oldValue, Utilisateur newValue)
        {
            OnPropertyChanged(nameof(NomUtilisateurConnecte));
        }

        // ----------- TÂCHES -----------
        private async void LoadTasks()
        {
            try
            {
                if (UtilisateurConnecte == null)
                    return;

                var tasks = await dbContext.Taches
                    .Where(t => t.ID_CreePar == UtilisateurConnecte.ID_Utilisateur)
                    .ToListAsync();

                Tasks.Clear();
                foreach (var task in tasks)
                {
                    Tasks.Add(task);
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
            if (string.IsNullOrWhiteSpace(TaskTitle))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Le titre de la tâche est requis.", "OK");
                return;
            }

            if (UtilisateurConnecte == null)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Aucun utilisateur connecté. Veuillez vous connecter.", "OK");
                return;
            }

            var newTask = new Tache
            {
                Titre = TaskTitle,
                Description = TaskDescription,
                ID_CreePar = UtilisateurConnecte.ID_Utilisateur,
                ID_Responsable = UtilisateurConnecte.ID_Utilisateur
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
                var fullMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    fullMessage += "\nDétails internes : " + ex.InnerException.Message;
                }

                Console.WriteLine($"Erreur lors de l'ajout de la tâche : {fullMessage}");
                await Application.Current.MainPage.DisplayAlert("Erreur", $"Erreur lors de l'ajout de la tâche : {fullMessage}", "OK");
            }
        }


        [RelayCommand]
        private async Task ShowDetails(Tache task)
        {
            if (task != null)
            {
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
            }
        }
    }
}
