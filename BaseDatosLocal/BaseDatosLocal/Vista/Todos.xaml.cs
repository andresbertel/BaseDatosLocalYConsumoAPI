using BaseDatosLocal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseDatosLocal.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Todos : ContentPage
    {
        
        public Todos(List<Persona> personas)
        {
            InitializeComponent();

            Lista.ItemsSource = personas;



        }
    }
}