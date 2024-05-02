using System.Collections.ObjectModel;
using App.Modelos;
using App.Repository;
using App.Views.ViewsVehiculos;

namespace App.Views;

public partial class InicioApp : ContentPage
{
    public ObservableCollection<AppModelos> Vehiculos { get; set; }
    public AppModelos VehiculoSeleccionado { get; set; }
    RepositoryApp RepositoryApp = new RepositoryApp();
    public InicioApp()
    {
        InitializeComponent();
        Vehiculos = new ObservableCollection<AppModelos>();
        VehiculosCollectionView.SelectionChanged += SeleccionarVehiculo;

    }

    private void SeleccionarVehiculo(object sender, SelectionChangedEventArgs e)
    {
        if (VehiculosCollectionView.SelectedItem != null)
        {
            VehiculoSeleccionado = (AppModelos)VehiculosCollectionView.SelectedItem;
        }
    }

    public async void GetAllMotos(object sender, EventArgs e)
    {

        Vehiculos = await RepositoryApp.GetAllAsync();
        VehiculosCollectionView.ItemsSource = Vehiculos;
    }

    private void SeleccionarVehiculoEnCollectionView()
    {
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
        await Navigation.PushAsync(new Agregar());
    }

    private async void EliminarBtn_Clicked(object sender, EventArgs e)
    {
        if(VehiculoSeleccionado != null)
        {
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar", $"¿Esta seguro que desea eliminar esta moto?: {VehiculoSeleccionado.marca} ", "Si", "No");
            if (respuesta)
            {
                var eliminado = await RepositoryApp.RemoveAsync(VehiculoSeleccionado._id);
                if (eliminado)
                {
                    await Application.Current.MainPage.DisplayAlert("Eliminar", $"Se ha eliminado la moto: {VehiculoSeleccionado.marca} correctamente", "Ok");
                    GetAllMotos(this, EventArgs.Empty);
                }

            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Eliminar", $"Debe seleccionar la Moto a elinimar", "ok");
        }

    }


    private async void EditarBtn_Clicked(object sender, EventArgs e)
    {
        if (VehiculoSeleccionado != null)
        {
            await Navigation.PushAsync(new Editar(VehiculoSeleccionado));
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Editar", "Error: debe seleccionar la Moto a editar", "ok");
        }

    }

    private async void IrAVehiculos_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InicioAppVehiculos());
    }
}