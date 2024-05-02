using App.Views.ViewsVehiculos;

namespace App.Views;

public partial class InicioPrincipal : ContentPage
{
	public InicioPrincipal()
	{
		InitializeComponent();
	}

    private async void MotosBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InicioApp());
    }


    private async void VehiculosBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InicioAppVehiculos());
    }
}