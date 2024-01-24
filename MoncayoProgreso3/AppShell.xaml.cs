using MoncayoProgreso3.Views;

namespace MoncayoProgreso3;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(HeroesGuardadosJorge), typeof(HeroesGuardadosJorge));
        Routing.RegisterRoute(nameof(HeroesPageJorge), typeof(HeroesPageJorge));

    }
}
