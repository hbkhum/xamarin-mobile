using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views.Cars
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarDetailView : ContentPage
    {
        public CarDetailView()
        {
            InitializeComponent();
        }
    }
}