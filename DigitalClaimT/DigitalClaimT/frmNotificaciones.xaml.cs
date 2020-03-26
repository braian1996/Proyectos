using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigitalClaimT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class frmNotificaciones : ContentPage
	{
		public frmNotificaciones ()
		{
			InitializeComponent ();


            //var listView = new ListView
            //{
            //    RowHeight = 40
            //};

            lvwNotificaciones.ItemsSource = new string[]
            {
                "10/06/2018                        10:22                            Pendiente", "17/06/2018                        15:46                            Asignado", "22/06/2018                        21:10                            Finalizado"
            };
            //Content = new StackLayout
            //{
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    Children = { listView }
            //};

            //    listView.ItemSelected += async (sender, e) =>
            //    {
            //        await DisplayAlert("Has tocado!", e.SelectedItem + "seleccionado!", "OK");
            //    };
        }

    }
}