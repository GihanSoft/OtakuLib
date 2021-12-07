using System.Windows;

using OtakuLib.WPF.ViewModels;

namespace OtakuLib.WPF;

/// <summary>
/// Interaction logic for Win.xaml
/// </summary>
[CLSCompliant(false)]
public sealed partial class Win
{
    public Win(WinVM viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }

    public WinVM ViewModel { get; }

    private void NavStackBtn_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.IsBackStackOpen = false;
        ViewModel.IsForwardStackOpen = false;
    }
}
