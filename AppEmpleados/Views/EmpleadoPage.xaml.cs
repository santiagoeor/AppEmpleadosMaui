using AppEmpleados.ViewModels;

namespace AppEmpleados.Views;

public partial class EmpleadoPage : ContentPage
{
	public EmpleadoPage(EmpleadoViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}