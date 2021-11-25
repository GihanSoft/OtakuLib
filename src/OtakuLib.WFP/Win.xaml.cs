using OtakuLib.Logic.ViewModels;

namespace OtakuLib.WFP
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
