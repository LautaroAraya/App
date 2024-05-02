using App.Modelos;
using App.Repositories;
using System.Collections.ObjectModel;

namespace App.Views.ViewsVehiculos;

public partial class InicioAppVehiculos : ContentPage
{
    public ObservableCollection<AppVehiculos> Vehiculos { get; set; }
    public AppVehiculos VehiculoSeleccionado { get; set; }
    RepositoryVehiculos RepositoryVehiculos = new RepositoryVehiculos();
    public InicioAppVehiculos()
    {
        InitializeComponent();
        Vehiculos = new ObservableCollection<AppVehiculos>();
        VehiculosCollectionView.SelectionChanged += SeleccionarVehiculo;

    }

    private void SeleccionarVehiculo(object sender, SelectionChangedEventArgs e)
    {
        if (VehiculosCollectionView.SelectedItem != null)
        {
            VehiculoSeleccionado = (AppVehiculos)VehiculosCollectionView.SelectedItem;
        }
    }

    public async void GetAllVehiculos(object sender, EventArgs e)
    {

        Vehiculos = await RepositoryVehiculos.GetAllAsync();
        VehiculosCollectionView.ItemsSource = Vehiculos;
    }

    private void SeleccionarVehiculoEnCollectionView()
    {
        //iteramos las peliculas hasta encontrar la que coincide con la pelicula seleccionada, al encontrarla la utilizaremos para indicar que es el selectedItem del CollectionView e interrumpiremos la interaccion. 
        foreach (var vehiculo in Vehiculos)
        {
            if (vehiculo._id == VehiculoSeleccionado._id)
            {
                VehiculosCollectionView.SelectedItem = vehiculo;
                break;
            }
        }
    }

    private async void AgregarBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarVehiculos());
    }

    private async void EliminarBtn_Clicked(object sender, EventArgs e)
    {
        if (VehiculoSeleccionado != null)
        {
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar", $"¿Esta seguro que desea eliminar este Vehiculo?: {VehiculoSeleccionado.marca} ", "Si", "No");
            if (respuesta)
            {
                var eliminado = await RepositoryVehiculos.RemoveAsync(VehiculoSeleccionado._id);
                if (eliminado)
                {
                    await Application.Current.MainPage.DisplayAlert("Eliminar", $"Se ha eliminado el Vehiculo: {VehiculoSeleccionado.marca} correctamente", "Ok");
                    GetAllVehiculos(this, EventArgs.Empty);
                }

            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Eliminar", $"Debe seleccionar el Vehiculo a elinimar", "ok");
        }

    }


    private async void EditarBtn_Clicked(object sender, EventArgs e)
    {
        if (VehiculoSeleccionado != null)
        {
            await Navigation.PushAsync(new EditarVehiculos(VehiculoSeleccionado));
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Editar", "Error: debe seleccionar el Vehiculo a editar", "ok");
        }

    }


    private async void InicioPrincipalBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InicioPrincipal());
    }
}