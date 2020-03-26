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
	public partial class menudc : ContentPage
	{
		public menudc ()
		{
			InitializeComponent ();
            BtnU.Clicked += BtnU_Clicked;
            BtnR.Clicked += BtnR_Clicked;
            BtnAd.Clicked += BtnAd_Clicked;
            BtnPediSolu.Clicked += BtnPediSolu_Clicked;
            BtnOS.Clicked += BtnOS_Clicked;

           // BtnU.Clicked -= BtnU_Clicked;
            BtnR.Clicked -= BtnR_Clicked;
            BtnAd.Clicked -= BtnAd_Clicked;
            BtnPediSolu.Clicked -= BtnPediSolu_Clicked;
            BtnOS.Clicked -= BtnOS_Clicked;

            BtnOServicio.Clicked += BtnOServicio_Clicked; 
            BtnAudito.Clicked += BtnAudito_Clicked;

            BtnOServicio.Clicked -= BtnOServicio_Clicked;
            BtnAudito.Clicked -= BtnAudito_Clicked;

            img.Source = ImageSource.FromResource("DigitalClaimT.logo_DigitalClaimLogin.png");
            imgAbajo.Source = ImageSource.FromResource("DigitalClaimT.FondoMenu.PNG");
        }

        private void BtnOServicio_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new FrmOS());
        }

        private void BtnAudito_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new frmAuditorias());
        }

        private void BtnOS_Clicked(object sender, EventArgs e)
        {
            
        }

        private void BtnPediSolu_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new PaginaPedidoSoluci());
        }

        private void BtnAd_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new frmNotificaciones());
        }

        private void BtnR_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new FrmNuevoReclamo());
        }

        private void BtnU_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new DatosUsuarios());
        }
    }
}