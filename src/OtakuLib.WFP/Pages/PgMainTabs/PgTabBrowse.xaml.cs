using System.Windows.Controls;

using OtakuLib.Logic.ViewModels.PgMainViewModels;

namespace OtakuLib.WPF.Pages.PgMainTabs;
/// <summary>
/// Interaction logic for PgTabBrowse.xaml
/// </summary>
public partial class PgTabBrowse : UserControl
{
    public PgTabBrowse(IPgTabBrowseVM viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }

    public IPgTabBrowseVM ViewModel { get; }
}
