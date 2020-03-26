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
	public partial class PaginaPedidoSoluci : ContentPage
	{
		public PaginaPedidoSoluci ()
		{
			InitializeComponent ();
            btnConsultar.Clicked += BtnConsultar_Clicked;
            pmc.SelectedIndexChanged += Pmc_SelectedIndexChanged;
            lbl.IsVisible = false;
            txtn.IsVisible = false;
        }

        private void Pmc_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = pmc.Items[pmc.SelectedIndex];
            if (selectedValue.Equals("Numero De Reclamo"))
            {
                lbl.IsVisible = true;
                txtn.IsVisible = true;
            }
            else
            {
                txtn.IsVisible = false;
                lbl.IsVisible = false;
            }
        }

        private void BtnConsultar_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new frmConsultarReclamo());
        }

      
    }
}