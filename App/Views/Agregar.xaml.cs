using App.Modelos;
using App.Repository;
using System;

namespace App.Views;

public partial class Agregar : ContentPage
{
    RepositoryApp repositoryApp = new RepositoryApp();
    public Agregar()
	{
		InitializeComponent();
	}

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        AppModelos nueva = new AppModelos()
        {
            nombre = txtNombre.Text,
            modelo = txtModelo.Text,
            velocidad_max = Convert.ToInt32(txtVelocidadMax.Text),
            precio = Convert.ToInt32(txtPrecio.Text),
            imagen_url = txtImagenUrl.Text,
            marca = txtMarca.Text,
        };

        //enviamos por POST el objeto que creamos a la URL de la API
        var agregada = await repositoryApp.AddAsync(nueva);

        if (agregada)
        {
            await DisplayAlert("Notificación", "Moto guardada", "OK");
            await Navigation.PopAsync();
        }

    }
    private async void CancelarBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}