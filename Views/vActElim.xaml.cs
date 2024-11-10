
using dsegoviaSA6.Models;
using Newtonsoft.Json;
using System.Text;

namespace dsegoviaSA6.Views;

public partial class vActElim : ContentPage
{
    private object cliente;

    public vActElim(Estudiante datos)
    {
        InitializeComponent();

        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre.ToString();
        txtApellido.Text = datos.apellido.ToString();
        txtEdad.Text = datos.edad.ToString();
        
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            HttpClient cliente = new HttpClient();

          
            var parametros = new Dictionary<string, string>
        {
            { "codigo", txtCodigo.Text },
            { "nombre", txtNombre.Text },
            { "apellido", txtApellido.Text },
            { "edad", txtEdad.Text },
            { "accion", "actualizar" } 
        };

            
            var contenido = new FormUrlEncodedContent(parametros);
            var respuesta = await cliente.PostAsync("http://192.168.1.3/uisraelws/estudiante.php", contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Estudiante actualizado correctamente", "Cerrar");
                await Navigation.PopAsync(); 
            }
            else
            {
                var mensajeError = await respuesta.Content.ReadAsStringAsync();
                await DisplayAlert("Error", "No se pudo actualizar el estudiante: " + mensajeError, "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR", ex.Message, "Cerrar");
        }
    }




    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        bool confirmacion = await DisplayAlert("Confirmación", "¿Desea eliminar este estudiante?", "Sí", "No");

        if (confirmacion)
        {
            try
            {
                
                HttpClient cliente = new HttpClient();

               
                var respuesta = await cliente.DeleteAsync($"http://192.168.1.3/uisraelws/estudiante.php?codigo={txtCodigo.Text}");

                if (respuesta.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Estudiante eliminado correctamente", "Cerrar");
                    await Navigation.PopAsync(); 
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar el estudiante", "Cerrar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERROR", ex.Message, "Cerrar");
            }
        }
    }

}