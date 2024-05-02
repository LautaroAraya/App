using App.Modelos;
using App.Repositories;

namespace App.Views.ViewsVehiculos;

public partial class AgregarVehiculos : ContentPage
{
    RepositoryVehiculos repositoryVehiculos = new RepositoryVehiculos();
    public AgregarVehiculos()
	{
		InitializeComponent();
	}

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        AppVehiculos nueva = new AppVehiculos()
        {
            nombre = txtNombre.Text,
            modelo = txtModelo.Text,
            velocidad_max = Convert.ToInt32(txtVelocidadMax.Text),
            precio = Convert.ToInt32(txtPrecio.Text),
            imagen_url = txtImagenUrl.Text,
            marca = txtMarca.Text,
        };

        //enviamos por POST el objeto que creamos a la URL de la API
        var agregada = await repositoryVehiculos.AddAsync(nueva);

        if (agregada)
        {
            await DisplayAlert("Notificación", "Vehiculo guardado", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void CancelarBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }
}