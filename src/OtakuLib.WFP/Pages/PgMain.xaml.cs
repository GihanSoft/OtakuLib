using OtakuLib.Logic.Pages;
using OtakuLib.Logic.ViewModels.PgMainViewModels;
using OtakuLib.WPF.Pages.PgMainTabs;

namespace OtakuLib.WPF.Pages;

/// <summary>
/// Interaction logic for PgMain.xaml
/// </summary>
public partial class PgMain : IPgMain
{
    /// <summary>Identifies the <see cref="SelectedTab"/> dependency property.</summary>
    public static readonly DependencyProperty SelectedTabProperty = DependencyProperty.Register(
    nameof(SelectedTab),
            typeof(FrameworkElement),
            typeof(PgMain),
            new PropertyMetadata(default(FrameworkElement)));

    private readonly PgTabBrowse pgTabBrowse;

    public PgMain(IPgMainVM viewModel, PgTabBrowse pgTabBrowse)
    {
        ViewModel = viewModel;
        this.pgTabBrowse = pgTabBrowse;
        Title = "Main Page";
        InitializeComponent();
        SelectedTab = pgTabBrowse;
    }

    public IPgMainVM ViewModel { get; }

    public FrameworkElement? SelectedTab
    {
        get => (FrameworkElement?)GetValue(SelectedTabProperty);
        set => SetValue(SelectedTabProperty, value);
    }
}