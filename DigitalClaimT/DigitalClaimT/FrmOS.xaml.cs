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
	public partial class FrmOS : ContentPage
	{
		public FrmOS ()
		{
            InitializeComponent();
            BtnBuscarOS.Clicked += BtnBuscarOS_Clicked;

            BtnBuscarOS.Clicked -= BtnBuscarOS_Clicked;
        }

        private void BtnBuscarOS_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new frmResulOrdenServicio());
        }

       
    }
}