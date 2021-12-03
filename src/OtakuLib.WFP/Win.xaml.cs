using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

using HandyControl.Controls;
using HandyControl.Tools.Extension;

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
