using System.ComponentModel;
using System.Windows.Threading;

using GihanSoft.AppBase.Services;

using OtakuLib.WPF.Models.Data;
using OtakuLib.WPF.ViewModels;

namespace OtakuLib.WPF;

/// <summary>
/// Interaction logic for Win.xaml
/// </summary>
[CLSCompliant(false)]
public sealed partial class Win
{
    private readonly IDataManager<WinState> dataProvider;
    private WindowState validWindowState;

    public Win(WinVM viewModel, IDataManager<WinState> dataProvider)
    {
        this.dataProvider = dataProvider;
        ViewModel = viewModel;

        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs args)
    {
        if (!dataProvider.TryFetch(out var state) || state is null)
        {
            return;
        }

        SetCurrentValue(LeftProperty, state.Left);
        SetCurrentValue(TopProperty, state.Top);
        SetCurrentValue(WidthProperty, state.Width);
        SetCurrentValue(HeightProperty, state.Height);
        SetCurrentValue(WindowStateProperty, state.WindowState);

        validWindowState = state.WindowState;
    }

    public WinVM ViewModel { get; }

    private void NavStackBtn_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.IsBackStackOpen = false;
        ViewModel.IsForwardStackOpen = false;
    }

    protected override void OnStateChanged(EventArgs e)
    {
        base.OnStateChanged(e);
        if (WindowState is not WindowState.Minimized)
        {
            validWindowState = WindowState;
        }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);

        dataProvider.Save(new WinState
        {
            Left = RestoreBounds.Left,
            Top = RestoreBounds.Top,
            Width = RestoreBounds.Width,
            Height = RestoreBounds.Height,
            WindowState = validWindowState,
        });
    }
}