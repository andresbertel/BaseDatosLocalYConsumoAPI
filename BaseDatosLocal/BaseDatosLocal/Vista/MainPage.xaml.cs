using BaseDatosLocal.Aplicacion;
using BaseDatosLocal.Model;
using BaseDatosLocal.Vista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BaseDatosLocal
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        Persona persona = null;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BuscarUno(object sender, EventArgs e)
        {
            // persona = await App.BaseDatos.BuscarUno(Convert.ToInt32(id.Text));
            var idPersona = id.Text;           
            persona = await ConsumirAPI.BuscarPorIdAPIAsync("http://192.168.1.13/", "application/json", "UTF-8", "api/v1/persona/BuscarUno", idPersona);

            if (persona == null)
            {
              await  DisplayAlert("Buscar","Persona no encontrada","Ok");
              limpiar();
            }
            else 
            {
                nombres.Text = persona.Nombres;
                apellidos.Text = persona.Apellidos;
               // celular.Text = persona.Celular;
            }

           
        }

        private async void Insertar(object sender, EventArgs e)
        {
                var nombre = nombres.Text;
                var apellido = apellidos.Text;
            // var cel = celular.Text;

            Persona persona;
            if ((string.IsNullOrEmpty(id.Text)))
            {
                persona = new Persona
                {
                    Nombres = nombre,
                    Apellidos = apellido,
                    //  Celular = cel
                };
            }
            else
            {
                persona = new Persona
                {
                    Id = Convert.ToInt32(id.Text),
                    Nombres = nombre,
                    Apellidos = apellido,
                    //  Celular = cel
                };
            }
                

               // var resultado = await App.BaseDatos.GuardarPersona(persona); //Guardar en base de datos local
                var resultadoApi = await ConsumirAPI.GuardarActualizarAPI("http://192.168.1.13/", "application/json", "UTF-8", "api/v1/persona/Inserta", "api/v1/persona/Actualizar", persona);

                if (resultadoApi > 0)
                {
                    await DisplayAlert("Mensaje", "Usuario insertado/actualizado", "Ok");
                }
                else
                {
                    await DisplayAlert("Mensaje", "Usuario NO insertado/actualizado", "Ok");
                }

                limpiar();
            
           
            
        }

        private async void Eliminar(object sender, EventArgs e)
        {
            // var personaEliminada = await App.BaseDatos.EliminarPersona(persona);
            var idPersona = Convert.ToInt32(id.Text);
            var personaEliminada = await ConsumirAPI.Eliminar("http://192.168.1.13/", "application/json", "UTF-8", "api/v1/persona/Eliminar", idPersona);

            if (personaEliminada > 0)
            {
               await DisplayAlert("Eliminar","Persona Eliminada","ok");
            }
            else
            {
                await DisplayAlert("Eliminar", "Persona No Eliminada", "ok");
            }

            limpiar();
        }

        private async void VerTodos(object sender, EventArgs e)
        {

            // var lista = await App.BaseDatos.GetPersonas();
            List<Persona> listaApi = await ConsumirAPI.ListarTodos("http://192.168.1.13/", "application/json", "UTF-8", "api/v1/persona/BuscarTodos");

            await Navigation.PushAsync(new Todos(listaApi));
            
        }

        private void limpiar()
        {
            id.Text = null;
            nombres.Text = "";
            apellidos.Text = "";
            celular.Text = "";
        }
    }
}
