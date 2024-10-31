using Snake.ViewModels;

namespace Snake.Views;

public partial class MainWindow
{
    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        DataContext = mainWindowViewModel;
        InitializeComponent();
    }
}
