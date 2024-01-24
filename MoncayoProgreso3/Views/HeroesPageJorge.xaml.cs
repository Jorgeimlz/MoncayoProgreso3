using MoncayoProgreso3.Data;
using MoncayoProgreso3.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;

namespace MoncayoProgreso3.Views
{
    public partial class HeroesPageJorge : ContentPage
    {
        private HeroesDbContext _dbContext;

        public HeroesPageJorge()
        {
            InitializeComponent();
            _dbContext = new HeroesDbContext(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HeroesGuardados.db3"));
        }


        public async void Button_Clicked(object sender, EventArgs e)
        {
            string accessToken = "6958948077535036";
            string Numero = numDC.Text;

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
                        lblNombreJorge.Text = DC.name;
                        lblPoderJorge.Text = $"Power: {DC.powerstats.power}";
                        lblImagenJorge.Source = ImageSource.FromUri(new Uri(DC.image.url));

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

        private void ButtonHeroes_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(HeroesGuardadosJorge));
        }
    }
}
