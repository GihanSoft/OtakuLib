using OtakuLib.Logic.ViewModels;

namespace OtakuLib.View
{
    /// <summary>
    /// Interaction logic for Win.xaml
    /// </summary>
    public sealed partial class Win
    {
        public Win(IWinVM viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        public IWinVM ViewModel { get; }
    }
}
