using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherMobileApp.UI.Controls.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherViewCell : ViewCell
    {
        public WeatherViewCell()
        {
            InitializeComponent();
        }
    }
}