using Control_Panel.MVVM.ViewModels;

namespace Control_Panel;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}