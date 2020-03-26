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
	public partial class frmResulOrdenServicio : ContentPage
	{
		public frmResulOrdenServicio ()
		{
			InitializeComponent ();
            btnGenerarAudi.Clicked += BtnGenerarAudi_Clicked;
            btnGenerarAudi.Clicked -= BtnGenerarAudi_Clicked;
        }

        private void BtnGenerarAudi_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new frmAuditorias());
        }
    }
}