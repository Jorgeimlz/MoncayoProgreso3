using MoncayoProgreso3.ViewModels;
using MoncayoProgreso3.Data;
using MoncayoProgreso3.ViewModels;

namespace MoncayoProgreso3.Views
{
    public partial class HeroesPageJorge : ContentPage
    {
        private HeroesPageJorgeViewModel _viewModel;

        public HeroesPageJorge()
        {
            InitializeComponent();
            _viewModel = new HeroesPageJorgeViewModel();
            BindingContext = _viewModel;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            _viewModel.ConsultarCommand.Execute(null);
        }

        private void ButtonHeroes_Clicked(object sender, EventArgs e)
        {
            _viewModel.IrAHeroesGuardadosCommand.Execute(null);
        }
    }
}
