using App.Modelos;
using App.Repositories;

namespace App.Views.ViewsVehiculos;

public partial class EditarVehiculos : ContentPage
{
    RepositoryVehiculos repositoryVehiculos = new RepositoryVehiculos();
    public AppVehiculos VehiculoSeleccionado { get; }
    public EditarVehiculos()
    {
        InitializeComponent();
    }


    public EditarVehiculos(AppVehiculos vehiculoSeleccionado)
    {
        InitializeComponent();
        VehiculoSeleccionado = vehiculoSeleccionado;
        CargarDatosEnPantalla();
    }

    private void CargarDatosEnPantalla()
    {
        txtNombre.Text = VehiculoSeleccionado.nombre;
        txtModelo.Text = VehiculoSeleccionado.modelo;
        txtVelocidadMax.Text = VehiculoSeleccionado.velocidad_max.ToString();
        txtPrecio.Text = VehiculoSeleccionado.precio.ToString();
        txtImagenUrl.Text = VehiculoSeleccionado.imagen_url;
        txtMarca.Text = VehiculoSeleccionado.marca;
    }

    private async void GuardarBtn_Clicked(object sender, EventArgs e)
    {
        AppVehiculos vehiculoEditado = new AppVehiculos()
        {
            _id = VehiculoSeleccionado._id,
            nombre = txtNombre.Text,
            modelo = txtModelo.Text,
            velocidad_max = Convert.ToInt32(txtVelocidadMax.Text),
            precio = Convert.ToInt32(txtPrecio.Text),
            imagen_url = txtImagenUrl.Text,
            marca = txtMarca.Text

        };
        //enviamos por PUT el objeto que creamos a la URL de la API
        var guardada = await repositoryVehiculos.UpdateAsync(vehiculoEditado);

        if (guardada)
        {
            await DisplayAlert("Notificación", "Vehiculo editado correctamente", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void CancelarBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}