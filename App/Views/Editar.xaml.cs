using App.Modelos;
using App.Repository;

namespace App.Views;

public partial class Editar : ContentPage
{
    RepositoryApp repositoryApp = new RepositoryApp();
    public AppModelos VehiculoSeleccionado { get; }

    public Editar()
    {
        InitializeComponent();
    }
    public Editar(AppModelos vehiculoSeleccionado)
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
        AppModelos vehiculoEditado = new AppModelos()
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
        var guardada = await repositoryApp.UpdateAsync(vehiculoEditado);

        if (guardada)
        {
            await DisplayAlert("Notificación", "Moto editada correctamente", "OK");
            await Navigation.PopAsync();
        }
    }
    private async void CancelarBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}