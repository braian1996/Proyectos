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
	public partial class menudcOS : ContentPage
	{
		public menudcOS ()
		{
			InitializeComponent ();
            img.Source = ImageSource.FromResource("DigitalClaimT.logo_DigitalClaimLogin.png");
            imgAbajo.Source = ImageSource.FromResource("DigitalClaimT.FondoMenu.PNG");
            BtnOS.Clicked += BtnOS_Clicked;
            BtnAudito.Clicked += BtnAudito_Clicked;

            //BtnOS.Clicked -= BtnOS_Clicked;
            BtnAudito.Clicked -= BtnAudito_Clicked;
        }

        private void BtnAudito_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new frmAuditorias());
        }

        private void BtnOS_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new FrmOS());
        }
    }
}