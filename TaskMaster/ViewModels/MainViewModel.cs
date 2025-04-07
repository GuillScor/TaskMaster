using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskMaster.Models;

namespace TaskMaster.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string taskTitle;
    [ObservableProperty]
    private string taskDescription;

    //private int count;
    //private string counterText = "Click me";


    public ObservableCollection<TaskModel> Tasks { get; set; } = new();

    //public string CounterText
    //{
    //    get => counterText;
    //    set => SetProperty(ref counterText, value);
    //}

    public MainViewModel()
    {
        //CounterText = "Click me";
    }


    [RelayCommand]
    private void AddTask()
    {
        if (!string.IsNullOrEmpty(TaskTitle) && !string.IsNullOrWhiteSpace(TaskDescription))
        {
            Tasks.Add(new TaskModel { Title = TaskTitle, Description = TaskDescription });

            TaskTitle = string.Empty;
            TaskDescription = string.Empty;
        }
    }

    [RelayCommand]
    private void DeleteTask(TaskModel task)
    {
        Tasks.Remove(task);
    }

}
