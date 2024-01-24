using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MoncayoProgreso3.Data;
using MoncayoProgreso3.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using MoncayoProgreso3.Views;

namespace MoncayoProgreso3.ViewModels
{
    public class HeroesPageJorgeViewModel : BaseViewModel
    {
        private HeroesDbContext _dbContext;

        public HeroesPageJorgeViewModel()
        {
            _dbContext = new HeroesDbContext(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HeroesGuardados.db3"));

            ConsultarCommand = new Command(Consultar);
            IrAHeroesGuardadosCommand = new Command(IrAHeroesGuardados);
        }

        private string numero;
        public string Numero
        {
            get { return numero; }
            set { SetProperty(ref numero, value); }
        }

        private string nombreJorge;
        public string NombreJorge
        {
            get { return nombreJorge; }
            set { SetProperty(ref nombreJorge, value); }
        }

        private string poderJorge;
        public string PoderJorge
        {
            get { return poderJorge; }
            set { SetProperty(ref poderJorge, value); }
        }

        private ImageSource imagenJorge;
        public ImageSource ImagenJorge
        {
            get { return imagenJorge; }
            set { SetProperty(ref imagenJorge, value); }
        }

        public ICommand ConsultarCommand { get; }
        public ICommand IrAHeroesGuardadosCommand { get; }

        private async void Consultar()
        {
            string accessToken = "6958948077535036";

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                using (var client = new HttpClient())
                {
                    var requestUri = $"https://superheroapi.com/api/{accessToken}/{Numero}";
                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var DC = JsonConvert.DeserializeObject<Rootobject>(json);
                        NombreJorge = DC.name;
                        PoderJorge = $"Power: {DC.powerstats.power}";
                        ImagenJorge = ImageSource.FromUri(new Uri(DC.image.url));

                        // Guardar en la base de datos
                        var heroeGuardado = new HeroeGuardado
                        {
                            Nombre = DC.name,
                            Poder = DC.powerstats.power,
                            ImagenUrl = DC.image.url,
                        };

                        await _dbContext.SaveHeroeGuardadoAsync(heroeGuardado);
                    }
                }
            }
        }

        private async void IrAHeroesGuardados()
        {
            await Shell.Current.GoToAsync(nameof(HeroesGuardadosJorge));
        }

    }
}

