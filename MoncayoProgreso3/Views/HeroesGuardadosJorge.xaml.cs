using MoncayoProgreso3.Data;
using MoncayoProgreso3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MoncayoProgreso3.Views
{
    public partial class HeroesGuardadosJorge : ContentPage
    {
        private HeroesDbContext _dbContext;

        public HeroesGuardadosJorge()
        {
            InitializeComponent();
            _dbContext = new HeroesDbContext(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HeroesGuardados.db3"));
            MostrarHistorial();
        }

        private async void MostrarHistorial()
        {
            var heroesGuardados = await _dbContext.GetHeroesGuardadosAsync();

            foreach (var heroe in heroesGuardados)
            {
                var formattedText = $"Nombre: {heroe.Nombre}, Poder: {heroe.Poder}";

                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Margin = new Thickness(0, 0, 0, 10)
                };

                var nombreLabel = new Label
                {
                    Text = formattedText,
                    FontSize = 16
                };

                var imagen = new Microsoft.Maui.Controls.Image
                {
                    Source = ImageSource.FromUri(new Uri(heroe.ImagenUrl)),
                    WidthRequest = 150,
                    HeightRequest = 150
                };

                var btnEliminar = new Button
                {
                    Text = "Eliminar",
                    CommandParameter = heroe.Id,
                    WidthRequest = 250
                };

                btnEliminar.Clicked += BtnEliminar_Clicked;

                stackLayout.Children.Add(nombreLabel);
                stackLayout.Children.Add(imagen);
                stackLayout.Children.Add(btnEliminar);

                historialLayoutJorge.Children.Add(stackLayout);
            }
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            // Obtenemos el Id del héroe desde el parámetro del comando
            if (sender is Button btn && btn.CommandParameter is int heroeId)
            {
                // Eliminamos el héroe de la base de datos
                await _dbContext.DeleteHeroeGuardadoAsync(heroeId);

                // Actualizamos la vista recargando el historial
                MostrarHistorial();
            }
        }

    }



}
