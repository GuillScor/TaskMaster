using TaskMaster.Models;
using TaskMaster.ViewModels;

namespace TaskMaster.Views;

public partial class TaskDetailPage : ContentPage
{
    public TaskDetailPage(TaskModel task)
    {
        InitializeComponent();
        BindingContext = new TaskDetailViewModel(task);
    }
}
