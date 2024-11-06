using dsegoviaSA6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace dsegoviaSA6.Views;

public partial class vEstudiante : ContentPage
{
	private const string Url = "http://192.168.85.52/uisraelws/estudiante.php";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Estudiante> estud;

    public vEstudiante()
	{
		InitializeComponent();
		mostrar();
	}

	public async void mostrar()
	{
		var content = await cliente.GetStringAsync(Url);
		List<Estudiante> mostrarEst = JsonConvert.DeserializeObject<List<Estudiante>>(content);
		estud = new ObservableCollection<Estudiante>(mostrarEst);
		lvEstudiantes.ItemsSource = estud;
	}
}